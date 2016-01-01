using FreeUniverse.Common.Arch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.World
{
    

    public class Projectile
    {
        public ArchProjectile arch { get; set; }

        public Projectile(ArchProjectile arch)
        {
            this.arch = arch;

            CreateProjectile();
        }

        private void CreateProjectile()
        {

        }

        public void OnInstantiate()
        {

        }

        public void OnDispose()
        {

        }

        public void SetupLaunchParameters()
        {

        }
    }

    public class ProjectilePool
    {
        private class Pool
        {
            private ArchProjectile projectileArch { get; set; }
            private Stack<Projectile> pooledProjectiles { get; set; }

            public Pool(ArchProjectile arch)
            {
                this.projectileArch = arch;
                this.pooledProjectiles = new Stack<Projectile>();
            }

            public Projectile Instantiate()
            {
                if (pooledProjectiles.Count == 0)
                    return new Projectile(projectileArch);

                Projectile p = pooledProjectiles.Pop();
                p.OnInstantiate();
                return p;
            }

            public void Dispose(Projectile projectile)
            {
                projectile.OnDispose();
                pooledProjectiles.Push(projectile);
            }
        }

        private Pool[] projectilePools { get; set; }

        public ProjectilePool()
        {
            projectilePools = new Pool[Arch.ArchManagerIndexed.GetNumArchesOfType<ArchProjectile>()];
        }

        public Projectile Instantiate(int archIndex)
        {
            return GetPool(archIndex).Instantiate();
        }

        public Projectile Instantiate(ArchProjectile arch)
        {
            return GetPool(arch.index).Instantiate();
        }

        public void Dispose(Projectile projectile)
        {
            GetPool(projectile.arch.index).Dispose(projectile);
        }

        private Pool GetPool(int archIndex)
        {
            if (projectilePools[archIndex] == null)
                projectilePools[archIndex] = new Pool(ArchManagerIndexed.GetByIndex<ArchProjectile>(archIndex));

            return projectilePools[archIndex];
        }
    }
}
