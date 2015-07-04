using FreeUniverse.Common;
using FreeUniverse.Server.FileDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Server.Account
{
    // MH: this is a server version of account information
    // client side has its own one
    public class ClientAccount : DatabaseElement
    {
        public uidkey guid { get; private set; }
        public DateTime lastLogin { get; private set; }
        public DateTime createDate { get; private set; }
        public string email { get; private set; }
        public string password { get; private set; }

        public ClientAccount()
        {

        }

        public ClientAccount(string email, string password) : base(email)
        {
            this.email = email;
            this.password = password;
            this.createDate = DateTime.Now;
            this.lastLogin = DateTime.Now;
            this.guid = new uidkey(this.email + this.createDate.ToBinary().ToString());
        }

        public override void Read(System.IO.BinaryReader reader)
        {
            base.Read(reader);

            guid = new uidkey(reader);

            lastLogin = new DateTime(reader.ReadInt64());
            createDate = new DateTime(reader.ReadInt64());
            email = reader.ReadString();
            password = reader.ReadString();
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            base.Write(writer);

            guid.Write(writer);
            writer.Write(lastLogin.ToBinary());
            writer.Write(createDate.ToBinary());
            writer.Write(email);
            writer.Write(password);
        }

        public void OnLogin()
        {
            lastLogin = DateTime.Now;
        }

        public void OnLogout()
        {
            
        }
    }
}
