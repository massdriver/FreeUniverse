using FreeUniverse.Common;
using System;
using System.Collections.Generic;


namespace FreeUniverse.Server
{
    public class ServerObjectFactory : IServerObjectFactory
    {
        public IBaseObject Create(ServerType type)
        {
            switch (type)
            {
                case ServerType.Login:
                    return new ServerLogin(new FreeUniverseServiceConfiguration());
                    //return new ServerLogin(new FreeUniverseServiceConfiguration(FreeUniverseServiceConfiguration.DEFAULT_CONFIG_FILE_PATH));
            }

            return null;
        }
    }
}
