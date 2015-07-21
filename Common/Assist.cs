using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common
{
    public static class Assist
    {
        // Find component in GameObject hierarchy by type
        public static T FindComponent<T>(GameObject root, string child) where T : Component
        {
            Transform childTransform = root.transform.Find(child);

            if (childTransform == null)
                return null;

            return childTransform.GetComponent<T>();
        }

        public static GameObject LoadUI(string prefab)
        {
            GameObject e = UnityEngine.Object.Instantiate(Resources.Load(prefab)) as GameObject;

            // MH: this is required to make UI visible with correct transform
            // make sure that canvas object is properly set before usage
            e.gameObject.transform.SetParent(FreeUniverse.Common.UI.External.canvas.transform, false);

            return e;
        }

        public static T CreateInstanceFromAssembly<T>(string assemblyName, string typeName)
        {
            Assembly assembly = Assembly.Load(assemblyName);

            if (assembly == null)
                return default(T);

            Type type = assembly.GetType(typeName);

            if (type == null)
                return default(T);

            return (T)Activator.CreateInstance(type);
        }

        public static void SetLayer(GameObject obj, int layer)
        {
            foreach (Transform tr in obj.transform)
            {
                tr.gameObject.layer = layer;

                SetLayer(tr.gameObject, layer);
            }
        }
    }
}
