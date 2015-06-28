using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Procedural
{
    public struct ParameterRange
    {
        public float min;
        public float max;

        public ParameterRange( float min, float max )
        {
            this.min = min;
            this.max = max;
        }

        public ParameterRange(int min, int max)
        {
            this.min = (float)min;
            this.max = (float)max;
        }

        public float RollAsFloat()
        {
            return UnityEngine.Random.Range((float)min, (float)max);
        }

        public int RollAsInt()
        {
            return UnityEngine.Random.Range((int)min, (int)max);
        }
    }
}
