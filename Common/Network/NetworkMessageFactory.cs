using FreeUniverse.Common.Network.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Network
{
    public sealed class NetworkMessageFactory
    {
        private delegate NetworkMessage MsgConstructor();

        private MsgConstructor[] constructors { get; set; }

        public NetworkMessageFactory()
        {
            constructors = new MsgConstructor[(int)NetworkMessageType.MaxMessages + 1];

            constructors[(int)NetworkMessageType.ReplyCreateAccount] = CreateMsgReplyCreateAccount;
            constructors[(int)NetworkMessageType.RequestCreateAccount] = CreateMsgRequestCreateAccount;
            constructors[(int)NetworkMessageType.GenericValues] = CreateMsgGenericValues;
        }

        public NetworkMessage Create(NetworkMessageType type)
        {
            if (constructors[(int)type] == null)
                return null;

            return constructors[(int)type]();
        }

        private NetworkMessage CreateMsgReplyCreateAccount()
        {
            return new MsgReplyCreateAccount();
        }

        private NetworkMessage CreateMsgRequestCreateAccount()
        {
            return new MsgRequestCreateAccount();
        }

        private NetworkMessage CreateMsgGenericValues()
        {
            return new MsgGenericValues();
        }
    }
}
