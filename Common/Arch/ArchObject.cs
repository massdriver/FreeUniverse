using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Arch
{
    public class ArchObject : IValueMapSerializable
    {
        public ulong id { get; set; }
        public uint syncid { get; set; } // MH: optimized arch id, globaly unique
        public string nickname { get; set; }
        public string idsInfo { get; set; }
        public string idsObjectName { get; set; }
        public string idsDescription { get; set; }

        public virtual void ReadFromValueMap(ValueMap pmap)
        {
            id = pmap[ArchConst.Id];
            nickname = pmap[ArchConst.Nickname];
            idsInfo = pmap[ArchConst.IdsInfo];
            idsObjectName = pmap[ArchConst.IdsObjectName];
            idsDescription = pmap[ArchConst.IdsDescription];
        }

        public virtual ValueMap WriteToValueMap()
        {
            ValueMap pmap = new ValueMap();
            pmap[ArchConst.Id] = id;
            pmap[ArchConst.Nickname] = nickname;
            pmap[ArchConst.IdsInfo] = idsInfo;
            pmap[ArchConst.IdsObjectName] = idsObjectName;
            pmap[ArchConst.IdsDescription] = idsDescription;
            return pmap;
        }
    }
}
