using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Arch
{
    public sealed class ArchSolar : ArchObject
    {
        [FieldCopy]
        public List<ArchSolarComponent> components { get; set; }

        public override bool Validate()
        {
            return base.Validate() && components != null && components.Count > 0;
        }
    }
}
