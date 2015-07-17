using FreeUniverse.Common.Unity;
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

        public ArchObject SetID(string nickname)
        {
            id = Hash.FromString64(nickname);
            return this;
        }

        [FieldCopy]
        public string nickname { get; set; }
        [FieldCopy]
        public string idsInfo { get; set; }
        [FieldCopy]
        public string idsObjectName { get; set; }
        [FieldCopy]
        public string idsDescription { get; set; }

        public static T Convert<D, T>(D desc)
            where T : ArchObject, new()
            where D : DescriptorArch
        {
            return FieldCopySerializer<D, T>.CopyValuesByNames(desc, new T()).SetID(desc.nickname) as T;
        }
    }
}
