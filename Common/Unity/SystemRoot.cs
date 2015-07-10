
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Unity
{
    public sealed class SystemRoot : EditableObjectDescriptor
    {
        public string backgroundMusic;

        public override ValueMap ToValueMap()
        {
            ValueMap pmap = base.ToValueMap();

            pmap["backgroundMusic"] = backgroundMusic;

            return pmap;
        }

    }
}
