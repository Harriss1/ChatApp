using ChatApp.Protocol;
using ChatApp.Server.Clients;
using ChatApp.Server.ConnectionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Server.MessageMediator {
    /// <summary>
    /// Verarbeitung und Weitergabe aller Protokoll-konformen Nachrichten
    /// entweder in andere Programmteile z.B. Modell, oder an Empfänger
    /// </summary>
    internal class PostalWorker {
        private static ConnectionRegister connectionRegister = ConnectionRegister.GetInstance();
        static LogPublisher log = new LogPublisher("PostalWorker");
        internal static List<ByteMessage> RedistributeInboxMessage(ProtocolMessage inboxMessage, Connection sender) {
            
            List<ByteMessage> outbox = new List<ByteMessage>();
            if (inboxMessage == null || inboxMessage.GetMessageType() == null) {
                log.Error("inboxMessage nicht korrekt geparst");
                return outbox;
            }

            string messageType = inboxMessage.GetMessageType();
            if (messageType.Equals(MessageTypeEnum.UNDEFINED)) {
                log.Warn("Request mit Nachrichten Typ 'UNDEFINED'");
                ProtocolMessage statusResponse = ServerMessageCreator.CreateServerStatusResponse();
                outbox.Add(CreateByteMessage(statusResponse, sender));
                ProtocolMessage failureResponse = ServerMessageCreator.CreateChatMessageTransmissionStatusResponse(ResultCodeEnum.FAILURE);
                outbox.Add(CreateByteMessage(failureResponse, sender));
                return outbox;
            }

            // Verbindungen ohne Login
            // --> erhalten nur Statusnachrichten und die Möglichkeit sich einzuloggen
            if (!sender.IsLoggedIn()) {
                // Statusaustausch
                if (messageType.Equals(MessageTypeEnum.STATUS_EXCHANGE)) {
                    log.Debug("status austausch");
                    ProtocolMessage response = ServerMessageCreator.CreateServerStatusResponse();
                    outbox.Add(CreateByteMessage(response, sender));
                }
                // Login
                if (messageType.Equals(MessageTypeEnum.LOGIN)) {
                    string username = "[nicht extrahiert]";
                    username = inboxMessage.GetSenderUsername();
                    log.Info("Loginversuch von username=" + username);
                    string result = ResultCodeEnum.FAILURE;
                    if (LoginUser(username, sender)) {
                        result = ResultCodeEnum.SUCCESS;
                        log.Info("Erfolg Login von username=" + username);
                    }
                    else {
                        log.Warn("Fehlschlag Login von username=" + username);
                    }
                        ProtocolMessage response = ServerMessageCreator.CreateLoginResponse(result);
                    outbox.Add(CreateByteMessage(response, sender));
                }
                // Logout
                if (messageType.Equals(MessageTypeEnum.LOGOUT)) {
                    log.Warn("Client ist ist nicht eingeloggt und versucht sich auszuloggen");
                    string result = ResultCodeEnum.FAILURE;
                    ProtocolMessage response = ServerMessageCreator.CreateLogoutResponse(result);
                    outbox.Add(CreateByteMessage(response, sender));
                }
                return outbox;
            }

            // Verbindungen mit Login

            log.Debug("EINGELOGGTER CLIENT Prüfe Antwortmöglichkeiten");

            // Chatnachricht
            if (messageType.Equals(MessageTypeEnum.CHAT_MESSAGE)) {
                log.Debug("Verarbeitung der  erkannten Chatnachricht");
                string receiverUsername = inboxMessage.GetReceiverUsername();
                log.Debug("Suche Benutzer: " + receiverUsername);
                Connection receiver = connectionRegister.SearchByUsername(receiverUsername);
                string transmissionCode = ResultCodeEnum.FAILURE;
                // Suche nach dem Empfänger
                if (receiver == null) {
                    log.Warn("Empfänger hat sich noch nicht registriert, kann Nachricht nicht weiterleiten");
                } else {
                    log.Debug("Empfänger gefunden");
                    log.Info("Sende Nachricht von [" + inboxMessage.GetSenderUsername() + "]"
                        +"an [" + inboxMessage.GetReceiverUsername() + "] \r\n" +
                        "Inhalt=" + inboxMessage.GetTextMessageFromContent() + "\r\n" +
                        "XML=" + inboxMessage.GetXml().OuterXml);
                    transmissionCode = ResultCodeEnum.SUCCESS;
                    ProtocolMessage messageToChatPartner = ServerMessageCreator.CreateChatMessageResponse(
                        inboxMessage.GetSenderUsername(),
                         inboxMessage.GetReceiverUsername(),
                         inboxMessage.GetTextMessageFromContent());
                    outbox.Add(CreateByteMessage(messageToChatPartner, receiver));
                }
                // Ergebnis der Übermittlung an Server (nicht ob der Empfänger die Nachricht hat)
                // dem Client mitteilen:
                log.Trace("erstelle Übermittlungsergebnis der Chatnachricht von Client an Server:");
                ProtocolMessage transmissionStatusResponse = 
                    ServerMessageCreator.CreateChatMessageTransmissionStatusResponse(transmissionCode);
                outbox.Add(CreateByteMessage(transmissionStatusResponse, sender));
            }
            if (messageType.Equals(MessageTypeEnum.CHAT_REQUEST)) {
                log.Debug("Verarbeitung der  erkannten Chat Anfrage");
                string receiverUsername = inboxMessage.GetReceiverUsername();
                log.Debug("Suche Benutzer: " + receiverUsername);
                Connection receiver = connectionRegister.SearchByUsername(receiverUsername);
                string permissionResult = ResultCodeEnum.FAILURE;
                // Suche nach dem Empfänger
                if (receiver == null) {
                    log.Warn("Empfänger hat sich noch nicht registriert, kann Nachricht nicht weiterleiten");
                }
                else {
                    log.Debug("Empfänger gefunden");
                    log.Info("Erlaube Nachrichtenaustausch zwischen [" + inboxMessage.GetSenderUsername() + "]"
                        + " und [" + inboxMessage.GetReceiverUsername() + "] \r\n" +
                        "XML=" + inboxMessage.GetXml().OuterXml);
                    permissionResult = ResultCodeEnum.SUCCESS;                  
                }
                // Ergebnis der Prüfung des Servers ob der Client existiert (nicht ob der Empfänger bestätigt hat)
                // dem Client mitteilen:
                log.Trace("erstelle Übermittlungsergebnis der Chatanfrage von Client an Server:");
                ProtocolMessage chatRequestResponse = ServerMessageCreator.
                        CreateChatPermissionResponse(inboxMessage.GetSenderUsername(), inboxMessage.GetReceiverUsername(),
                        permissionResult);
                outbox.Add(CreateByteMessage(chatRequestResponse, sender));
            }
            // Status Austausch
            if (messageType.Equals(MessageTypeEnum.STATUS_EXCHANGE)) {
                log.Debug("Antwort: Status Austausch");
                ProtocolMessage response = ServerMessageCreator.CreateServerStatusResponse();
                outbox.Add(CreateByteMessage(response, sender));
            }
            // Logout
            if (messageType.Equals(MessageTypeEnum.LOGOUT)) {
                string username = inboxMessage.GetSenderUsername();
                log.Info("Beginne Logout-Vorgang für User:" + inboxMessage.GetSenderUsername());
                string result = ResultCodeEnum.FAILURE;
                if (LogoutUser(username, sender)) {
                    result = ResultCodeEnum.SUCCESS;
                }
                ProtocolMessage response = ServerMessageCreator.CreateLogoutResponse(result);
                outbox.Add(CreateByteMessage(response, sender));
            }

            return outbox;
        }

        private static bool LoginUser(string username, Connection sender) {
            foreach(Connection connection in connectionRegister.connections) {
                if (connection.Client != null &&
                    connection.Client.Name.Equals(username)) {
                    log.Debug("Benutzer [" + username + "] ist bereits eingeloggt.");
                    return false;
                }
                if(connection == sender) {
                    Client client = new Client();
                    client.Name = username;
                    connection.Client = client;
                    return true;
                }
            }
            log.Debug("konnte Benutzer ["+username+"] nicht einloggen");
            return false;
        }

        private static bool LogoutUser(string username, Connection sender) {
            foreach (Connection connection in connectionRegister.connections) {
                if (connection == sender) {
                    if (connection.Client == null) {
                        log.Warn("Logoutrequest verweigert: Client-Objekt existiert für Verbindung nicht.");
                        return false;
                    }
                    if(!connection.Client.Name.Equals(username)) {
                        log.Warn("Logoutrequest verweigert: Übermittelter Username (" + username +
                            ") und gespeicherter (" + connection.Client.Name +
                            ") stimmen nicht überein.");
                        return false;
                    }
                    log.Info("Benutzer für Logoutvorgang [" + username + "] gefunden.");
                    connection.FlaggedToCancel = true;
                    connection.Client = null;
                    return true;
                }
            }
            log.Warn("konnte Benutzer [" + username + "] nicht ausloggen.");
            return false;
        }

        private static ByteMessage CreateByteMessage(ProtocolMessage message, Connection receiver) {
            string xmlAsString = message.GetXml().OuterXml;

            ByteMessage outboxByteMessage = new ByteMessage(
                ByteConverter.ToByteArray(xmlAsString), xmlAsString.Length, receiver);
            return outboxByteMessage;
        }
    }
}
