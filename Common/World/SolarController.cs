using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.World
{
    public class SolarController : IBaseObject
    {
        public Solar solar { get; private set; }

        public SolarController(Solar solar)
        {
            this.solar = solar;
        }

        public virtual void Init()
        {
            throw new NotImplementedException();
        }

        public virtual void Release()
        {
            throw new NotImplementedException();
        }

        public virtual void Update(float time)
        {
            throw new NotImplementedException();
        }
    }
}
