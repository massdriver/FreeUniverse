using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Shared
{
    public sealed class UniverseLocation : IBinarySerializable
    {
        public enum DockType
        {
            Invalid,
            Static, // static bases
            Dynamic // MH: dynamic dockable ships or similar that are property of character
        }

        private ulong id0 { get; set; }
        private ulong id1 { get; set; }
        public DockType type { get; set; }

        public ulong system
        {
            get
            {
                return id0;
            }
        }

        public ulong baseid
        {
            get
            {
                return id1;
            }
        }

        public ulong character
        {
            get
            {
                return id0;
            }
        }

        public ulong solarid
        {
            get
            {
                return id1;
            }
        }

        public void Write(System.IO.BinaryWriter writer)
        {
            writer.Write(id0);
            writer.Write(id1);
            writer.Write((byte)type);
        }

        public void Read(System.IO.BinaryReader reader)
        {
            id0 = reader.ReadUInt64();
            id1 = reader.ReadUInt64();
            type = (DockType)reader.ReadByte();
        }
    }
}
