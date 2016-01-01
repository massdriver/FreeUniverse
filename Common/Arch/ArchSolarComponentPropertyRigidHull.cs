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
            if (parameter == "resistance")
            {
                ResistanceStats.ResistanceType resType = ResistanceStats.FromString(parameter.Get<string>(0));

                if (resType != ResistanceStats.ResistanceType.Null && resType != ResistanceStats.ResistanceType.MaxTypes)
                    resistanceStats[resType] = parameter.Get<float>(1);
            }
            else if (parameter == "prefab_path")
                prefabPath = parameter.Get<string>(0);
            else
                base.ReadParameter(parameter);
        }
    }
}
