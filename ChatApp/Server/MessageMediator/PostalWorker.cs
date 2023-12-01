﻿using ChatApp.Protocol;
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

            string messageType = inboxMessage.GetMessageType();
            if (messageType.Equals(MessageTypeEnum.UNDEFINED)) {
                log.Debug("Warnung: Request mit Nachrichten Typ 'UNDEFINED'");
                ProtocolMessage statusResponse = ServerMessageCreator.CreateServerStatusResponse();
                outbox.Add(CreateByteMessage(statusResponse, sender));
                ProtocolMessage failureResponse = ServerMessageCreator.CreateChatMessageTransmissionStatusResponse(ResultCodeEnum.FAILURE);
                outbox.Add(CreateByteMessage(failureResponse, sender));
                return outbox;
            }

            // Verbindungen ohne Login
            // --> erhalten nur Statusnachrichten und die Möglichkeit sich einzuloggen
            if (!sender.IsLoggedIn()) {
                if (messageType.Equals(MessageTypeEnum.STATUS_EXCHANGE)) {
                    log.Debug("status austausch");
                    ProtocolMessage response = ServerMessageCreator.CreateServerStatusResponse();
                    outbox.Add(CreateByteMessage(response, sender));
                }
                if (messageType.Equals(MessageTypeEnum.LOGIN)) {
                    string username = "[nicht extrahiert]";
                    username = inboxMessage.GetSenderUsername();
                    log.Info("Loginversuch von username=" + username);
                    string result = ResultCodeEnum.FAILURE;
                    if (LoginUser(username, sender)) {
                        result = ResultCodeEnum.SUCCESS;
                    }
                    ProtocolMessage response = ServerMessageCreator.CreateLoginResponse(result);
                    outbox.Add(CreateByteMessage(response, sender));
                }
                return outbox;
            }

            // Verbindungen mit Login

            log.Debug("EINGELOGGTER CLIENT Prüfe Antwortmöglichkeiten");

            // Chatnachricht
            if (messageType.Equals(MessageTypeEnum.CHAT_MESSAGE)) {
                log.Debug("Chatnachricht Verwaltung");
                string receiverUsername = inboxMessage.GetReceiverUsername();
                log.Debug("Suche Benutzer: " + receiverUsername);
                Connection receiver = connectionRegister.SearchByUsername(receiverUsername);
                string transmissionCode = ResultCodeEnum.FAILURE;
                // Suche nach dem Empfänger
                if (receiver == null) {
                    log.Warn("Empfänger hat sich noch nicht registriert");
                } else {
                    log.Debug("Empfänger gefunden");
                    log.Trace("Sende Nachricht von [" + inboxMessage.GetSenderUsername() + "]"
                        +"an [" + inboxMessage.GetReceiverUsername() + "] \r\n" +
                        "Inhalt=" + inboxMessage.GetTextMessageFromContent());
                    transmissionCode = ResultCodeEnum.SUCCESS;
                    ProtocolMessage messageToChatPartner = inboxMessage;
                    outbox.Add(CreateByteMessage(messageToChatPartner, receiver));
                }
                // Ergebnis der Übermittlung an Server (nicht ob der Empfänger die Nachricht hat)
                // dem Client mitteilen:
                log.Debug("chat message transmission result");
                ProtocolMessage transmissionStatusResponse = 
                    ServerMessageCreator.CreateChatMessageTransmissionStatusResponse(transmissionCode);
                outbox.Add(CreateByteMessage(transmissionStatusResponse, sender));
            }
            // Status Austausch
            if (messageType.Equals(MessageTypeEnum.STATUS_EXCHANGE)) {
                log.Debug("Antwort: Status Austausch");
                ProtocolMessage response = ServerMessageCreator.CreateServerStatusResponse();
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
            log.Debug("WARN konnte Benutzer ["+username+"] nicht einloggen");
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
