using FreeUniverse.Common.Arch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.World
{
    public sealed class SolarComponent : IBaseObject
    {
        public Solar solar { get; private set; }

        public SolarComponentPropertyHull hull { get; private set; }

        public SolarComponent(Solar solar, ArchSolarComponent arch)
        {
            this.solar = solar;
        }

        public void Init()
        {
            
        }

        public void Release()
        {
            
        }

        public void Update(float time)
        {
            
        }
    }
}
