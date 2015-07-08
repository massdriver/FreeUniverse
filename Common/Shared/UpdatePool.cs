using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Shared
{
    // MH: unity calls behaviour update calls through reflection
    // imagine calling thousands of reflection shit
    public static class UpdatePool
    {
        private static List<IBaseObject> objects = new List<IBaseObject>();

        public static void Add(IBaseObject obj)
        {
            objects.Add(obj);
        }

        public static void Remove(IBaseObject obj)
        {
            objects.Remove(obj);
        }

        public static void Update()
        {
            float dt = Time.deltaTime;

            foreach (IBaseObject obj in objects)
                obj.Update(dt);
        }

        public static void Release()
        {
            objects.Clear();
        }
    }
}
