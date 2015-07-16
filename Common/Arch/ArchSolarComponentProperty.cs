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
    }
}
