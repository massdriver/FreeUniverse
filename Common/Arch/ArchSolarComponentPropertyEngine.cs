using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Arch
{
    public sealed class ArchSolarComponentPropertyEngine : ArchSolarComponentProperty
    {
        [FieldCopy]
        public float forceForward { get; set; }

        [FieldCopy]
        public float forceBackward { get; set; }

        [FieldCopy]
        public float forceStrafeVertical { get; set; }

        [FieldCopy]
        public float forceStrafeHorizontal { get; set; }

        [FieldCopy]
        public float torqueYaw { get; set; }

        [FieldCopy]
        public float torquePitch { get; set; }

        [FieldCopy]
        public float torqueRoll { get; set; }

        [FieldCopy]
        public float maxLinearVelocity { get; set; }

        [FieldCopy]
        public float maxAngularVelocity { get; set; }

        [FieldCopy]
        public float forceModifier { get; set; }

        [FieldCopy]
        public bool useGlobalVelocityLimitMod { get; set; }
    }
}
