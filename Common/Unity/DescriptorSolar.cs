using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Unity
{
    public sealed class DescriptorSolar : EditableObjectDescriptor
    {
        public float mass;
        
        [Range(0.0f, 1.0f)]
        public float angularDrag;
        
        [Range(0.0f, 1.0f)]
        public float linearDrag;

        // <= 0.0f means invulnerable
        public float hitPoints;

        [Range(0.0f, 1.0f)]
        public float resistanceKinetic;
        
        [Range(0.0f, 1.0f)]
        public float resistanceDarkMatter;
        
        [Range(0.0f, 1.0f)]
        public float resistanceEnergy;
        
        [Range(0.0f, 1.0f)]
        public float resistanceEMP;

        public bool useCustomColliders;
    }
}
