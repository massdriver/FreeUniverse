using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Arch
{
    public class ArchSolarComponentPropertyHull : ArchSolarComponentProperty
    {
        public float linearDrag { get; private set; }
        public float angularDrag { get; private set; }
        public float hitPoints { get; private set; }
        public bool staticHull { get; private set; }
        public bool collideWithWorld { get; private set; }
        public bool useCustomColliders { get; private set; }
        public string assetPath { get; private set; }

        public static readonly string LinearDrag = "linearDrag";
        public static readonly string AngularDrag = "angularDrag";
        public static readonly string HitPoints = "hitPoints";
        public static readonly string StaticHull = "staticHull";
        public static readonly string CollideWithWorld = "collideWithWorld";
        public static readonly string UseCustomColliders = "useCustomColliders";

        public static readonly string AssetPath = "assetPath";

        public ArchSolarComponentPropertyHull()
        {

        }

        public override void ReadFromValueMap(ValueMap pmap)
        {
            base.ReadFromValueMap(pmap);

            linearDrag = pmap[LinearDrag];
            angularDrag = pmap[AngularDrag];
            hitPoints = pmap[HitPoints];
            staticHull = pmap[StaticHull];
            collideWithWorld = pmap[CollideWithWorld];
            useCustomColliders = pmap[UseCustomColliders];
            assetPath = pmap[AssetPath];
        }

        public override ValueMap WriteToValueMap()
        {
            ValueMap pmap = base.WriteToValueMap();

            pmap[LinearDrag] = linearDrag;
            pmap[AngularDrag] = angularDrag;
            pmap[HitPoints] = hitPoints;
            pmap[StaticHull] = staticHull;
            pmap[CollideWithWorld] = collideWithWorld;
            pmap[UseCustomColliders] = useCustomColliders;
            pmap[AssetPath] = assetPath;

            return pmap;
        }
    }
}
