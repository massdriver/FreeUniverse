using FreeUniverse.Common;
using FreeUniverse.Common.Common;
using FreeUniverse.Common.Common.UI;
using FreeUniverse.Common.Network;
using FreeUniverse.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreeUniverse.Common.Network.Messages;

namespace FreeUniverse
{
    public sealed class GameClient :
        IBaseObject,
        IViewControllerLoginDelegate,
        IViewControllerCreateAccountDelegate
    {
        private NetworkClient loginClient { get; set; }
        private LoginServerDelegate loginDelegate { get; set; }
        private PlayerAccountInfo playerAccountInfo { get; set; }

        public static GameClient instance { get; set; }

        public GameClient()
        {
            instance = this;
        }

        public void Init()
        {
            //InitNetworkClient();
            LoadViewControllers();
        }

        public void Release()
        {
            if (loginClient != null)
                loginClient.Stop();
        }

        public void Update(float time)
        {
            if (loginClient != null)
                loginClient.Update();

            if (viewControllerLogin != null)
                viewControllerLogin.Update(time);
            
        }

        private void InitNetworkClient()
        {
            loginDelegate = new LoginServerDelegate(this);
            loginClient = new NetworkClient();
            loginClient.clientDelegate = loginDelegate;
            loginClient.Start();
        }

        private ViewControllerLogin viewControllerLogin { get; set; }
        private ViewControllerCreateAccount viewControllerCreateAccount { get; set; }

        private void LoadViewControllers()
        {
            viewControllerLogin = new ViewControllerLogin();
            viewControllerLogin.controllerDelegate = this;
            viewControllerLogin.visible = true;

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

        public void OnViewControllerLoginAction(ViewControllerLogin controller, string user, string password)
        {
            controller.visible = false;

            if (loginDelegate.isConnected)
                return;

            if (loginDelegate.status != LoginServerDelegate.Status.Diconnected)
                return;

            loginClient.Connect(LOGIN_SERVER_IP, LOGIN_SERVER_PORT);

            
        }

        public void OnViewControllerCreateAccountAction(ViewControllerCreateAccount controller, string email, string password)
        {
            //
        }
    }
}
