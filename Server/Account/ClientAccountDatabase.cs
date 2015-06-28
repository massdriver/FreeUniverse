using FreeUniverse.Server.FileDatabase;
using FreeUniverse.Server.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Server.Account
{
    internal class ClientAccountDatabase
    {
        private Database<ClientAccount> accounts { get; set; }

        public ClientAccountDatabase(FreeUniverseServiceConfiguration config)
        {
            accounts = new Database<ClientAccount>(config.accountDatabaseDir, config.accountDatabasePassword);
        }

        public bool CreateNewAccount(string userName, string password)
        {
            ClientAccount newAccount = null;

            if (accounts.Exists(userName, out newAccount))
                return false;

            newAccount = new ClientAccount(userName, password);

            accounts.Store(newAccount, DatabaseLocationType.DiskOnly);

            ServerPluginController.OnServerPluginEvent(ServerPluginEvent.AccountCreated, newAccount);

            return true;
        }

        public bool Logout(ulong accid)
        {
            ClientAccount acc = accounts.Read(accid, DatabaseLocationType.MemoryOnly);

            if (acc == null)
                return false;

            acc.OnLogout();

            ServerPluginController.OnServerPluginEvent(ServerPluginEvent.AccountLogout, acc);

            return accounts.StoreAndUnload(acc.id);
        }

        public ClientAccount Login(string userName, string password)
        {
            ClientAccount acc = null;

            if (!accounts.Exists(userName, out acc))
                return null;

            if (acc.userPassword.CompareTo(password) != 0)
                return null;

            acc.OnLogin();

            ServerPluginController.OnServerPluginEvent(ServerPluginEvent.AccountLogin, acc);

            return acc;
        }
    }
}
