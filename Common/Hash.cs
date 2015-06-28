using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common
{
    public static class Hash
    {
        public static UInt32 FromString(string str)
        {
            return FromBytes(Encoding.ASCII.GetBytes(str));
        }

        public static UInt32 FromBytes(Byte[] data)
        {
            return FromBytes(data, 0);
        }

        public static UInt32 FromBytes(Byte[] data, UInt32 seed)
        {
            uint hash = seed;

            foreach (byte b in data)
            {
                hash += b;
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }

            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);
            return hash;
        }

        public static ulong FromString64(string str)
        {
            return FromBytes64(Encoding.ASCII.GetBytes(str));
        }

        public static ulong FromBytes64(Byte[] data)
        {
            uint h1 = FromBytes(data, 0);
            uint h2 = FromBytes(data, h1);

            return (((ulong)(h1)) << 32) + (ulong)(h2);
        }
    }
}
