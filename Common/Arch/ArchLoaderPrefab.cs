using FreeUniverse.Common.Arch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Unity
{
    public sealed class ArchLoaderPrefab<ComponentType, ArchType>
        where ComponentType : EditableObjectDescriptor
        where ArchType : ArchObject, new()
    {
        private ArchLoaderPrefab(string path)
        {
            descriptors = new Dictionary<ulong, ArchType>();

            GameObject[] assets = Resources.LoadAll<GameObject>(path);

            foreach (GameObject obj in assets)
                LoadArchFromGameObject(obj);
        }

        private void LoadArchFromGameObject(GameObject obj)
        {
            ComponentType ct = obj.GetComponent<ComponentType>();

            if (ct == null)
                return;

            ArchType archObj = new ArchType();
            archObj.ReadFromValueMap(ct.WriteToValueMap());
            descriptors.Add(archObj.id, archObj);
        }

        private Dictionary<ulong, ArchType> descriptors { get; set; }

        public static Dictionary<ulong, ArchType> Load(string path)
        {
            ArchLoaderPrefab<ComponentType, ArchType> alp = new ArchLoaderPrefab<ComponentType, ArchType>(path);

            return alp.descriptors;
        }
    }
}
