using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common
{
    public interface IStateSerializable
    {
        ValueMap GetState();
        void SetState(ValueMap map);
    }
}
