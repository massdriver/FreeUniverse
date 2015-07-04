using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Network
{
    public interface INetworkServerDelegate
    {
        void OnNetworkServerClientConnected(NetworkServer server, int client);
        void OnNetworkServerClientDisconnected(NetworkServer server, int client);
        void OnNetworkServerClientMessage(NetworkServer server, int client, NetworkMessage msg);
    }

    public class NetworkServer
    {
        public INetworkServerDelegate serverDelegate { get; set; }
        private Lidgren.Network.NetServer server { get; set; }
        private Lidgren.Network.NetPeerConfiguration config { get; set; }
        public int maxClients { get; private set; }
        public int port { get; private set; }
        private Stack<int> clientIDStack { get; set; }
        private ClientData[] clientData { get; set; }
        private NetworkMessageFactory msgFactory { get; set; }

        private class ClientData
        {
            public int id;
            public NetConnection sc;

            public ClientData(int id, NetConnection sc)
            {
                this.id = id;
                this.sc = sc;
            }
        }

        public NetworkServer()
        {
            this.msgFactory = new NetworkMessageFactory();
        }

        public void Start(int port, int maxClients)
        {
            if (server != null)
                return;

            this.port = port;
            this.maxClients = maxClients;

            this.clientIDStack = new Stack<int>();
            this.clientData = new ClientData[maxClients];

            for (int i = 0; i < this.maxClients; i++)
                clientIDStack.Push(i);

            this.config = new NetPeerConfiguration("FreeUniverse");
            this.config.Port = port;
            this.config.MaximumConnections = maxClients;

            this.server = new NetServer(config);

            this.server.Start();
        }

        public void Stop()
        {
            server.Shutdown(null);
            server = null;
        }

        public static readonly int ALL_CLIENTS = -1;

        public void Send(int targetClient, NetworkMessage msg, NetDeliveryMethod deliveryType)
        {
            NetOutgoingMessage msgOut = server.CreateMessage();
            msg.Write(msgOut);

            if (targetClient == ALL_CLIENTS)
                server.SendToAll(msgOut, deliveryType);
            else
            {
                server.SendMessage(msgOut, clientData[targetClient].sc, deliveryType);
            }
        }

        public void Update(float time)
        {
            NetIncomingMessage im;
            while ((im = server.ReadMessage()) != null)
            {

                switch (im.MessageType)
                {
                    case NetIncomingMessageType.DebugMessage:
                        break;
                    case NetIncomingMessageType.ErrorMessage:
                        break;
                    case NetIncomingMessageType.WarningMessage:
                        break;
                    case NetIncomingMessageType.VerboseDebugMessage:
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus status = (NetConnectionStatus)im.ReadByte();

                        if (status == NetConnectionStatus.Connected)
                        {
                            int newClient = clientIDStack.Pop();

                            clientData[newClient] = new ClientData(newClient, im.SenderConnection);
                            im.SenderConnection.Tag = clientData[newClient];

                            if (serverDelegate != null)
                                serverDelegate.OnNetworkServerClientConnected(this, newClient);
                        }

                        if (status == NetConnectionStatus.Disconnected)
                        {
                            int clientID = ((ClientData)im.SenderConnection.Tag).id;

                            if (serverDelegate != null)
                                serverDelegate.OnNetworkServerClientDisconnected(this, clientID);

                            clientData[clientID] = null;
                            clientIDStack.Push(clientID);
                        }

                        break;
                    case NetIncomingMessageType.Data:
                        {
                            int clientID = ((ClientData)im.SenderConnection.Tag).id;

                            NetworkMessageType msgid = (NetworkMessageType)im.PeekUInt16();

                            NetworkMessage flmsg = msgFactory.Create(msgid);

                            if ((serverDelegate != null) && (flmsg != null))
                            {
                                flmsg.Read(im);
                                flmsg.source = clientID;
                                serverDelegate.OnNetworkServerClientMessage(this, clientID, flmsg);
                            }

                        }
                        break;
                    default:
                        break;
                }

                server.Recycle(im);
            }

        }
    }
}
