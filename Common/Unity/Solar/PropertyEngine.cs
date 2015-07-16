using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Unity.Solar
{
    public class PropertyEngine : ComponentProperty
    {
        [FieldCopy]
        public float forceForward = 1.0f;

        [FieldCopy]
        public float forceBackward = 1.0f;

        [FieldCopy]
        public float forceStrafeVertical = 1.0f;

        [FieldCopy]
        public float forceStrafeHorizontal = 1.0f;

        [FieldCopy]
        public float torqueYaw = 1.0f;

        [FieldCopy]
        public float torquePitch = 1.0f;

        [FieldCopy]
        public float torqueRoll = 1.0f;

        [FieldCopy]
        public float maxLinearVelocity = 0.0f;

        [FieldCopy]
        public float maxAngularVelocity = 0.0f;

        [FieldCopy]
        [Tooltip("Multiply forces by this")]
        public float forceModifier = 1.0f;

        [FieldCopy]
        public bool useGlobalVelocityLimitMod = false;
    }
}
