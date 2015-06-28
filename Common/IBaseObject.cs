using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common
{
    public interface IBaseObject
    {
        void Init();
        void Release();
        void Update(float time);
    }
}
