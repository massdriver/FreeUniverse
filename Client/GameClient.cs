using FreeUniverse.Common;
using FreeUniverse.Common.Common;
using FreeUniverse.Common.Common.UI;
using FreeUniverse.Common.Network;
using FreeUniverse.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse
{
    public sealed class GameClient : IBaseObject 
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

        private void LoadViewControllers()
        {
            viewControllerLogin = new ViewControllerLogin();
            viewControllerLogin.visible = true;
        }

        private class LoginServerDelegate : INetworkClientDelegate
        {
            private GameClient client { get; set; }

            public LoginServerDelegate(GameClient client)
            {
                this.client = client;
            }

            public void OnNetworkClientConnect(NetworkClient client, int result)
            {
                ClientPluginController.OnClientPluginEvent(ClientPluginEvent.LoginServerConnect, result);
            }

            public void OnNetworkClientDisconnect(NetworkClient client, int reason)
            {
                ClientPluginController.OnClientPluginEvent(ClientPluginEvent.LoginServerDisconnect, reason);
            }

            public void OnNetworkClientMessage(NetworkClient client, NetworkMessage message)
            {
                // MH: or is it?
                //ClientPluginController.OnClientPluginEvent(ClientPluginEvent.LoginServerIncomingMessage, message);
            }
        }
    }
}
