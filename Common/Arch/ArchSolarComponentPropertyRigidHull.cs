using FreeUniverse.Common.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Arch
{

    public sealed class ArchSolarComponentPropertyRigidHull : ArchSolarComponentProperty
    {
        public ResistanceStats resistanceStats { get; set; }
        public string prefabPath { get; set; }

        public ArchSolarComponentPropertyRigidHull()
        {
            resistanceStats = new ResistanceStats();
        }

        public override void ReadParameter(INIReaderParameter parameter)
        {
            base.ReadParameter(parameter);

            if (parameter.Check("resistance"))
            {
                ResistanceStats.ResistanceType resType = ResistanceStats.FromString(parameter.GetString(0));

                if( resType != ResistanceStats.ResistanceType.Null && resType != ResistanceStats.ResistanceType.MaxTypes )
                {
                    resistanceStats[resType] = parameter.GetFloat(1);
                }
            }
            else if (parameter.Check("prefab_path"))
            {
                prefabPath = parameter.GetString(0);
            }
        }
    }
}
