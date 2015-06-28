using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Network
{
    public class NetworkMessage
    {
        public NetworkMessageType type { get; set; }
        public int source { get; set; }

        public static readonly int INVALID_SOURCE_ID = -1;

        public NetworkMessage()
        {
            type = NetworkMessageType.Null;
            source = INVALID_SOURCE_ID;
        }

        public static NetworkMessageType PeekType(NetIncomingMessage msg)
        {
            return (NetworkMessageType)msg.PeekUInt16();
        }

        public virtual NetworkMessage Read(NetIncomingMessage msgIn)
        {
            type = (NetworkMessageType)msgIn.ReadUInt16();
            return this;
        }

        public virtual NetworkMessage Write(NetOutgoingMessage msgOut)
        {
            msgOut.Write((ushort)type);
            return this;
        }

        protected static void WriteByteBuffer(byte[] data, NetOutgoingMessage msgOut)
        {
            msgOut.Write(data.Length);
            msgOut.Write(data);
        }

        protected static byte[] ReadByteBuffer(NetIncomingMessage msgIn)
        {
            return msgIn.ReadBytes(msgIn.ReadInt32());
        }
    }
}
