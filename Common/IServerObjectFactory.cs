using System;
using System.Collections.Generic;
using System.Text;

namespace FreeUniverse.Common
{
    public enum ServerType
    {
        Login,
        Chat,
        Zone,
        Master
    }

    public interface IServerObjectFactory
    {
        IBaseObject Create(ServerType type);
    }
}
