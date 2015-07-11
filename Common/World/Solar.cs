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

        private Dictionary<uint, SolarComponent> components { get; set; }

        public SolarComponent rootComponent { get; private set; }

        public Solar(WorldController worldController, uint id, ArchSolar arch)
        {
            this.valid = true;
            this.worldController = worldController;
            this.id = id;
            this.components = new Dictionary<uint, SolarComponent>();

            CreateComponents(arch);
        }

        private void CreateComponents(ArchSolar arch)
        {

        }

        public void Update(float dt)
        {
            UpdateComponentProperties<SolarComponentPropertyHull>(dt);
        }

        public void Init()
        {
            
        }

        public void Release()
        {
            
        }

        public List<T> EnumerateProperties<T>() where T : SolarComponentProperty
        {
            List<T> lst = new List<T>();

            foreach (KeyValuePair<uint, SolarComponent> pair in components)
            {
                T component = pair.Value.GetProperty<T>();

                if (component != null)
                    lst.Add(component);
            }

            return lst;
        }

        public void UpdateComponentProperties<T>(float dt) where T : SolarComponentProperty
        {
            List<T> properties = EnumerateProperties<T>();

            foreach (T property in properties)
                property.Update(dt);
        }
    }
}
