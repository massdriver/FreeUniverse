using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.World
{
    public class SolarControlPanel
    {
        public Solar solar { get; private set; }

        public SolarControlPanel(Solar solar)
        {
            this.solar = solar;
        }

        public void Update(float dt)
        {

        }


    }
}
