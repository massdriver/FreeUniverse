using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Procedural
{

    public class StarSystemNameGenerator
    {
        private string[] parts { get; set; }

        public StarSystemNameGenerator(string[] parts)
        {
            this.parts = parts;
        }

        public string Generate(uint seed)
        {
            FastRandom.SetSeed(seed);

            uint id = FastRandom.Next(9000);

            return parts[FastRandom.Next((uint)parts.Length)] + "-" + id;
        }
    }
}
