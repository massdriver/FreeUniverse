using FreeUniverse.Common.Arch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace FreeUniverse.Common.Unity.Solar
{
    [CustomEditor(typeof(DescriptorSolar))]
    public sealed class DescriptorSolarRootExporter : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Validate"))
            {
                DescriptorSolar solarRoot = (DescriptorSolar)target;

                ArchSolar archSolar = solarRoot.ToArchObject() as ArchSolar;


            }


        }
    }

    [FieldCopyReminder(typeof(ArchSolar))]
    public sealed class DescriptorSolar : EditableArchDescriptor
    {
        [FieldCopy]
        public List<ArchSolarComponent> components { get; set; }

        public override ArchObject ToArchObject()
        {
            // Process components
            {
                components = new List<ArchSolarComponent>();

                foreach (Transform tr in gameObject.transform)
                {
                    DescriptorSolarComponent component = tr.gameObject.GetComponent<DescriptorSolarComponent>();

                    if (component == null)
                        continue;

                    components.Add(component.ToArchObject() as ArchSolarComponent);
                }
            }

            return ArchObject.Convert<DescriptorSolar, ArchSolar>(this);
        }

    }
}
