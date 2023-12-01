using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Protocol {
    internal class ClientMessageCreator {
        public static ProtocolMessage CreateStatusExchangeRequest() {
            ProtocolMessage message = CreateBaseClientRequest();
            message.SetMessageType(MessageTypeEnum.STATUS_EXCHANGE);

            message.AppendStatusCodeIntoContent(StatusCodeEnum.ONLINE);

            return message;
        }

        internal static ProtocolMessage CreateLoginRequest(string username) {
            ProtocolMessage message = CreateBaseClientRequest();
            message.SetMessageType(MessageTypeEnum.LOGIN);

            message.AppendSenderIntoContent(username);

            return message;
        }

        internal static ProtocolMessage CreateLogoutRequest(string username) {
            ProtocolMessage message = CreateBaseClientRequest();
            message.SetMessageType(MessageTypeEnum.LOGOUT);

            message.AppendSenderIntoContent(username);

            return message;
        }

        internal static ProtocolMessage CreateChatMessageRequest(string sender, string receiver, string textMessage) {
            ProtocolMessage message = CreateBaseClientRequest();
            message.SetMessageType(MessageTypeEnum.CHAT_MESSAGE);

            message.AppendSenderIntoContent(sender);
            message.AppendReceiverIntoContent(receiver);
            message.AppendTextMessageIntoContent(textMessage);

            return message;
        }

        private static ProtocolMessage CreateBaseClientRequest() {
            ProtocolMessage message = (new ProtocolMessage()).CreateBaseMessage();
            message.SetProtocolVersion(Config.ProtocolVersion);
            message.SetSourceType(MessageSourceEnum.CLIENT_REQUEST);
            return message;
        }
    }
}
