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

        public float hitPoints = 1.0f;
        public float resistanceKinetic = 0.0f;
        public float resistanceDarkMatter = 0.0f;
        public float resistanceEMP = 0.0f;
        public float resistanceEnergy = 0.0f;

        public bool collideWithWorld = true;

        public Mesh hullMesh;
    }
}
