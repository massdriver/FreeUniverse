using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Procedural.System
{
    public class SystemGeneratorParams
    {
        public ParameterRange starCount;
        public ParameterRange starSize;

        public SystemGeneratorParams()
        {
            starCount = new ParameterRange(1, 3);
            starSize = new ParameterRange(1.0f, 15.0f);
        }
    }
}
