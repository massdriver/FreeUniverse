using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Arch
{
    public class ArchSystemZone : ArchObject
    {
        public enum ZoneShapeType
        {
            Sphere,
            Box
        }

        public ZoneShapeType shapeType { get; set; }
        public Vector3 position { get; set; }
        public Vector3 size { get; set; }
        public uint seed { get; set; }
        public ulong zoneArch { get; set; }
    
        ZoneShapeType FromString(string str)
        {
            return ZoneShapeType.Sphere;
        }

        public override void ReadHeader(INIReaderHeader header)
        {
            base.ReadHeader(header);

            foreach (INIReaderParameter p in header.parameters)
            {
                if (p.Check("position"))
                {
                    position = p.GetVector3(0);
                    continue;
                }
                else if (p.Check("size"))
                {
                    size = p.GetVector3(0);
                    continue;
                }
                else if (p.Check("seed"))
                {
                    seed = (uint)p.GetInt(0);
                    continue;
                }
                else if (p.Check("shape"))
                {
                    shapeType = FromString(p.GetString(0));
                    continue;
                }
                else if (p.Check("zone_arch"))
                {
                    zoneArch = p.GetStrkey64(0);
                    continue;
                }
            }
        }
    }
}
