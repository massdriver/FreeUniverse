using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Arch
{
    public sealed class ArchSolarComponentPropertyHull : ArchSolarComponentProperty
    {
        [FieldCopy]
        public float linearDrag { get; set; }

        [FieldCopy]
        public float angularDrag { get; set; }

        [FieldCopy]
        public float hitPoints { get; set; }

        [FieldCopy]
        public bool staticHull { get; set; }

        [FieldCopy]
        public bool collideWithWorld { get; set; }

        [FieldCopy]
        public bool useCustomColliders { get; set; }

        [FieldCopy]
        public string assetPath { get; set; }

        public ArchSolarComponentPropertyHull()
        {

        }


    }
}
