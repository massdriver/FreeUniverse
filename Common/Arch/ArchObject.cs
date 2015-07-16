using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Arch
{
    public class ArchObject
    {
        public ulong id { get; set; }
        public uint index { get; set; } // MH: optimized arch id, globaly unique after all arches loaded

        [FieldCopy]
        public string nickname { get; set; }
        [FieldCopy]
        public string idsInfo { get; set; }
        [FieldCopy]
        public string idsObjectName { get; set; }
        [FieldCopy]
        public string idsDescription { get; set; }
    }
}
