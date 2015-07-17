using FreeUniverse.Common.Arch;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace FreeUniverse.Common.Unity
{
    public class DescriptorArch : MonoBehaviour
    {
        [FieldCopy]
        public string nickname;
        [FieldCopy]
        public string idsInfo;
        [FieldCopy]
        public string idsObjectName;
        [FieldCopy]
        public string idsDescription;
    }

    // matches ArchObject
    public class EditableArchDescriptor : DescriptorArch
    {
        public virtual ArchObject ToArchObject()
        {
            return null;
        }

    }
}
