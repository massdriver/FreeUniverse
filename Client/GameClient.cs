using FreeUniverse.Common;
using FreeUniverse.Common.UI;
using FreeUniverse.Common.Network;
using FreeUniverse.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreeUniverse.Common.Network.Messages;
using UnityEngine;
using Lidgren.Network;
using FreeUniverse.Common.Shared;

namespace FreeUniverse
{
    public sealed class GameClient :
        IBaseObject,
        IViewControllerLoginDelegate,
        IViewControllerCreateAccountDelegate
    {
        private NetworkClient loginClient { get; set; }
        private LoginServerDelegate loginDelegate { get; set; }
        private PlayerAccountDesc playerAccountInfo { get; set; }

        public static GameClient instance { get; set; }

        public GameClient()
        {
            instance = this;
        }

        public void Init()
        {
            InitNetworkClient();
            LoadViewControllers();
        }

        public void Release()
        {
            if (loginClient != null)
                loginClient.Stop();
        }

        public void Update(float dt)
        {
            if (loginClient != null)
            {
                loginClient.Update();
            }

            if (viewControllerLogin != null)
                viewControllerLogin.Update(dt);

            if (viewControllerCreateAccount != null)
                viewControllerCreateAccount.Update(dt);
            
        }

        private void InitNetworkClient()
        {
            loginDelegate = new LoginServerDelegate(this);
            loginClient = new NetworkClient();
            loginClient.Start();
        }

        private ViewControllerLogin viewControllerLogin { get; set; }
        private ViewControllerCreateAccount viewControllerCreateAccount { get; set; }

        private void LoadViewControllers()
        {
            viewControllerLogin = new ViewControllerLogin();
            viewControllerLogin.controllerDelegate = this;
            viewControllerLogin.visible = false;

            viewControllerCreateAccount = new ViewControllerCreateAccount();
            viewControllerCreateAccount.controllerDelegate = this;
            viewControllerCreateAccount.visible = false;
        }

        private class LoginServerDelegate : INetworkClientDelegate
        {
            private GameClient client { get; set; }

            public bool isConnected { get; set; }

            public enum Status
            {
                ConnectedNotAuthorized,
                ConnectedAndAuthorized, 
                Diconnected
            }

            public Status status { get; set; }

            public LoginServerDelegate(GameClient client)
            {
                this.status = Status.Diconnected;
                this.client = client;
                this.isConnected = false;
            }

            public void OnNetworkClientConnect(NetworkClient client, int result)
            {
                isConnected = true;

                this.status = Status.ConnectedNotAuthorized;

                ClientPluginController.OnClientPluginEvent(ClientPluginEvent.LoginServerConnect, result);
            }

            public void OnNetworkClientDisconnect(NetworkClient client, int reason)
            {
                isConnected = false;

                this.status = Status.Diconnected;

                ClientPluginController.OnClientPluginEvent(ClientPluginEvent.LoginServerDisconnect, reason);
            }

            public void OnNetworkClientMessage(NetworkClient client, NetworkMessage message)
            {
                // MH: or is it?
                //ClientPluginController.OnClientPluginEvent(ClientPluginEvent.LoginServerIncomingMessage, message);
            }
        }

        private static readonly string LOGIN_SERVER_IP = "127.0.0.1";
        private static readonly int LOGIN_SERVER_PORT = 16890;

        private struct LoginServerInfo
        {
            public string ip { get; set; }
            public int port { get; set; }

            public LoginServerInfo(string ip, int port) : this()
            {
                this.ip = ip;
                this.port = port;
            }
        }

        private LoginServerInfo GetLoginServerConnectionInfo()
        {
            return new LoginServerInfo(LOGIN_SERVER_IP, LOGIN_SERVER_PORT);
        }

        public void OnViewControllerLoginAction(ViewControllerLogin controller, string user, string password)
        {
            controller.visible = false;

            if (loginDelegate.isConnected)
                return;

            if (loginDelegate.status != LoginServerDelegate.Status.Diconnected)
                return;

            LoginServerInfo info = GetLoginServerConnectionInfo();

            loginClient.Connect(info.ip, info.port);
        }

        private class CreateAccountDelegate : INetworkClientDelegate
        {
            private GameClient client { get; set; }

            public CreateAccountDelegate(GameClient client)
            {
                this.client = client;
            }

            public void OnNetworkClientConnect(NetworkClient netClient, int result)
            {
                MsgRequestCreateAccount msg = new MsgRequestCreateAccount();
                msg[MsgRequestCreateAccount.FIELD_EMAIL] = client.data[0];
                msg[MsgRequestCreateAccount.FIELD_PASSWORD] = client.data[1];

                netClient.Send(msg, Lidgren.Network.NetDeliveryMethod.ReliableOrdered);
            }

            public void OnNetworkClientDisconnect(NetworkClient client, int reason)
            {
                
            }

            public void OnNetworkClientMessage(NetworkClient netClient, NetworkMessage message)
            {
                if (message.type != NetworkMessageType.ReplyCreateAccount)
                    return;

                MsgReplyCreateAccount msg = message as MsgReplyCreateAccount;

                bool result = msg[MsgReplyCreateAccount.FIELD_RESULT] == MsgReplyCreateAccount.ACCOUNT_CREATED;

                netClient.clientDelegate = null;
                netClient.Disconnect();
            }
        }

        private bool CanCreateAccount()
        {
            return viewControllerCreateAccount.visible && loginClient.isConnected;
        }

        private string[] data { get; set; }

        public void OnViewControllerCreateAccountAction(ViewControllerCreateAccount controller, string email, string password)
        {
            if (!CanCreateAccount())
                return;

            LoginServerInfo info = GetLoginServerConnectionInfo();

            loginClient.clientDelegate = new CreateAccountDelegate(this);
            loginClient.Connect(info.ip, info.port);

            data = new string[2];
            data[0] = email;
            data[1] = password;

            viewControllerCreateAccount.visible = false;

            // show wait form
        }
    }
}
