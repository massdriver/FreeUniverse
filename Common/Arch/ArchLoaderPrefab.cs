using FreeUniverse.Common.Arch;
using FreeUniverse.Common.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Arch
{
    public sealed class ArchLoaderPrefab<ComponentType, ArchType>
        where ComponentType : EditableArchDescriptor
        where ArchType : ArchObject, new()
    {
        private ArchLoaderPrefab(string path)
        {
            descriptors = new Dictionary<ulong, ArchType>();

            GameObject[] assets = Resources.LoadAll<GameObject>(path);

            foreach (GameObject obj in assets)
            {
                LoadArchFromGameObject(obj);

                // MH: release here since many arches may eat a lot of memory
                //Resources.UnloadUnusedAssets();
            }
        }

        private void LoadArchFromGameObject(GameObject obj)
        {
            ComponentType ct = obj.GetComponent<ComponentType>();

            if (ct == null)
                return;

            ArchType archObj = ct.ToArchObject() as ArchType;
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
