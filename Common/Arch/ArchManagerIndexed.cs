using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Arch
{
    public sealed class ArchManagerIndexed
    {
        private static IndexArchStorage<ArchSolar> solars { get; set; }
        private static IndexArchStorage<ArchSystem> systems { get; set; }
        private static IndexArchStorage<ArchSolarComponent> components { get; set; }
        private static IndexArchStorage<ArchProjectile> projectiles { get; set; }

        public static void Init()
        {
            solars = new IndexArchStorage<ArchSolar>();
            systems = new IndexArchStorage<ArchSystem>();
            components = new IndexArchStorage<ArchSolarComponent>();
            projectiles = new IndexArchStorage<ArchProjectile>();
        }

        public static T GetByIndex<T>(int index) where T : ArchObject
        {
            if (typeof(T) == typeof(ArchSolar))
                return solars.GetByIndex(index) as T;
            else
            if (typeof(T) == typeof(ArchSolarComponent))
                return components.GetByIndex(index) as T;
            else
            if (typeof(T) == typeof(ArchSystem))
                return systems.GetByIndex(index) as T;
            else
            if (typeof(T) == typeof(ArchProjectile))
                return projectiles.GetByIndex(index) as T;

            return null;
        }

        public static int numProjectiles
        {
            get
            {
                return projectiles.Count;
            }
        }
    }
}
