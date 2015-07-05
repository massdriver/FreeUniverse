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
using UnityEngine.UI;

namespace FreeUniverse.Client
{
    public sealed class TestTableView :
        ITableViewDataProvider,
        ITableViewDelegate
    {
        private GameObject tablePanel { get; set; }
        private UITableView tableView { get; set; }

        public TestTableView(string panelNickname)
        {
            this.tablePanel = UnityEngine.GameObject.Find(panelNickname);

            this.tableView = this.tablePanel.GetComponent<UITableView>();
            this.tableView.tableViewDataProvider = this;
            this.tableView.tableViewDelegate = this;
            this.tableView.Reload();
            
        }

        public UITableViewParameters OnTableViewGetParameters(UITableView tableView)
        {
            UITableViewParameters p = new UITableViewParameters();

            p.rows = 4;
            p.initialScroll = 0.0f;
            p.cellHeight = -1.0f;
            p.cellWidth = -1.0f;

            return p;
        }

        public void OnTableViewSetupCell(UITableView tableView, UITableViewCell cell)
        {
            cell.GetElement<Text>("character_name").text = "char " + cell.row;
        }

        public void OnTableViewRowSelected(UITableView tableView, UITableViewCell cell)
        {
            
        }
    }

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

            //gameClient = new GameClient();
           // gameClient.Init();

            //StartServers();

            someTableView = new TestTableView("panel_tableview");
        }

        private TestTableView someTableView { get; set; }

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
