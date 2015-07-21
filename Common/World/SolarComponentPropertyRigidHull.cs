using FreeUniverse.Common.Arch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.World
{
    // MH: rigidbody in world
    public sealed class SolarComponentPropertyRigidHull : SolarComponentProperty
    {
        public float hitPoints { get; private set; }
        public float maxHitPoints { get; private set; }
        public GameObject worldObject { get; private set; }

        public SolarComponentPropertyRigidHull(SolarComponent component, ArchSolarComponentPropertyRigidHull arch)
            : base(component)
        {

        }
    }
}
