using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Unity.Solar
{
    public class PropertyEngine : ComponentProperty
    {
        public float forceForward = 1.0f;
        public float forceBackward = 1.0f;
        public float forceStrafeVertical = 1.0f;
        public float forceStrafeHorizontal = 1.0f;

        public float torqueYaw = 1.0f;
        public float torquePitch = 1.0f;
        public float torqueRoll = 1.0f;

        public float maxLinearVelocity = 0.0f;
        public float maxAngularVelocity = 0.0f;

        [Tooltip("Multiply forces by this")]
        public float forceModifier = 1.0f;

        public bool useGlobalVelocityLimitMod = false;
    }
}
