using FreeUniverse.Common.Network.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Network
{
    public class NetworkMessageFactory
    {
        private delegate NetworkMessage MsgConstructor();

        private MsgConstructor[] constructors { get; set; }

        public NetworkMessageFactory()
        {
            constructors = new MsgConstructor[(int)NetworkMessageType.MaxMessages + 1];

            constructors[(int)NetworkMessageType.GenericValues] = CreateMsgGenericValues;
        }

        public NetworkMessage Create(NetworkMessageType type)
        {
            return constructors[(int)type]();
        }

        private NetworkMessage CreateMsgGenericValues()
        {
            return new MsgGenericValues();
        }
    }
}
