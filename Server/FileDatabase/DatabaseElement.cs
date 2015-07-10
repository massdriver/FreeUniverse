using FreeUniverse.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FreeUniverse.Server.FileDatabase
{
    public class DatabaseElement : IBinarySerializable
    {
        public ulong id { get; protected set; }

        public static readonly ulong ID_INVALID = 0;
        public static readonly uint MAGIC_NUMBER = 0xFDB00001;

        public string hexIdentifier
        {
            get
            {
                return GenerateHexIdentifier(id);
            }
        }

        public DatabaseElement()
        {
            id = DatabaseElement.ID_INVALID;
        }

        public DatabaseElement(string nickname)
        {
            id = GenerateID(nickname);
        }

        public virtual void Write(BinaryWriter writer)
        {
            writer.Write(MAGIC_NUMBER);
            writer.Write(id);
        }

        public virtual void Read(BinaryReader reader)
        {
            if (reader.ReadUInt32() != MAGIC_NUMBER)
                throw new Exception("incorrect database element file start");

            id = reader.ReadUInt64();
        }

        public static string GenerateHexIdentifier(ulong id)
        {
            return string.Format("{0:X}", id);
        }

        public static ulong GenerateID(string nickname)
        {
            return DatabaseHashService.StringToUINT64(nickname);
        }
    }
}
