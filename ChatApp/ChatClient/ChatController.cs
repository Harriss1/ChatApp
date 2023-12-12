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
        LogPublisher log = new LogPublisher("ChatController", false);
        private static SynchronisedServerlink serverlink = new SynchronisedServerlink();
        private static ChatSession chatSession;
        private static Queue<ProtocolMessage> chatMessages = new Queue<ProtocolMessage>();
        private static ProtocolMessage lastServerStatus;
        internal List<string> permittedChatPartners = new List<string>();
        private static int mainRoutineCounter = 0;
        internal bool LastChatMessageTransmitted { get; private set; }
        internal bool LastChatMessageSuccessfullySent { get; private set; }
        internal ChatController() {
            LastChatMessageSuccessfullySent = true;
            LastChatMessageTransmitted = true;
        }
        internal void LoginToServer(string username, string ipAddress) {
            serverlink.StartConnection(ipAddress, Config.ServerPort);
            chatSession = new ChatSession(username);
            chatSession.IsLoggedIn = false;
            Thread.Sleep(2000);
            if (serverlink.IsConnectionToServerEstablished()) {
                SendLoginRequest();
                HandleNetworkMessages();
            }
            if(lastServerStatus == null) {
                return;
            }
            if (lastServerStatus.GetStatusCodeFromContent().Equals(StatusCodeEnum.ONLINE)) {
                log.Info("sollte mich jetzt einloggen...");
                // login
                // <issue>7</issue>
            } else {
                log.Warn("WARN kann nicht einloggen, da Server offline.");
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
        }

        internal void HandleNetworkMessages() {
            if (serverlink.GracefullShutdown || !serverlink.IsConnectionToServerEstablished()) {
                log.Debug("Verbindung ist geschlossen.");
                return;
            }
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
                            LastChatMessageTransmitted = true;
                            log.Info("CLIENT HAT TRANSMISSION STATUS ERHALTEN:" + message.GetXml().OuterXml);
                            if (message.GetResultCodeFromContent().Equals(ResultCodeEnum.SUCCESS)) {
                                LastChatMessageSuccessfullySent = true;
                            }
                            else {
                                LastChatMessageSuccessfullySent = false;
                            }
                            chatMessages.Enqueue(message);
                        }
                        if (message.GetMessageType().Equals(MessageTypeEnum.LOGIN)) {
                            if (message.GetResultCodeFromContent().Equals(ResultCodeEnum.SUCCESS)) {
                                chatSession.IsLoggedIn = true;
                                log.Info("Server Response erhalten: LOGIN SUCCESSFUL");
                            } else {
                                log.Warn("Server Response erhalten: LOGIN FAILURE");
                                chatMessages.Enqueue(message);
                            }
                        }
                        if (message.GetMessageType().Equals(MessageTypeEnum.LOGOUT)) {
                            if (message.GetResultCodeFromContent().Equals(ResultCodeEnum.SUCCESS)) {
                                chatSession.IsLoggedIn = false;
                                log.Info("Server Response erhalten: LOGOUT erfolgreich");
                            }
                            else {
                                log.Warn("Server Response erhalten: LOGOUT fehlgeschlagen");
                                chatMessages.Enqueue(message);
                            }
                        }
                        if (message.GetMessageType().Equals(MessageTypeEnum.CHAT_REQUEST)) {
                            if (message.GetResultCodeFromContent().Equals(ResultCodeEnum.SUCCESS)) {
                                string approvedPartner = message.GetReceiverUsername();
                                if (!permittedChatPartners.Contains(approvedPartner)) {
                                    permittedChatPartners.Add(approvedPartner);
                                }
                            } else {
                                string deniedPartner = message.GetReceiverUsername();
                                if (permittedChatPartners.Contains(deniedPartner)) {
                                    permittedChatPartners.Remove(deniedPartner);
                                }
                            }
                        }
                    }
                }
                receivedMessage = serverlink.DequeueMessageFromInbox();
            }
        }

        internal ProtocolMessage SendChatPermissionRequest(string chatpartnerName) {
            ValidateSession();
            ProtocolMessage protocolMessage = ClientMessageCreator.
                CreateChatMessagePermissionRequest(chatSession.Username, chatpartnerName);
            serverlink.EnqueueMessageToOutBox(protocolMessage.GetXml().OuterXml);
            return protocolMessage;
        }

        internal ProtocolMessage SendMessage(string message, string receiver) {
            ValidateSession();

            ProtocolMessage protocolMessage = ClientMessageCreator.
                CreateChatMessageRequest(chatSession.Username, receiver, message);
            serverlink.EnqueueMessageToOutBox(protocolMessage.GetXml().OuterXml);
            LastChatMessageSuccessfullySent = false;
            LastChatMessageTransmitted = false;
            // <issue>8</issue> Übermittlungsstatus zurück geben für spezifische Nachricht.
            return protocolMessage;
        }


        internal ProtocolMessage DequeueReceivedChatMessage() {
            ValidateSession();
            HandleNetworkMessages();

            if(chatMessages.Count == 0) {
                return null;
            }
            ProtocolMessage protocolMessage = chatMessages.Dequeue();
            //return protocolMessage.GetXml().OuterXml;
            return protocolMessage;
        }
        internal void LogoutFromServer() {
            log.Error("Logout angefragt...");
            if (!chatSession.IsLoggedIn) {
                log.Error("Logout nicht möglich, da nicht eingeloggt.");
                return;
            }
            SendLogoutRequest();
            serverlink.ShutdownConnection();
        }

        internal string GetServerlinkStatusMessage() {
            if (lastServerStatus != null) {
                return "Serverstatus: " + lastServerStatus.GetStatusCodeFromContent();
            }
            return "Serverstatus: offline";
        }

        private bool ValidateSession() {
            if (chatSession == null) {
                log.Warn("WARN Bitte zuerst auf Server verbinden");
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
