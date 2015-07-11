using FreeUniverse.Common.Arch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.World
{
    public sealed class Solar : IBaseObject
    {
        public uint id { get; private set; }
        public string nickname { get; set; }
        public SolarController controller { get; set; }
        public WorldController worldController { get; private set; }

        public bool valid { get; set; }

        private List<SolarComponent> components { get; set; }

        public Solar(WorldController worldController, uint id, ArchSolar arch)
        {
            this.valid = true;
            this.worldController = worldController;
            this.id = id;
            this.components = new List<SolarComponent>();

            CreateComponents(arch);
        }

        private void CreateComponents(ArchSolar arch)
        {

        }

        public void Update(float dt)
        {

        }

        public void Init()
        {
            
        }

        public void Release()
        {
            
        }
    }
}
