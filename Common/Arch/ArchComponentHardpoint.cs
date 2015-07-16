using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Arch
{
    public sealed class ArchComponentHardpoint : ArchObject
    {
        public Transform3D transform { get; set; }
        public string type { get; set; }

        public static readonly string HpName = "hpname";
        public static readonly string HpType = "hptype";
        public static readonly string HpTransform3D = "hptransform3d";

    }
}
