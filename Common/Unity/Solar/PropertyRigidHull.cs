using FreeUniverse.Common.Arch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
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

        private string GetVisualMeshPath()
        {
            MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();

            if (meshFilter == null || meshFilter.mesh == null )
                throw new Exception("No mesh was assigned to component root");

            return AssetDatabase.GetAssetPath(meshFilter.mesh);
        }
    }
}
