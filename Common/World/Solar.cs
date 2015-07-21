using FreeUniverse.Common.Arch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.World
{
    public sealed class Solar : IBaseObject, IStateSerializable
    {
        public uint id { get; private set; }
        public string nickname { get; set; }
        public SolarController controller { get; set; }
        public WorldController worldController { get; private set; }
        public bool valid { get; set; }
        public SolarComponent rootComponent { get; private set; }
        public SolarControlPanel controlPanel { get; private set; }

        private List<SolarComponent> components { get; set; }

        public Solar(WorldController worldController, uint id, ArchSolar arch)
        {
            this.valid = true;
            this.worldController = worldController;
            this.id = id;
            this.components = new List<SolarComponent>();
            this.controlPanel = new SolarControlPanel(this);

            CreateSolarComponents(arch);
        }

        private void CreateSolarComponents(ArchSolar arch)
        {
            foreach (ArchSolarComponent compArch in arch.components)
                components.Add(new SolarComponent(this, compArch));

            rootComponent = GetComponentByID(Hash.FromString64("root_component"));

            if (rootComponent == null)
                throw new Exception("Solar component doesnt contatin root component");
        }

        private SolarComponent GetComponentByID(ulong id)
        {
            foreach (SolarComponent comp in components)
            {
                if (comp.id == id)
                    return comp;
            }

            return null;
        }

        public void Update(float dt)
        {
            if (controller != null)
                controller.Update(dt);

            UpdateComponentProperties<SolarComponentPropertyRigidHull>(dt);
        }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public void Release()
        {
            throw new NotImplementedException();
        }

        public List<T> EnumerateProperties<T>() where T : SolarComponentProperty
        {
            List<T> lst = new List<T>();

            foreach (SolarComponent comp in components)
            {
                T component = comp.GetProperty<T>();

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

        public ValueMap GetState()
        {
            throw new NotImplementedException();
        }

        public void SetState(ValueMap map)
        {
            throw new NotImplementedException();
        }
    }
}
