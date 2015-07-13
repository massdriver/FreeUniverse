using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Arch
{
    public class ArchSolarComponent : ArchObject
    {
        public List<ArchComponentHardpoint> hardpoints { get; set; }

        public ArchSolarComponent()
        {
            hardpoints = new List<ArchComponentHardpoint>();
        }
    }
}
