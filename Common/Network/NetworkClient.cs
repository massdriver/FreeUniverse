using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Network
{
    public interface INetworkClientDelegate
    {
        void OnNetworkClientConnect(NetworkClient client, int result);
        void OnNetworkClientDisconnect(NetworkClient client, int reason);
        void OnNetworkClientMessage(NetworkClient client, NetworkMessage message);
    }

    public sealed class NetworkClient
    {
        public INetworkClientDelegate clientDelegate { get; set; }
        private Lidgren.Network.NetClient client { get; set; }
        private Lidgren.Network.NetPeerConfiguration config { get; set; }
        private NetworkMessageFactory msgFactory { get; set; }

        public NetworkClient()
        {
            this.msgFactory = new NetworkMessageFactory();

            this.config = new NetPeerConfiguration("FreeUniverse");
            this.client = new NetClient(this.config);
        }

        public void Start()
        {
            client.Start();
        }

        public void Stop()
        {

            client.Shutdown("bye");
        }

        public NetConnection Connect(string ip, int port)
        {
            return client.Connect(ip, port);
        }

        public void Disconnect()
        {
            client.Disconnect("bye");
        }

        public bool isConnected
        {
            get
            {
                return client != null && client.ConnectionStatus == NetConnectionStatus.Connected;
            }
        }

        public void Update()
        {
            if (client == null)
                throw new Exception("net client object is null");

            NetIncomingMessage msg = null;

            while ((msg = client.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.StatusChanged:
                        {
                            NetConnectionStatus status = (NetConnectionStatus)msg.ReadByte();

                            if (clientDelegate != null)
                            {
                                if (status == NetConnectionStatus.Connected)
                                {
                                    clientDelegate.OnNetworkClientConnect(this, 1);
                                }

                                if (status == NetConnectionStatus.Disconnected)
                                {
                                    clientDelegate.OnNetworkClientDisconnect(this, 1);
                                }
                            }

                        }
                        break;

                    case NetIncomingMessageType.Data:
                        {
                            NetworkMessageType msgid = (NetworkMessageType)msg.PeekUInt16();
                            NetworkMessage flmsg = msgFactory.Create(msgid);

                            if ((clientDelegate != null) && (flmsg != null))
                            {
                                flmsg.Read(msg);
                                clientDelegate.OnNetworkClientMessage(this, flmsg);
                            }
                        }
                        break;

                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.ErrorMessage:
                    default:
                        break;
                }

                client.Recycle(msg);
            }
        }

        public NetSendResult Send(NetworkMessage message, NetDeliveryMethod deliveryType)
        {
            NetOutgoingMessage msg = client.CreateMessage();
            message.Write(msg);
            return client.SendMessage(msg, deliveryType);
        }
    }
}
