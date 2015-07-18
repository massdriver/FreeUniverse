using FreeUniverse.Common.Unity.Solar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Arch
{
    public class ArchSolarComponentProperty
    {
        [FieldCopy]
        public float mass { get; set; }
        [FieldCopy]
        public bool swapable { get; set; }

        public static T Convert<D, T>(D desc)
            where T : ArchSolarComponentProperty, new()
            where D : ComponentProperty
        {
            return FieldCopySerializer<D, T>.CopyValuesByNames(desc, new T()) as T;
        }
    }
}
