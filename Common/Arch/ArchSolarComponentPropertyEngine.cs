using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Arch
{
    public sealed class ArchSolarComponentPropertyEngine : ArchSolarComponentProperty
    {
        public Vector3 torque { get; private set; }
        public Vector3 forcePositive { get; private set; }
        public Vector3 forceNegative { get; private set; }

        public ArchSolarComponentPropertyEngine()
        {

        }

        public override void ReadHeader(INIReaderHeader header)
        {
            foreach (INIReaderParameter param in header.parameters)
            {
                if (param == "torque")
                    torque = param.Get<Vector3>(0);
                else if (param == "force_positive")
                    forcePositive = param.Get<Vector3>(0);
                else if (param == "force_negative")
                    forceNegative = param.Get<Vector3>(0);
                else
                    base.ReadParameter(param);
            }
        }
    }
}
