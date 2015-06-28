using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using FreeUniverse.Common.Arch;
using FreeUniverse.Common;
using FreeUniverse.Common.Procedural;
using FreeUniverse.Common.Procedural.System;
using FreeUniverse.Common.Unity;
using FreeUniverse.Common.Procedural.Galaxy;

namespace FreeUniverse.Client
{
    public sealed class StartupController : MonoBehaviour
    {
        public GameObject mainCamera;

        public StartupController()
        {
            // mh: should be empty
        }

        void Start()
        {
            FreeUniverse.Common.UI.External.Init();

            gameClient = new GameClient();
            gameClient.Init();

            //StartServers();
        }

        void Update()
        {
            if (gameClient != null)
                gameClient.Update(Time.deltaTime);

            if (servers != null)
            {
                foreach (IBaseObject s in servers)
                    s.Update(Time.deltaTime);
            }
        }

        void OnApplicationQuit()
        {
            Debug.Log("shutdown");

            if (gameClient != null)
            {
                gameClient.Release();
                GameClient.instance = null;
                gameClient = null;
            }

            StopServers();
        }

        private GameClient gameClient { get; set; }

        private IServerObjectFactory serverFactory { get; set; }
        private List<IBaseObject> servers { get; set; }

        private void StartServers()
        {
            Debug.Log("starting servers");

            servers = new List<IBaseObject>();
            serverFactory = ServerObjectFactoryLoader.Load();

            IBaseObject loginServer = serverFactory.Create(ServerType.Login);

            loginServer.Init();

            servers.Add(loginServer);
        }

        private void StopServers()
        {
            if (servers != null)
            {
                foreach (IBaseObject s in servers)
                    s.Release();
            }
        }
        
    }
}
