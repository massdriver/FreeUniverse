using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Shared
{
    public sealed class CharacterDesc
    {
        public string name { get; private set; }
        public ulong credits { get; private set; }
        private List<SolarDesc> characterSolars { get; set; }

        public CharacterDesc()
        {

        }
    }
}
