using FreeUniverse.Common.Unity.Solar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Arch
{
    public sealed class ArchManager
    {
        private ArchObject[] indexedArches { get; set; }
        
        private Dictionary<ulong, ArchFaction> factions { get; set; }
        private Dictionary<ulong, ArchSolar> solars { get; set; }

        public static ArchManager instance { get; private set; }

        public ArchManager()
        {
            solars = new Dictionary<ulong, ArchSolar>();
            factions = new Dictionary<ulong, ArchFaction>();
            instance = this;
        }

        public T GetArchByIndex<T>(uint index) where T : ArchObject
        {
            return indexedArches[index] as T;
        }

        public T GetArch<T>(string nickname) where T : ArchObject
        {
            return GetArch<T>(Hash.FromString64(nickname));
        }

        public T GetArch<T>(ulong id) where T : ArchObject
        {
            Type type = typeof(T);

            if (type == typeof(ArchSolar))
            {
                ArchSolar solar = null;

                solars.TryGetValue(id, out solar);

                return solar as T;
            }

            return default(T);
        }

        public void LoadArchesFromPrefabs()
        {
            solars = ArchLoaderPrefab<DescriptorSolar, ArchSolar>.Load("Arch/Solars");

            Debug.Log("solar arches: " + solars.Count);

            ComputeIndices();
        }

        private void ComputeIndices()
        {
            uint index = 0;

            index = SetIndices<ArchSolar>(index, solars);
        }

        private uint SetIndices<T>(uint baseIndex, Dictionary<ulong, T> arches) where T : ArchObject
        {
            uint currentIndex = baseIndex;

            foreach (KeyValuePair<ulong, T> kp in arches)
            {
                kp.Value.index = currentIndex;
                currentIndex++;
            }

            return currentIndex;
        }
    }
}
