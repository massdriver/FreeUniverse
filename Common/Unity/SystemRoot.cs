
using FreeUniverse.Common.Arch;
using FreeUniverse.Common.Unity.Solar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace FreeUniverse.Common.Unity
{
    // MH:
    // 1) SystemRoot may contain a number of game objects with DescriptorSolar, DescriptorField properties, all other are ignored on export
    // 2) it doesnt export actual arches but only transforms and nicknames of child solars
    [FieldCopyTarget(typeof(ArchSystem))]
    public sealed class SystemRoot : EditableArchDescriptor
    {
        [FieldCopy]
        public string backgroundMusic;

        [FieldCopy]
        public List<ArchSystemEntity> systemEntities { get; set; }

        public override Arch.ArchObject ToArchObject()
        {
            // Process solars
            {
                systemEntities = new List<ArchSystemEntity>();

                foreach (Transform tr in gameObject.transform)
                {
                    DescriptorSolar solar = tr.gameObject.GetComponent<DescriptorSolar>();

                    if (solar == null)
                        continue;

                    ArchSystemEntity entity = new ArchSystemEntity();
                    entity.solarName = tr.gameObject.name;
                    entity.solarArch = solar.nickname;
                    entity.solarTransform = new ArchTransform(tr.gameObject.transform);

                    systemEntities.Add(entity);
                }

            }

            return ArchObject.Convert<SystemRoot, ArchSystem>(this);
        }
    }
}
