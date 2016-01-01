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
        public string nickname { get; set; }
        public string idsInfo { get; set; }
        public string idsObjectName { get; set; }
        public string idsDescription { get; set; }

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
