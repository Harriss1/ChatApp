using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatApp.ChatClient.Network;
using ChatApp.Protocol;

namespace ChatApp.ChatClient {
    internal class ChatController {
        LogPublisher log = new LogPublisher("ChatController");
        private static SynchronisedServerlink serverlink = new SynchronisedServerlink();
        private static ChatSession chatSession;
        private static Queue<ProtocolMessage> chatMessages = new Queue<ProtocolMessage>();
        private static ProtocolMessage lastServerStatus;
        private static int mainRoutineCounter = 0;
        internal void LoginToServer(string username, string ipAddress) {
            serverlink.StartConnection(ipAddress, Config.ServerPort);
            chatSession = new ChatSession(username);
            chatSession.IsLoggedIn = false;
            SendLoginRequest();
            HandleNetworkMessages();
            if(lastServerStatus == null) {
                return;
            }
            if (lastServerStatus.GetStatusCodeFromContent().Equals(StatusCodeEnum.ONLINE)) {
                log.Debug("sollte mich jetzt einloggen...");
                // login
            } else {
                log.Debug("WARN kann nicht einloggen, da Server offline.");
            }
        }

        private void SendStatusExchangeRequest() {
            ProtocolMessage statusExchange = ClientMessageCreator.CreateStatusExchangeRequest();
            statusExchange.AppendSenderIntoContent(chatSession.Username);
            serverlink.EnqueueMessageToOutBox(statusExchange.GetXml().OuterXml);
        }
        private void SendLoginRequest() {
            ProtocolMessage loginRequest = ClientMessageCreator.CreateLoginRequest(chatSession.Username);
            serverlink.EnqueueMessageToOutBox(loginRequest.GetXml().OuterXml);
        }
        private void SendLogoutRequest() {
            ProtocolMessage logout = ClientMessageCreator.CreateLogoutRequest(chatSession.Username);
            serverlink.EnqueueMessageToOutBox(logout.GetXml().OuterXml);
            chatSession.IsLoggedIn = false;
        }

        internal void HandleNetworkMessages() {
            if (!ValidateSession()) {
                return;
            }
            if (mainRoutineCounter++ >= 0) {
                // nur hier schaut der Server nach, ob wir eine Nachricht erhalten haben -.-
                SendStatusExchangeRequest();
                mainRoutineCounter = 0;
            }

            string receivedMessage = serverlink.DequeueMessageFromInbox();
            while (receivedMessage != null) {
                ProtocolMessage message = new ProtocolMessage();
                if (message.LoadAndValidate(receivedMessage) != null) {
                    if (message.GetSource() == MessageSourceEnum.SERVER_RESPONSE) {
                        if (message.GetMessageType().Equals(MessageTypeEnum.STATUS_EXCHANGE)) {
                            lastServerStatus = message;
                        }
                        if (message.GetMessageType().Equals(MessageTypeEnum.CHAT_MESSAGE)) {
                            log.Debug("CLIENT HAT NACHRICHT ERHALTEN:" + message.GetXml().OuterXml);
                            chatMessages.Enqueue(message);
                        }
                        if (message.GetMessageType().Equals(MessageTypeEnum.CHAT_MESSAGE_TRANSMISSION_STATUS)) {
                            log.Debug("CLIENT HAT TRANSMISSION STATUS ERHALTEN:" + message.GetXml().OuterXml);
                            chatMessages.Enqueue(message);
                        }
                        if (message.GetMessageType().Equals(MessageTypeEnum.LOGIN)) {
                            if (message.GetResultCodeFromContent().Equals(ResultCodeEnum.SUCCESS)) {
                                chatSession.IsLoggedIn = true;
                                log.Debug("LOGIN SUCCESSFUL");
                            } else {
                                log.Warn("LOGIN FAILURE");
                                chatMessages.Enqueue(message);
                            }
                        }
                    }
                }
                receivedMessage = serverlink.DequeueMessageFromInbox();
            }
        }

        internal void SendMessage(string message, string receiver) {
            ValidateSession();

            ProtocolMessage protocolMessage = ClientMessageCreator.
                CreateChatMessageRequest(chatSession.Username, receiver, message);

            serverlink.EnqueueMessageToOutBox(protocolMessage.GetXml().OuterXml);
        }

        internal string DequeueReceivedChatMessage() {
            ValidateSession();
            if(chatMessages.Count == 0) {
                return null;
            }
            ProtocolMessage protocolMessage = chatMessages.Dequeue();
            return protocolMessage.GetXml().OuterXml;
        }
        internal void LogoutFromServer() {
            SendLogoutRequest();
            ValidateSession();
            serverlink.ShutdownConnection();
        }

        internal string GetServerlinkStatusMessage() {
            if (lastServerStatus != null) {
                return lastServerStatus.GetStatusCodeFromContent();
            }
            return "(keine Verbindung)";
        }

        private bool ValidateSession() {
            if (chatSession == null) {
                log.Debug("WARN Bitte zuerst auf Server verbinden");
                return false;
            }
            return true;
        }

        public bool IsLoggedIn() {
            if (chatSession == null || !chatSession.IsLoggedIn) {
                return false;
            }
            else {
                return true;
            }
        }

    }
}
