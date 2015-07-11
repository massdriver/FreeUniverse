using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.World
{
    public class SolarComponentProperty : IBaseObject
    {
        public SolarComponent component { get; private set; }

        public SolarComponentProperty(SolarComponent component)
        {
            this.component = component;
        }

        public virtual void Update(float dt)
        {

        }

        public virtual void Init()
        {
            
        }

        public virtual void Release()
        {
            
        }
    }
}
