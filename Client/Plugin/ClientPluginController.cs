using FreeUniverse.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Plugin
{
    public enum ClientPluginEvent
    {
        PluginLoaded,

        LoginServerConnect,
        LoginServerAuthorize,
        LoginServerDisconnect,

        LoginServerIncomingMessage
    }

    public interface IClientPlugin
    {
        void OnClientPluginEvent(ClientPluginEvent eventType, object data);
    }

    public sealed class ClientPluginController
    {
        private static List<IClientPlugin> plugins = new List<IClientPlugin>();

        private static readonly string PLUGIN_CONFIG_FILE = "client_plugins.ini";

        public static void LoadPlugins()
        {
            string[] strs = File.ReadAllLines(PLUGIN_CONFIG_FILE);

            foreach (string s in strs)
            {
                PluginInfo info = Parse(s);

                IClientPlugin plugin = Assist.CreateInstanceFromAssembly<IClientPlugin>(info.assembly, info.className);

                if (plugin != null)
                {
                    plugins.Add(plugin);
                    plugin.OnClientPluginEvent(ClientPluginEvent.PluginLoaded, null);
                }
            }

            Debug.Log("ClientPluginController: plugins=" + plugins.Count);
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

        public static void OnClientPluginEvent(ClientPluginEvent eventType, object data)
        {
            foreach (IClientPlugin p in plugins)
                p.OnClientPluginEvent(eventType, data);
        }
    }
}
