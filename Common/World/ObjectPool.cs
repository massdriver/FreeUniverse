using FreeUniverse.Common.Arch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.World
{
    public abstract class ObjectPoolEntity<T> where T : Arch.ArchObject
    {
        public T arch { get; private set; }

        public ObjectPoolEntity()
        {
            
        }

        // MH: called on construction of object to load resources etc (no more pooled objects found)
        public abstract void CreateObjectPoolEntity(T arch);

        // Called when object is going to be reused from pool
        public abstract void OnInstantiate();

        // Called when object will be put in pool
        public abstract void OnDispose();
    }

    public class ObjectPool<ArchType, PooledType>
        where ArchType : Arch.ArchObject
        where PooledType : ObjectPoolEntity<ArchType>, new()
    {
        private class Pool
        {
            private ArchType pooledArchType { get; set; }
            private Stack<PooledType> pooledProjectiles { get; set; }

            public Pool(ArchType arch)
            {
                this.pooledArchType = arch;
                this.pooledProjectiles = new Stack<PooledType>();
            }

            public PooledType Instantiate()
            {
                if (pooledProjectiles.Count == 0)
                {
                    PooledType pobj = new PooledType();
                    pobj.CreateObjectPoolEntity(pooledArchType);
                    pobj.OnInstantiate();
                    return pobj;
                }

                PooledType p = pooledProjectiles.Pop();
                p.OnInstantiate();
                return p;
            }

            public void Dispose(PooledType projectile)
            {
                projectile.OnDispose();
                pooledProjectiles.Push(projectile);
            }
        }

        private Pool[] projectilePools { get; set; }

        public ObjectPool()
        {
            projectilePools = new Pool[Arch.ArchManagerIndexed.numProjectiles];
        }

        public PooledType Instantiate(int archIndex)
        {
            return GetPool(archIndex).Instantiate();
        }

        public PooledType Instantiate(ArchType arch)
        {
            return GetPool(arch.index).Instantiate();
        }

        public void Dispose(PooledType projectile)
        {
            GetPool(projectile.arch.index).Dispose(projectile);
        }

        private Pool GetPool(int archIndex)
        {
            if (projectilePools[archIndex] == null)
                projectilePools[archIndex] = new Pool(ArchManagerIndexed.GetByIndex<ArchType>(archIndex));

            return projectilePools[archIndex];
        }
    }
}
