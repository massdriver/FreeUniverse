using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FreeUniverse.Common
{
    public static class ServerObjectFactoryLoader
    {
        public static readonly string DEFAULT_SERVER_ASSEMBLY_NAME = "FreeUniverse.Server";
        public static readonly string DEFAULT_SERVER_FACTORY = "FreeUniverse.Server.ServerObjectFactory";

        public static IServerObjectFactory Load()
        {
            return Assist.CreateInstanceFromAssembly<IServerObjectFactory>(DEFAULT_SERVER_ASSEMBLY_NAME, DEFAULT_SERVER_FACTORY);
        }
    }
}
