using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Arch
{
    public sealed class ArchSystem : ArchObject
    {
        public string backgroundMusic { get; set; }
        public string starSphere { get; set; }
        public string musicDefault { get; set; }
        public string musicTension { get; set; }
        public string musicBattle { get; set; }
        public string stars { get; set; }
        public string background { get; set; }
        public Vector4 backgroundColor { get; set; }
        public Dictionary<ulong, ArchSystemSolar> solars { get; set; }
        public Dictionary<ulong, ArchSystemZone> zones { get; set; }

        public ArchSystem()
        {
            solars = new Dictionary<ulong, ArchSystemSolar>();
            zones = new Dictionary<ulong, ArchSystemZone>();
        }

        override public void ReadHeader(INIReaderHeader header)
        {
            if (header.Check("System"))
            {
                foreach (INIReaderParameter p in header.parameters)
                    ReadParameter(p);
            }
            else if (header.Check("Zone"))
                ReadZone(header);
            else if (header.Check("Solar"))
                ReadSolar(header);
        }

        void ReadSolar(INIReaderHeader header)
        {
            ArchSystemSolar solar = new ArchSystemSolar();
            solar.ReadHeader(header);
            solars[solar.id] = solar;
        }

        void ReadZone(INIReaderHeader header)
        {
            ArchSystemZone zone = new ArchSystemZone();
            zone.ReadHeader(header);
            zones[zone.id] = zone;
        }

        override public void ReadParameter(INIReaderParameter parameter)
        {
            base.ReadParameter(parameter);

            if (parameter.Check("music_default"))
            {
                musicDefault = parameter.GetString(0);
            }
            else if (parameter.Check("music_tension"))
            {
                musicTension = parameter.GetString(0);
            }
            else if (parameter.Check("music_battle"))
            {
                musicBattle = parameter.GetString(0);
            }
            else if (parameter.Check("background"))
            {
                background = parameter.GetString(0);
            }
            else if (parameter.Check("stars"))
            {
                stars = parameter.GetString(0);
            }
            else if (parameter.Check("backgroundColor"))
            {
                backgroundColor = parameter.GetVector4(0);
            }
        }
    }
}
