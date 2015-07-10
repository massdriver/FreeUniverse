using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Shared
{
    public sealed class PlayerAccountDesc : IBinarySerializable
    {
        public List<CharacterDesc> characters { get; private set; }

        public PlayerAccountDesc()
        {
            characters = new List<CharacterDesc>();
        }

        public void Write(System.IO.BinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public void Read(System.IO.BinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
