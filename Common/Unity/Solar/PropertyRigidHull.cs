using FreeUniverse.Common.Arch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Unity.Solar
{
    public class PropertyRigidHull : ComponentProperty
    {
        public float linearDrag = 0.1f;
        public float angularDrag = 0.1f;

        [Tooltip("Zero or less means that hull is indestructile")]
        public float hitPoints = 1.0f;
        public bool staticHull = false;

        [Tooltip("Enable collision with other world objects and projectiles")]
        public bool collideWithWorld = true;

        [Tooltip("Use child colliders for this rigid or use convex mesh")]
        public bool useCustomColliders = true;

        public override sealed void ReadFromValueMap(ValueMap map)
        {
            throw new NotImplementedException();
        }

        public override sealed ValueMap WriteToValueMap()
        {
            ValueMap pmap = base.WriteToValueMap();

            pmap[ArchSolarComponentPropertyHull.LinearDrag] = linearDrag;
            pmap[ArchSolarComponentPropertyHull.AngularDrag] = angularDrag;
            pmap[ArchSolarComponentPropertyHull.HitPoints] = hitPoints;
            pmap[ArchSolarComponentPropertyHull.StaticHull] = staticHull;
            pmap[ArchSolarComponentPropertyHull.CollideWithWorld] = collideWithWorld;
            pmap[ArchSolarComponentPropertyHull.UseCustomColliders] = useCustomColliders;

            return pmap;
        }
    }
}
