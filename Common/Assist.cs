﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common
{
    public class Assist
    {
        // Find UI component in GameObject hierarchy
        public static T FindUI<T>(GameObject root, string child) where T : Component
        {
            Transform childTransform = root.transform.Find(child);

            if (childTransform == null)
                return null;

            return childTransform.GetComponent<T>();
        }


        public static GameObject LoadUI(string prefab)
        {
            GameObject e = UnityEngine.Object.Instantiate(Resources.Load(prefab)) as GameObject;

            e.gameObject.transform.SetParent(FreeUniverse.Common.UI.External.canvas.transform, false); // MH: this is required to make UI visible with correct transform

            return e;
        }

        public static T CreateInstanceFromAssembly<T>(string assemblyName, string typeName) where T: class
        {
            Assembly assembly = Assembly.Load(assemblyName);

            if (assembly == null)
                return default(T);

            Type type = assembly.GetType(typeName);

            if (type == null)
                return default(T);

            return Activator.CreateInstance(type) as T;
        }
    }
}