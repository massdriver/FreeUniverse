using FreeUniverse.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse
{
    internal class FreeUniverseServiceConfiguration
    {
        public string accountDatabaseDir { get; private set; }
        public string accountDatabasePassword { get; private set; }

        public int loginServerPort { get; private set; }
        public int maxClients { get; private set; }

        public static readonly string DEFAULT_CONFIG_FILE_PATH = "server_config.ini";

        public FreeUniverseServiceConfiguration()
        {
            TextAsset txt = Resources.Load("server_config") as TextAsset;

            INIReader reader = new INIReader(txt.text);

            foreach (INIReaderHeader header in reader.GetHeaders())
            {
                if (header.Check("ServerConfig"))
                {
                    foreach (INIReaderParameter parameter in header.parameters)
                    {
                        if (parameter.Check("maxclients"))
                        {
                            maxClients = parameter.GetInt(0);
                        }
                        else
                            if (parameter.Check("login_server_port"))
                            {
                                loginServerPort = parameter.GetInt(0);
                            }
                            else
                                if (parameter.Check("account_db_dir"))
                                {
                                    accountDatabaseDir = parameter.GetString(0);
                                }
                                else
                                    if (parameter.Check("account_db_pass"))
                                    {
                                        accountDatabasePassword = parameter.GetString(0);
                                    }
                    }
                }
            }
        }

        public FreeUniverseServiceConfiguration(string configFilePath)
        {
            throw new Exception("read from file not supported");

        }
    }
}
