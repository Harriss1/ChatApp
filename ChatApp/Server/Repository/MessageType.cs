using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Server.Clients {
    internal enum MessageType {
        StatusExchangeRequest,
        StatusExchangeResponse,

        ChatPartnerStatusRequest,
        ChatPartnerStatusResponse,

        TextMessage,

        Login,
        Logout,
    }
}
