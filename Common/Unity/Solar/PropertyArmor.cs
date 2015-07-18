using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Unity.Solar
{
    public sealed class PropertyArmor : ComponentProperty
    {
        [FieldCopy]
        public float hitPoints = 1000.0f;
        [FieldCopy]
        public float resistanceKinetic = 0.01f;
        [FieldCopy]
        public float resistanceDarkMatter = 0.01f;
        [FieldCopy]
        public float resistanceEMP = 0.01f;
        [FieldCopy]
        public float resistanceEnergy = 0.01f;
    }
}
