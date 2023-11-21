using ChatApp.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Server.MessageMediator {
    internal class ServerMessageCreator {

        public static ProtocolMessage CreateServerStatusResponse() {
            ProtocolMessage statusResponse = (new ProtocolMessage()).CreateBaseMessage();
            string typeString = MessageTypeEnum.STATUS_EXCHANGE;
            string sourceString = MessageSourceEnum.SERVER_RESPONSE;
            statusResponse.SetMessageType(typeString);
            statusResponse.SetSourceType(sourceString);
            statusResponse.SetProtocolVersion(Config.ProtocolVersion);
            statusResponse.AppendStatusCodeIntoContent(StatusCodeEnum.ONLINE);
            return statusResponse;
        }
    }
}
