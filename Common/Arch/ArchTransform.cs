using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Arch
{
    // MH: this one is required for json export since json exports properties too and unity vector properties are somehat useless for export
    public sealed class ArchTransform
    {
        public float xPos { get; set; }
        public float yPos { get; set; }
        public float zPos { get; set; }
        public float xRot { get; set; }
        public float yRot { get; set; }
        public float zRot { get; set; }
        public float wRot { get; set; }
        public float xScl { get; set; }
        public float yScl { get; set; }
        public float zScl { get; set; }

        public ArchTransform()
        {

        }

        public ArchTransform(Transform transform)
        {
            xPos = transform.position.x;
            yPos = transform.position.y;
            zPos = transform.position.z;

            xScl = transform.localScale.x;
            yScl = transform.localScale.y;
            zScl = transform.localScale.z;

            xRot = transform.rotation.x;
            yRot = transform.rotation.y;
            zRot = transform.rotation.z;
            wRot = transform.rotation.w;
        }
    }
}
