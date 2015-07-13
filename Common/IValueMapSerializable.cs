using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common
{
    public interface IValueMapSerializable
    {
        void ReadFromValueMap(ValueMap map);
        ValueMap WriteToValueMap();
    }
}
