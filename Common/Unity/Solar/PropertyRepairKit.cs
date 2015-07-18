using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Unity.Solar
{
    public sealed class PropertyRepairKit : ComponentProperty
    {
        [FieldCopy]
        public float repairCapacity;
        [FieldCopy]
        public float repairRate;
    }
}
