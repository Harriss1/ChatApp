using ChatApp.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Protocol {
    internal class ServerMessageCreator {
        private static ProtocolMessage CreateBaseServerResponse() {
            ProtocolMessage message = (new ProtocolMessage()).CreateBaseMessage();
            message.SetProtocolVersion(Config.ProtocolVersion);
            string sourceString = MessageSourceEnum.SERVER_RESPONSE;
            message.SetSourceType(sourceString);
            return message;
        }
        private static ProtocolMessage CreateBaseServerRequest() {
            ProtocolMessage message = (new ProtocolMessage()).CreateBaseMessage();
            message.SetProtocolVersion(Config.ProtocolVersion);
            string sourceString = MessageSourceEnum.SERVER_REQUEST;
            message.SetSourceType(sourceString);
            return message;
        }
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
        internal static ProtocolMessage CreateLogoutResponse(string resultCode) {
            ProtocolMessage message = CreateBaseServerResponse();
            message.SetMessageType(MessageTypeEnum.LOGOUT);

            message.AppendResultCodeIntoContent(resultCode);

            return message;
        }

        public static ProtocolMessage CreateChatMessageTransmissionStatusResponse(string resultCode) {
            ProtocolMessage message = CreateBaseServerResponse();
            message.SetMessageType(MessageTypeEnum.CHAT_MESSAGE_TRANSMISSION_STATUS);

            message.AppendResultCodeIntoContent(resultCode);

            return message;
        }

        
        internal static ProtocolMessage CreateChatMessageResponse(string sender, string receiver, string textMessage) {
            ProtocolMessage message = CreateBaseServerResponse();
            message.SetMessageType(MessageTypeEnum.CHAT_MESSAGE);

            message.AppendSenderIntoContent(sender);
            message.AppendReceiverIntoContent(receiver);
            message.AppendTextMessageIntoContent(textMessage);

            return message;
        }
        internal static ProtocolMessage CreateChatPermissionResponse(string sender, string receiver, string resultCode) {
            ProtocolMessage message = CreateBaseServerResponse();
            message.SetMessageType(MessageTypeEnum.CHAT_REQUEST);

            message.AppendSenderIntoContent(sender);
            message.AppendReceiverIntoContent(receiver);
            message.AppendResultCodeIntoContent(resultCode);

            return message;
        }
        internal static ProtocolMessage CreateChatPermissionRequest(string sender, string receiver, string resultCode) {
            ProtocolMessage message = CreateBaseServerRequest();
            message.SetMessageType(MessageTypeEnum.CHAT_REQUEST);

            message.AppendSenderIntoContent(sender);
            message.AppendReceiverIntoContent(receiver);
            message.AppendResultCodeIntoContent(resultCode);

            return message;
        }
    }
}
