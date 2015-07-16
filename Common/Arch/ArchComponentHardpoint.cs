using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Arch
{
    public sealed class ArchComponentHardpoint : ArchObject
    {
        [FieldCopy]
        public Transform3D hardpointTransform { get; set; }
        public string hardpointType { get; set; }

    }
}
