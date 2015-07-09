using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Shared
{
    public sealed class PlayerAccountDesc
    {
        public List<CharacterDesc> characters { get; private set; }

        public PlayerAccountDesc()
        {
            characters = new List<CharacterDesc>();
        }
    }
}
