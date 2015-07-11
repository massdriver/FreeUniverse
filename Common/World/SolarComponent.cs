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

        private Dictionary<uint, SolarComponentHardpoint> hardpoints { get; set; }
        private Dictionary<Type, SolarComponentProperty> properties { get; set; }

        public SolarComponent(Solar solar, ArchSolarComponent arch)
        {
            this.solar = solar;
            this.properties = new Dictionary<Type, SolarComponentProperty>();
            this.hardpoints = new Dictionary<uint, SolarComponentHardpoint>();
        }

        public T GetProperty<T>() where T : SolarComponentProperty
        {
            SolarComponentProperty p = null;

            if (properties.TryGetValue(typeof(T), out p))
                return p as T;

            return null;
        }

        public T AddProperty<T>(ArchSolarComponentProperty arch) where T : SolarComponentProperty
        {
            Type archType = arch.GetType();

            if (archType == typeof(ArchSolarComponentPropertyHull))
            {
                SolarComponentPropertyHull hull = new SolarComponentPropertyHull(this, arch as ArchSolarComponentPropertyHull);
                properties[typeof(SolarComponentPropertyHull)] = hull;
                return hull as T;
            }

            return null;
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
