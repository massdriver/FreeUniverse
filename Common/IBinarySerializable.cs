using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common
{
    public interface IBinarySerializable
    {
        void Write(BinaryWriter writer);
        void Read(BinaryReader reader);
    }
}
