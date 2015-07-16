using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Arch
{
    public sealed class ArchSolarComponent : ArchObject
    {
        [FieldCopy]
        public List<ArchComponentHardpoint> hardpoints { get; set; }
    }
}
