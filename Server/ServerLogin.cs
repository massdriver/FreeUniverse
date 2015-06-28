using FreeUniverse.Common;
using FreeUniverse.Common.Network;
using FreeUniverse.Common.Network.Messages;
using FreeUniverse.Server.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Server
{
    internal class ServerLogin : ServerDefault
    {
        private ClientAccountDatabase accountDatabase { get; set; }

        public ServerLogin(FreeUniverseServiceConfiguration config) : base(config)
        {
            accountDatabase = new ClientAccountDatabase(config);
        }

        private void HandleRequestAccountLogin(NetworkMessage msg)
        {

        }

        private void HandleRequestAccountLogout(NetworkMessage msg)
        {

        }

        /* Create account */
        private void HandleRequestCreateAccount(NetworkMessage msg)
        {
            MsgRequestCreateAccount message = msg as MsgRequestCreateAccount;

            string user = message[MsgRequestCreateAccount.FIELD_USERNAME];
            string pass = message[MsgRequestCreateAccount.FIELD_PASSWORD];

            bool result = accountDatabase.CreateNewAccount(user, pass);

            MsgReplyCreateAccount reply = new MsgReplyCreateAccount();

            if (result)
                reply[MsgReplyCreateAccount.FIELD_RESULT] = MsgReplyCreateAccount.ACCOUNT_CREATED;
            else
                reply[MsgReplyCreateAccount.FIELD_RESULT] = MsgReplyCreateAccount.ACCOUNT_NOT_CREATED;

            server.Send(msg.source, reply, Lidgren.Network.NetDeliveryMethod.ReliableOrdered);
        }

        protected override void SetupMessageHandlers()
        {
            base.SetupMessageHandlers();

            msgHandler[NetworkMessageType.ReplyCreateAccount] = HandleRequestCreateAccount;
            msgHandler[NetworkMessageType.RequestAccountLogin] = HandleRequestAccountLogin;
            msgHandler[NetworkMessageType.RequestAccountLogout] = HandleRequestAccountLogout;
        }

        public override void Init()
        {
            base.Init();
            Debug.Log("initializing login server, maxClients=" + this.configuration.maxClients + ", port=" + this.configuration.loginServerPort);
            server.Start(this.configuration.maxClients, this.configuration.loginServerPort);
        }

        public override void Release()
        {
            Debug.Log("login server shutdown");
            server.Stop();

            base.Release();
        }

        public override void Update(float time)
        {
            base.Update(time);
        }

        public override void OnNetworkServerClientConnected(NetworkServer server, int client)
        {
            base.OnNetworkServerClientConnected(server, client);
        }

        public override void OnNetworkServerClientDisconnected(NetworkServer server, int client)
        {
            base.OnNetworkServerClientDisconnected(server, client);
        }

        public override void OnNetworkServerClientMessage(NetworkServer server, int client, NetworkMessage msg)
        {
            base.OnNetworkServerClientMessage(server, client, msg);
        }
    }
}
