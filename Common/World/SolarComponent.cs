using FreeUniverse.Common.Arch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.World
{
    public sealed class SolarComponent : IBaseObject, IStateSerializable
    {
        public ulong id { get; private set; }
        public Solar solar { get; private set; }

        private Dictionary<ulong, SolarComponentHardpoint> hardpoints { get; set; }
        private Dictionary<Type, SolarComponentProperty> properties { get; set; }

        public SolarComponent(Solar solar, ArchSolarComponent arch)
        {
            this.id = arch.id;
            this.solar = solar;
            this.properties = new Dictionary<Type, SolarComponentProperty>();
            this.hardpoints = new Dictionary<ulong, SolarComponentHardpoint>();
            
            CreateProperties(arch); // should be first before creating hardpoints
            CreateHardpoints(arch);
        }

        private void CreateHardpoints(ArchSolarComponent arch)
        {
            SolarComponentPropertyRigidHull hull = GetProperty<SolarComponentPropertyRigidHull>();

            if (hull == null)
                return;

            foreach (ArchComponentHardpoint hp in arch.hardpoints)
            {
                SolarComponentHardpoint hardpoint = new SolarComponentHardpoint(hp, hull);

                hardpoints.Add(hardpoint.id, hardpoint);
            }
        }

        private void CreateProperties(ArchSolarComponent arch)
        {
            foreach (ArchSolarComponentProperty prop in arch.properties)
                AddProperty(prop);
        }

        public T GetProperty<T>() where T : SolarComponentProperty
        {
            SolarComponentProperty p = null;

            if (properties.TryGetValue(typeof(T), out p))
                return p as T;

            return null;
        }

        public bool AddProperty(ArchSolarComponentProperty arch)
        {
            Type archType = arch.GetType();

            if (archType == typeof(ArchSolarComponentPropertyRigidHull))
            {
                SolarComponentPropertyRigidHull hull = new SolarComponentPropertyRigidHull(this, arch as ArchSolarComponentPropertyRigidHull);
                properties[typeof(SolarComponentPropertyRigidHull)] = hull;
                return true;
            }

            return false;
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
