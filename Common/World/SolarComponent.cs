using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.World
{
    public sealed class SolarComponent : IBaseObject
    {
        private List<SolarComponentProperty> properties { get; set; }

        public Solar solar { get; private set; }

        public SolarComponent(Solar solar)
        {
            this.solar = solar;
            properties = new List<SolarComponentProperty>();
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
