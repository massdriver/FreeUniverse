using FreeUniverse.Common.Arch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Shared
{
    public sealed class CharacterDesc : IBinarySerializable
    {
        public string name { get; private set; }
        public UniverseLocation location { get; private set; }
        public CreditAccount creditAccount { get; private set; }
        public CharacterReputation reputation { get; private set; }

        public ValueMap customData { get; private set; }
        
        private List<SolarDesc> characterSolars { get; set; }

        public CharacterDesc()
        {
            customData = new ValueMap();
            characterSolars = new List<SolarDesc>();
        }

        public void SetupFromArch(ArchObject obj)
        {

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
