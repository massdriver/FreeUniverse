﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Unity.Solar
{
    public class ComponentHardpoint : MonoBehaviour
    {
        [FieldCopy]
        public string hardpointType = "hp_any";

        [FieldCopy]
        public Transform3D hardpointTransform;

        public virtual void PrepareForCopy()
        {

        }
    }
}
