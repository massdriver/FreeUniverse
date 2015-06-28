using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Network
{
    public enum NetworkMessageType
    {
        Null = 0,

        NetConnectionAccepted,
        NetConnectionLost,

        GenericValues,

        RequestAccountLogin,
        RequestAccountLogout,

        ReplyAccountLogin,
        ReplyAccountLogout,

        RequestCreateAccount,
        ReplyCreateAccount,

        MaxMessages
    }
}
