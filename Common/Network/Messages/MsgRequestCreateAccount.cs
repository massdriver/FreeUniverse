using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Network.Messages
{
    public class MsgRequestCreateAccount : MsgGenericValues
    {
        public MsgRequestCreateAccount()
        {
            type = NetworkMessageType.RequestCreateAccount;
        }

        public static readonly string FIELD_EMAIL = "user";
        public static readonly string FIELD_PASSWORD = "pass";
    }
}
