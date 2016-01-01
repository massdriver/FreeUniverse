using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Shared
{
    public sealed class ResistanceStats
    {
        private float[] resistanceMods { get; set; }

        public enum ResistanceType
        {
            Null,
            Energy,
            Kinetic,
            Emp,
            DarkMatter,
            MaxTypes
        }

        public ResistanceStats()
        {
            this.resistanceMods = new float[(int)ResistanceType.MaxTypes];
        }

        public float this[ResistanceType type]
        {
            get
            {
                return resistanceMods[(int)type];
            }
            set
            {
                resistanceMods[(int)type] = value;
            }
        }

        public static readonly string RESISTANCE_ENERGY = "energy";
        public static readonly string RESISTANCE_KINETIC = "kinetic";
        public static readonly string RESISTANCE_EMP = "emp";
        public static readonly string RESISTANCE_DARKMATTER = "darkmatter";

        public static ResistanceType FromString(string str)
        {
            if (str.CompareTo(RESISTANCE_ENERGY) == 0)
                return ResistanceType.Energy;

            if (str.CompareTo(RESISTANCE_KINETIC) == 0)
                return ResistanceType.Kinetic;

            if (str.CompareTo(RESISTANCE_DARKMATTER) == 0)
                return ResistanceType.DarkMatter;

            if (str.CompareTo(RESISTANCE_EMP) == 0)
                return ResistanceType.Emp;

            return ResistanceType.Null;
        }
    }
}
