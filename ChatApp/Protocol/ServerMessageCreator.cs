using ChatApp.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Protocol {
    internal class ServerMessageCreator {

        public static ProtocolMessage CreateServerStatusResponse() {
            ProtocolMessage message = CreateBaseServerResponse();
            message.SetMessageType(MessageTypeEnum.STATUS_EXCHANGE);

            message.AppendStatusCodeIntoContent(StatusCodeEnum.ONLINE);

            return message;
        }

        internal static ProtocolMessage CreateLoginResponse(string resultCode) {
            ProtocolMessage message = CreateBaseServerResponse();
            message.SetMessageType(MessageTypeEnum.LOGIN);

            message.AppendResultCodeIntoContent(resultCode);

            return message;
        }

        public static ProtocolMessage CreateChatMessageTransmissionResponse(string resultCode) {
            ProtocolMessage message = CreateBaseServerResponse();
            message.SetMessageType(MessageTypeEnum.CHAT_MESSAGE_TRANSMISSION_STATUS);

            message.AppendResultCodeIntoContent(resultCode);

            return message;
        }

        private static ProtocolMessage CreateBaseServerResponse() {
            ProtocolMessage message = (new ProtocolMessage()).CreateBaseMessage();
            message.SetProtocolVersion(Config.ProtocolVersion);
            string sourceString = MessageSourceEnum.SERVER_RESPONSE;
            message.SetSourceType(sourceString);
            return message;
        }
    }
}
