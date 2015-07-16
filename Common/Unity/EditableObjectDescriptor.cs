using FreeUniverse.Common.Arch;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace FreeUniverse.Common.Unity
{
    // matches ArchObject
    public class EditableObjectDescriptor : MonoBehaviour
    {
        [FieldCopy]
        public string nickname;
        [FieldCopy]
        public string idsInfo;
        [FieldCopy]
        public string idsObjectName;
        [FieldCopy]
        public string idsDescription;

        public virtual void PrepareForCopy()
        {

        }
    }
}
