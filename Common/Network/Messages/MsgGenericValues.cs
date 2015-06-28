using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Network.Messages
{
    public class MsgGenericValues : NetworkMessage
    {
        public ValueMap values { get; set; }

        public Value this[string key]
        {
            get
            {
                return values[key];
            }
            set
            {
                values[key] = value;
            }
        }

        public MsgGenericValues()
        {
            type = NetworkMessageType.GenericValues;
            values = new ValueMap();
        }

        public override NetworkMessage Write(Lidgren.Network.NetOutgoingMessage msgOut)
        {
            base.Write(msgOut);

            NetworkMessage.WriteByteBuffer(values.ToByteArray(), msgOut);

            return this;
        }

        public override NetworkMessage Read(Lidgren.Network.NetIncomingMessage msgIn)
        {
            base.Read(msgIn);

            values.FromByteArray(NetworkMessage.ReadByteBuffer(msgIn));

            return this;
        }
    }
}
