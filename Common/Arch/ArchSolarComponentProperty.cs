using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Arch
{
    public class ArchSolarComponentProperty : ArchObject
    {
        public float mass { get; set; }
        public bool swapable { get; set; }

        public override void ReadHeader(INIReaderHeader header)
        {
            foreach (INIReaderParameter p in header.parameters)
                ReadParameter(p);
        }

        public override void ReadParameter(INIReaderParameter parameter)
        {
            base.ReadParameter(parameter);

            if (parameter.Check("swappable"))
            {
                swapable = parameter.GetBool(0);
            }
            else if (parameter.Check("mass"))
            {
                mass = parameter.GetFloat(0);
            }
        }
    }
}
