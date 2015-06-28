using FreeUniverse.Common;
using FreeUniverse.Server.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Server.Plugin
{
    public enum ServerPluginEvent
    {
        PluginLoaded,

        AccountCreated,

        AccountLogin,
        AccountLogout
    }

    public interface IServerPlugin : IBaseObject
    {
        void OnServerPluginEvent(ServerPluginEvent eventType, object data);
    }
}
