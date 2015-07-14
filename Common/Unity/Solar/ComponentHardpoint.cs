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

        public static readonly string HpName = "hpname";
        public static readonly string HpRotation = "hprot";
        public static readonly string HpPosition = "hppos";
        public static readonly string HpType = "hptype";
        public static readonly string HpTransform3D = "hptransform3d";

        public void ReadFromValueMap(ValueMap map)
        {
            throw new NotImplementedException();
        }

        public ValueMap WriteToValueMap()
        {
            ValueMap pmap = new ValueMap();

            pmap[HpName] = gameObject.name;
            pmap[HpType] = hardpointType;
            pmap[HpTransform3D] = new Transform3D(gameObject.transform);

            return pmap;
        }
    }
}
