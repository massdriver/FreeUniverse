using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Unity.Solar
{
    public class ComponentHardpoint : MonoBehaviour, IValueMapSerializable
    {
        public string hardpointType = "hp_any";

        public void ReadFromValueMap(ValueMap map)
        {
            throw new NotImplementedException();
        }

        public ValueMap WriteToValueMap()
        {
            ValueMap pmap = new ValueMap();

            pmap[Arch.ArchConst.Id] = Hash.FromString64(gameObject.name);
            pmap[Arch.ArchConst.Nickname] = gameObject.name;
            pmap[Arch.ArchComponentHardpoint.HpType] = hardpointType;
            pmap[Arch.ArchComponentHardpoint.HpTransform3D] = new Transform3D(gameObject.transform);

            return pmap;
        }
    }
}
