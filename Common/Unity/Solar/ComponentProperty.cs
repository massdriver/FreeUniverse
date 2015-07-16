using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Unity.Solar
{
    public class ComponentProperty : MonoBehaviour
    {
        [FieldCopy]
        public float mass = 1.0f;
        [FieldCopy]
        public bool swapable = false;
    }
}
