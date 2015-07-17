using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Arch
{
    public sealed class ArchSolarComponent : ArchObject
    {
        [FieldCopy]
        public string hardpointConnectionType { get; set; }

        [FieldCopy]
        public bool canAttach { get; set; }

        [FieldCopy]
        public List<ArchComponentHardpoint> hardpoints { get; set; }

        [FieldCopy]
        public List<ArchSolarComponentProperty> properties { get; set; }
    }
}
