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
            Invalid, // MH: im outta here
            Static, // static bases
            PlayerSolar, // MH: dynamic dockable ships or similar that are property of character
            LocalDynamicSystem, // MH: zone server only generated system, possibly for missions (use system + baseid properties)
            GlobalDynamicSystem // MH: universe wide accessible system (use system + baseid properties)
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

        // valid only with type equal to PlayerSolar
        public ulong character
        {
            get
            {
                return id0;
            }
        }

        // valid only with type equal to PlayerSolar
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
