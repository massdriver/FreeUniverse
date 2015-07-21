using FreeUniverse.Common.Arch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.World
{
    public sealed class SolarComponentHardpoint
    {
        public ulong id { get; private set; }
        public SolarComponentPropertyRigidHull hull { get; private set; }

        public SolarComponentHardpoint(ArchComponentHardpoint arch, SolarComponentPropertyRigidHull hull)
        {
            this.id = arch.id;
            this.hull = hull;
        }
    }
}
