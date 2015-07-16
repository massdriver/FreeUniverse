using FreeUniverse.Common.Arch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Unity.Solar
{
    [FieldCopyReminder(typeof(ArchSolarComponent))]
    public sealed class DescriptorSolarComponent : EditableObjectDescriptor
    {
        public string hardpointConnectionType = "hp_any";
    }
}
