using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Unity.Solar
{
    public class PropertyCapacitor : ComponentProperty
    {
        [Tooltip("Overall power stored")]
        public float capacity = 1.0f;
        [Tooltip("Input power will be modified by this")]
        public float chargeModifier = 1.0f;
    }
}
