using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
