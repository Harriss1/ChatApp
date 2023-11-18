using ChatApp.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Server.MessageMediator {
    internal class MessageCreator {

        public static ProtocolMessage CreateServerStatusResponse() {
            return (new ProtocolMessage()).CreateBaseMessage();
        }
    }
}
