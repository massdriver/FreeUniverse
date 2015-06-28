using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Arch
{
    public class ArchObject
    {
        public ulong id { get; set; }
        public string nickname { get; set; }

        public string idsInfo { get; set; }
        public string idsObjectName { get; set; }
        public string idsDescription { get; set; }

        public virtual void Read(ValueMap map)
        {
            id = Hash.FromString(map[ArchConst.Nickname]);
            nickname = map[ArchConst.Nickname];
            idsInfo = map[ArchConst.IdsInfo];
            idsObjectName = map[ArchConst.IdsObjectName];
            idsDescription = map[ArchConst.IdsDescription];
        }
    }
}
