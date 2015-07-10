using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Shared
{
    public sealed class CharacterReputation : IBinarySerializable
    {
        private Dictionary<ulong, float> reputation { get; set; }

        public CharacterReputation()
        {
            reputation = new Dictionary<ulong, float>();
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
