using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Unity.Solar
{
    public class ComponentProperty : MonoBehaviour, IValueMapSerializable
    {
        public float mass = 1.0f;
        public bool swapable = false;

        public static readonly string Mass = "mass";
        public static readonly string Swapable = "swapable";

        public virtual void ReadFromValueMap(ValueMap map)
        {
            
        }

        public virtual ValueMap WriteToValueMap()
        {
            ValueMap pmap = new ValueMap();

            pmap[Mass] = mass;
            pmap[Swapable] = swapable;

            return pmap;
        }
    }
}
