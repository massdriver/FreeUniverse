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
        public float xPos;
        public float yPos;
        public float zPos;

        public float xRot;
        public float yRot;
        public float zRot;
        public float wRot;

        public float xScl;
        public float yScl;
        public float zScl;

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

    public sealed class ArchSystemEntity 
    {
        public string solarName { get; set; }
        public string solarArch { get; set; }
        public ArchTransform solarTransform { get; set; }
    }

    public sealed class ArchSystem : ArchObject
    {
        [FieldCopy]
        public List<ArchSystemEntity> systemEntities { get; set; }

        [FieldCopy]
        public string backgroundMusic { get; set; }
    }
}
