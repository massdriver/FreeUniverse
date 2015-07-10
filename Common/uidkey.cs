using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common
{
    public class uidkey : IBinarySerializable
    {
        public uint a, b, c, d;

        public uidkey(uint a, uint b, uint c, uint d)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }

        public uidkey(string str)
        {
            byte[] data = Encoding.ASCII.GetBytes(str);

            a = Hash.FromBytes(data, 0);
            b = Hash.FromBytes(data, a);
            c = Hash.FromBytes(data, b);
            d = Hash.FromBytes(data, c);
        }

        public uidkey(BinaryReader reader)
        {
            a = reader.ReadUInt32();
            b = reader.ReadUInt32();
            c = reader.ReadUInt32();
            d = reader.ReadUInt32();
        }

        public void Read(BinaryReader reader)
        {
            a = reader.ReadUInt32();
            b = reader.ReadUInt32();
            c = reader.ReadUInt32();
            d = reader.ReadUInt32();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(a);
            writer.Write(b);
            writer.Write(c);
            writer.Write(d);
        }

        public static bool operator <(uidkey a, uidkey b)
        {
            if (a.a < b.a)
                return true;

            if (a.a == b.a)
            {
                if (a.b < b.b)
                    return true;

                if (a.b == b.b)
                {
                    if (a.c < b.c)
                        return true;

                    if (a.c == b.c)
                    {
                        if (a.d < b.d)
                            return true;
                    }
                }
            }

            return false;
        }

        public static bool operator >(uidkey a, uidkey b)
        {
            if (a.a > b.a)
                return true;

            if (a.a == b.a)
            {
                if (a.b > b.b)
                    return true;

                if (a.b == b.b)
                {
                    if (a.c > b.c)
                        return true;

                    if (a.c == b.c)
                    {
                        if (a.d > b.d)
                            return true;
                    }
                }
            }

            return false;
        }

        public static bool operator ==(uidkey a, uidkey b)
        {
            return a.a == b.a && a.b == b.b && a.c == b.c && a.d == b.d;
        }

        public static bool operator !=(uidkey a, uidkey b)
        {
            return a.a != b.a || a.b != b.b || a.c != b.c || a.d != b.d;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(uidkey))
                return (uidkey)obj == this;

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return (int)(a ^ b ^ c ^ d);
        }

        public override string ToString()
        {
            return "uidkey=" + a + "-" + b + "-" + c + "-" + d;
        }

        public bool IsValid()
        {
            return a != 0 && b != 0 && c != 0 && d != 0;
        }
        
    }
}
