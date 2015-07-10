using FreeUniverse.Common;
using FreeUniverse.Server.Account;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Server.Plugin
{
    // config file layout
    //
    // Assembly.dll ClassName
    //
    public sealed class ServerPluginController
    {
        private static List<IServerPlugin> plugins = new List<IServerPlugin>();

        private static readonly string PLUGIN_CONFIG_FILE = "server_plugins.ini";

        public static void LoadPlugins()
        {
            string[] strs = File.ReadAllLines(PLUGIN_CONFIG_FILE);

            foreach (string s in strs)
            {
                PluginInfo info = Parse(s);

                IServerPlugin plugin = Assist.CreateInstanceFromAssembly<IServerPlugin>(info.assembly, info.className);

                if (plugin != null)
                {
                    plugins.Add(plugin);
                    plugin.OnServerPluginEvent(ServerPluginEvent.PluginLoaded, null);
                }
            }

            Debug.Log("ServerPluginController: plugins=" + plugins.Count);
        }

        private struct PluginInfo
        {
            public string assembly;
            public string className;

            public PluginInfo(string assembly, string className)
            {
                this.assembly = assembly;
                this.className = className;
            }
        }

        private static PluginInfo Parse(string str)
        {
            string[] entry = str.Split(' ');

            return new PluginInfo(entry[0], entry[1]);
        }

        public static void OnServerPluginEvent(ServerPluginEvent eventType, object data)
        {
            foreach (IServerPlugin p in plugins)
                p.OnServerPluginEvent(eventType, data);
        }
    }
}
