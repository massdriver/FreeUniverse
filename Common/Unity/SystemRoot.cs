
using FreeUniverse.Common.Arch;
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
    [FieldCopyTarget(typeof(ArchSystem))]
    public sealed class SystemRoot : EditableArchDescriptor
    {
        public string backgroundMusic;

        // MH: scale is saved from solar root transform
        //
        // System root may contain a number of game objects with DescriptorSolar, DescriptorField properties, all other are ignored on export
        public override Arch.ArchObject ToArchObject()
        {
            return base.ToArchObject();
        }
    }
}
