using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Network
{
    public class NetworkMessageHandler
    {
        public delegate void NetMsgDelegate(NetworkMessage msg);

        private NetMsgDelegate[] handlers { get; set; }

        public NetworkMessageHandler()
        {
            handlers = new NetMsgDelegate[(int)NetworkMessageType.MaxMessages + 1];
        }

        public NetMsgDelegate this[NetworkMessageType id]
        {
            get
            {
                return handlers[(int)id];
            }

            set
            {
                handlers[(int)id] = value;
            }
        }

        public void ProcessMessage(NetworkMessage msg)
        {
            if (handlers[(int)msg.type] != null)
                handlers[(int)msg.type](msg);
        }
    }
}
