using FreeUniverse.Common.Arch;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace FreeUniverse.Common.Unity
{
    public class EditableObjectDescriptor : MonoBehaviour
    {
        public string nickname;
        
        [Multiline]
        public string idsObjectName;

        [Multiline]
        public string idsInfo;
        
        [Multiline]
        public string idsDescription;

        public virtual ValueMap ToValueMap()
        {
            ValueMap pmap = new ValueMap();

            pmap[ArchConst.Nickname] = nickname;
            pmap[ArchConst.IdsInfo] = idsInfo;
            pmap[ArchConst.IdsObjectName] = idsObjectName;
            pmap[ArchConst.IdsDescription] = idsDescription;

            return pmap;
        }
    }
}
