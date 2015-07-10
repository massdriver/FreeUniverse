using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace FreeUniverse.Server.FileDatabase
{
    public sealed class Database<T> where T : DatabaseElement, new()
    {
        public string fullPath { get; private set; }

        private string encryptionKey { get; set; }

        private Dictionary<ulong, T> preloadedElements { get; set; }

        private readonly string DB_FILE_EXTENSION = "dat";

        public int elementCountInMemory
        {
            get
            {
                return preloadedElements.Count;
            }
        }

        public int diskElementCount
        {
            get
            {
                return Directory.GetFiles(fullPath, "*." + DB_FILE_EXTENSION).Length;
            }
        }

        public Database(string fullDirectoryPath, string encryptionKey)
        {
            this.fullPath = fullDirectoryPath;
            this.preloadedElements = new Dictionary<ulong, T>();
            this.encryptionKey = encryptionKey;
        }

        public bool Exists(string id, out T value)
        {
            return Exists(DatabaseElement.GenerateID(id), out value);
        }

        public bool Exists(ulong id, out T value)
        {
            value = TryGetElementFromMemory(id);

            if (value == null)
                value = TryGetElementFromDiskByID(id);

            return value != null;
        }

        public T Read(string id, DatabaseLocationType type)
        {
            return Read(DatabaseElement.GenerateID(id), type);
        }

        public T Read(ulong id, DatabaseLocationType type)
        {
            switch (type)
            {
                case DatabaseLocationType.MemoryAndDisk:
                    return ReadFromMemoryFirstThenDisk(id);
                case DatabaseLocationType.MemoryOnly:
                    return TryGetElementFromMemory(id);
                case DatabaseLocationType.DiskOnly:
                    return TryGetElementFromDiskByID(id);
            }

            throw new Exception("unsupported read method");
        }

        public bool StoreToDisk(ulong id)
        {
            T e = TryGetElementFromMemory(id);

            if (e == null)
                return false;

            return ForceStoreToDisk(e);
        }

        public bool StoreAndUnload(ulong id)
        {
            T e = TryGetElementFromMemory(id);

            if (e == null)
                return false;

            ForceStoreToDisk(e);

            preloadedElements.Remove(id);

            return true;
        }

        public bool Store(T e, DatabaseLocationType type)
        {
            switch (type)
            {
                case DatabaseLocationType.MemoryAndDisk:
                    return ForceStoreToMemory(e) && ForceStoreToDisk(e);
                case DatabaseLocationType.MemoryOnly:
                    return ForceStoreToMemory(e);
                case DatabaseLocationType.DiskOnly:
                    return ForceStoreToDisk(e);
            }

            throw new Exception("unsupported store method");
        }

        public int LoadAll()
        {
            string[] files = Directory.GetFiles(fullPath, "*." + DB_FILE_EXTENSION);

            int loaded = 0;

            foreach (string s in files)
            {
                T e = TryGetElementFromDiskByPath(s);

                preloadedElements[e.id] = e;

                loaded++;
            }

            return loaded;
        }

        public int StoreAll()
        {
            int count = 0;

            foreach (KeyValuePair<ulong, T> kv in preloadedElements)
            {
                if (ForceStoreToDisk(kv.Value))
                    count++;
            }

            return count;
        }

        private T TryGetElementFromMemory(ulong id)
        {
            T memoryElement = null;

            if (preloadedElements.TryGetValue(id, out memoryElement))
                return memoryElement;

            return null;
        }

        private T ReadFromMemoryFirstThenDisk(ulong id)
        {
            T e = TryGetElementFromMemory(id);

            if (e != null)
                return e;

            return TryGetElementFromDiskByID(id);
        }

        private T TryGetElementFromDiskByID(ulong id)
        {
            return TryGetElementFromDiskByPath(MakeFullPath(id));
        }

        private bool ForceStoreToMemory(T e)
        {
            preloadedElements[e.id] = e;

            return true;
        }

        private bool ForceStoreToDisk(T e)
        {
            if (e.id == DatabaseElement.ID_INVALID)
                throw new Exception("unable to store element with an invalid identifier");

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    BinaryWriter writer = new BinaryWriter(ms);
                    e.Write(writer);
                    File.WriteAllBytes(MakeFullPath(e.id), DatabaseCryptoService.Encrypt(ms.ToArray(), encryptionKey));
                }
            }
            catch (Exception exc)
            {
                Console.Out.WriteLine(exc.Message);

                return false;
            }

            return true;
        }

        private T TryGetElementFromDiskByPath(string path)
        {
            try
            {
                T e = new T();

                e.Read(new BinaryReader(new MemoryStream(DatabaseCryptoService.Decrypt(File.ReadAllBytes(path), encryptionKey))));

                return e;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }

            return null;
        }

        private string MakeFullPath(ulong id)
        {
            string s = fullPath + "\\" + DatabaseElement.GenerateHexIdentifier(id) + "." + DB_FILE_EXTENSION;
            return s;
        }  
    }
}
