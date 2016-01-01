using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Arch
{
    public sealed class ArchComponentHardpoint : ArchObject
    {
        public static readonly ulong HP_TYPE_ANY = Hash.FromString64("hp_type_any");

        public ulong hptype { get; set; }
        public Vector3 position { get; set; }
        public Vector3 rotation { get; set; }
        public Vector2 viewAngles { get; set; }

        public override void ReadHeader(INIReaderHeader header)
        {
            hptype = HP_TYPE_ANY;
        }

        public override void ReadParameter(INIReaderParameter parameter)
        {
            id = parameter.GetStrkey64(0);
            nickname = parameter.GetString(0);
            hptype = parameter.GetStrkey64(1);
            position = new Vector3(parameter.GetFloat(2), parameter.GetFloat(3), parameter.GetFloat(4));
            rotation = new Vector3(parameter.GetFloat(5), parameter.GetFloat(6), parameter.GetFloat(7));
            viewAngles = new Vector2(parameter.GetFloat(8), parameter.GetFloat(9));
        }

    }
}
