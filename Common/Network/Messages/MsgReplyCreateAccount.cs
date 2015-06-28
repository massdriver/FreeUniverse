using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Network.Messages
{
    public class MsgReplyCreateAccount : MsgGenericValues
    {
        public MsgReplyCreateAccount()
        {
            type = NetworkMessageType.ReplyCreateAccount;
        }

        public bool IsAccountCreated()
        {
            return this[FIELD_RESULT] == ACCOUNT_CREATED;
        }

        public static readonly int ACCOUNT_NOT_CREATED = 0;
        public static readonly int ACCOUNT_CREATED = 1;
        public static readonly string FIELD_RESULT = "result";
    }
}
