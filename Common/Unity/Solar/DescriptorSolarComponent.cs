using FreeUniverse.Common.Arch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Unity.Solar
{
    [FieldCopyReminder(typeof(ArchSolarComponent))]
    public sealed class DescriptorSolarComponent : EditableArchDescriptor
    {
        [FieldCopy]
        public string hardpointConnectionType = "hp_any";

        [FieldCopy]
        public bool canAttach = false;

        [FieldCopy]
        public List<ArchComponentHardpoint> hardpoints { get; set; }

        [FieldCopy]
        public List<ArchSolarComponentProperty> properties { get; set; }

        public override ArchObject ToArchObject()
        {
            hardpoints = GetHardpoints();
            properties = GetProperties();

            return ArchObject.Convert<DescriptorSolarComponent, ArchSolarComponent>(this);
        }

        private T GetArchProperty<D,T>() where T : Arch.ArchSolarComponentProperty where D:ComponentProperty
        {
            D property = gameObject.GetComponent<D>();

            if (property == null)
                return null;

            return property.ToArchProperty() as T;
        }

        private void AddProperty<D, T>(List<ArchSolarComponentProperty> lst)
            where T : Arch.ArchSolarComponentProperty
            where D : ComponentProperty
        {
            T archProperty = GetArchProperty<D, T>();

            if (archProperty == null)
                return;

            lst.Add(archProperty);
        }

        private List<ArchSolarComponentProperty> GetProperties()
        {
            List<ArchSolarComponentProperty> properties = new List<ArchSolarComponentProperty>();

            AddProperty<PropertyRigidHull, ArchSolarComponentPropertyRigidHull>(properties);
            AddProperty<PropertyEngine, ArchSolarComponentPropertyEngine>(properties);

            return properties;
        }

        private List<ArchComponentHardpoint> GetHardpoints()
        {
            List<ArchComponentHardpoint> hps = new List<ArchComponentHardpoint>();

            foreach (Transform tr in gameObject.transform)
            {
                ComponentHardpoint hp = tr.gameObject.GetComponent<ComponentHardpoint>();

                hps.Add(hp.ToArchObject() as ArchComponentHardpoint);
            }

            return hps;
        }
    }
}
