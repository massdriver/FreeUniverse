using FreeUniverse.Common.Arch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace FreeUniverse.Common.Unity.Solar
{
    [FieldCopyTarget(typeof(ArchSolarComponentPropertyRigidHull))]
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

        [FieldCopy]
        public string assetPath;

        public override ArchSolarComponentProperty ToArchProperty()
        {
            assetPath = GetAssetPath();
            return ArchSolarComponentProperty.Convert<PropertyRigidHull, ArchSolarComponentPropertyRigidHull>(this);
        }

        private static readonly string PATH_GUESS = "Assets/Resources/";

        private string GetAssetPath()
        {
            Transform tr = gameObject.transform.GetChild(0);

             if (tr == null)
                throw new Exception("Solar component should contain exactly one child prefab that will be used to represent your object in game visually");

            string fullPath = AssetDatabase.GetAssetPath(PrefabUtility.GetPrefabParent(tr.gameObject));

            // GetAssetPath returns full path in form of Assets/Resources/
            return fullPath.Substring(PATH_GUESS.Length, fullPath.Length - PATH_GUESS.Length);
        }
    }
}
