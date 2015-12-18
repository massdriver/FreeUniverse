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
        public int index { get; set; } // MH: optimized arch id, globaly unique after all arches loaded

        protected ArchObject SetID(string nickname)
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

        public virtual bool Validate()
        {
            return id != 0 && nickname.Length > 0;
        }

        public virtual void ReadHeader(INIReaderHeader header)
        {
            foreach (INIReaderParameter p in header.parameters)
                ReadParameter(p);
        }

        public virtual void ReadParameter(INIReaderParameter parameter)
        {
            if (parameter.Check(ArchConst.Nickname))
            {
                nickname = parameter.GetString(0);
                id = Hash.FromString64(nickname);
            }
            else if (parameter.Check(ArchConst.IdsInfo))
            {
                
            }
            else if (parameter.Check(ArchConst.IdsObjectName))
            {

            }
            else if (parameter.Check(ArchConst.IdsDescription))
            {

            }
        }
    }
}
