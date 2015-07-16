using FreeUniverse.Common.Arch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace FreeUniverse.Common.Unity.Solar
{
    [FieldCopyReminder(typeof(ArchSolarComponentPropertyRigidHull))]
    public sealed class PropertyRigidHull : ComponentProperty
    {
        [FieldCopy]
        public float linearDrag = 0.1f;

        [FieldCopy]
        public float angularDrag = 0.1f;

        [FieldCopy]
        [Tooltip("Zero or less means that hull is indestructile")]
        public float hitPoints = 1.0f;

        [FieldCopy]
        public bool staticHull = false;

        [FieldCopy]
        [Tooltip("Enable collision with other world objects and projectiles")]
        public bool collideWithWorld = true;

        [FieldCopy]
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
