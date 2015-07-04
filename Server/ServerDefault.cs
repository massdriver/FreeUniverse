using FreeUniverse.Common;
using FreeUniverse.Common.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Server
{
    public class ServerDefault :
        IBaseObject,
        INetworkServerDelegate
    {
        protected NetworkMessageHandler msgHandler { get; set; }
        protected FreeUniverseServiceConfiguration configuration { get; set; }
        protected NetworkServer server { get; set; }

        public ServerDefault(FreeUniverseServiceConfiguration configuration)
        {
            this.configuration = configuration;

            msgHandler = new NetworkMessageHandler();
            server = new NetworkServer();
            server.serverDelegate = this;

            SetupMessageHandlers();
        }

        protected virtual void SetupMessageHandlers()
        {
            
        }

        public virtual void Init()
        {
            
        }

        public virtual void Release()
        {
            msgHandler = null;
            server = null;
            configuration = null;
        }

        public virtual void Update(float time)
        {
            if (server != null)
                server.Update(time);
        }

        public virtual void OnNetworkServerClientConnected(NetworkServer server, int client)
        {
            
        }

        public virtual void OnNetworkServerClientDisconnected(NetworkServer server, int client)
        {
            
        }

        public virtual void OnNetworkServerClientMessage(NetworkServer server, int client, NetworkMessage msg)
        {
            msgHandler.ProcessMessage(msg);
        }
    }
}
