using ChatApp.Protocol;
using ChatApp.Server.ConnectionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.Server.MessageMediator {
    internal class MessageService {
        List<ByteMessage> inboxByteMessageStack = new List<ByteMessage>();
        List<ByteMessage> outboxByteMessageStack = new List<ByteMessage>();
        LogPublisher log = new LogPublisher("MessageService");
        public void AddByteArrayToInbox(byte[] data, int length, Connection connection) {
            log.Debug("AddByteArrayToInbox");
            inboxByteMessageStack.Add(new ByteMessage(data, length, connection));
            ProcessByteMessageStack(connection);
        }

        public byte[] GetNextOutboxByteArray(Connection connection) {
            log.Debug("### GetNextOutboxByteArray ThreadId= " + Thread.CurrentThread.ManagedThreadId);
            ByteMessage response = PopNextOutboxByteMessage(connection);
            if (response == null) {
                return null;
            }
            return response.Data;
        }

        private ByteMessage PopNextOutboxByteMessage(Connection connection) {
            ByteMessage response = null;
            foreach (ByteMessage message in outboxByteMessageStack) {
                if (message.connection == connection) {
                     response = message;
                }
            }
            if (response != null) {
                outboxByteMessageStack.Remove(response);
                return response;
            }
            return null;
        }

        private void ProcessByteMessageStack(Connection connection) {
            List<ByteMessage> processedSegments = new List<ByteMessage>();
            foreach (ByteMessage message in inboxByteMessageStack) {
                if(message.connection == connection) {
                    // TODO Auf mehrere Segmente verteilte Nachrichten verarbeiten.
                    processedSegments.Add(message);
                    ProcessSingleByteSegment(message, connection);
                }
            }
            foreach (ByteMessage finished in processedSegments) {
                log.Debug("inbox_length = " + inboxByteMessageStack.Count);
                log.Debug("Entferne INBOX Nachricht nach Processing:" + ByteConverter.ToString(finished.Data, finished.Length));
                inboxByteMessageStack.Remove(finished);
                log.Debug("inbox_length = " + inboxByteMessageStack.Count);
            }
        }

        private void ProcessSingleByteSegment(ByteMessage byteMessage, Connection connection) {
            log.Debug("ProcessSingleByteSegment");
            String incommingString = ByteConverter.ToString(byteMessage.Data, byteMessage.Length);

            log.Debug("INBOX ProcessSingleByteSegment : erhaltene Nachricht: " + incommingString);
            ProtocolMessage debugMockMessage = new ProtocolMessage();
            debugMockMessage.CreateBaseMessage();
            string mockMessageString = debugMockMessage.GetXml().OuterXml;
            ProtocolMessage incommingMessage = new ProtocolMessage();
            incommingMessage.LoadAndValidate(incommingString);
            log.Debug("INBOX ProcessSingleByteSegment : erstelle entsprechende Reaktion (im Debug: add irgendwas to Outbox)");
            //doc.Create(message);
            if (incommingMessage != null) {
                if (!byteMessage.connection.HasDefinedClient()) {
                    ProtocolMessage response = new ProtocolMessage();
                    string messageType = incommingMessage.GetMessageType();
                    if (messageType.Equals(MessageTypeEnum.UNDEFINED)) {
                        log.Publish("Warnung: Request mit Nachrichten Typ 'UNDEFINED'");
                        response = ServerMessageCreator.CreateServerStatusResponse();
                    }
                    if (messageType.Equals(MessageTypeEnum.LOGIN)) {
                        string username = "[nicht extrahiert]";
                        username = incommingMessage.GetSenderUsername();
                        log.Publish("Loginversuch von username=" + username);
                        string result = ResultCodeEnum.SUCCESS;
                        response = ServerMessageCreator.CreateLoginResponse(result);
                    }
                    if (messageType.Equals(MessageTypeEnum.STATUS_EXCHANGE)) {
                        log.Publish("status austausch");
                        response = ServerMessageCreator.CreateServerStatusResponse();
                    }
                    if (messageType.Equals(MessageTypeEnum.CHAT_MESSAGE)) {
                        log.Publish("chat message transmission result");
                        string transmissionCode = ResultCodeEnum.SUCCESS;
                        response = ServerMessageCreator.CreateChatMessageTransmissionResponse(transmissionCode);
                    }
                    //switch (incommingMessage.GetMessageType()) {
                    //    case MessageTypeEnum.UNDEFINED:
                    //        log.Publish("Warnung: Request mit Nachrichten Typ 'UNDEFINED'");
                    //        response = ServerMessageCreator.CreateServerStatusResponse();                            
                    //        break;
                    //    case MessageTypeEnum.LOGIN:
                    //        string username = "[nicht extrahiert]";
                    //        username = incommingMessage.GetSenderUsername();
                    //        log.Publish("Loginversuch von username="+username);
                    //        string result = ResultCodeEnum.SUCCESS;
                    //        response = ServerMessageCreator.CreateLoginResponse(result);
                    //        break;
                    //    case MessageTypeEnum.STATUS_EXCHANGE:
                    //        response = ServerMessageCreator.CreateServerStatusResponse();
                    //        break;
                    //    case MessageTypeEnum.CHAT_MESSAGE:
                    //        string transmissionCode = ResultCodeEnum.SUCCESS;
                    //        response = ServerMessageCreator.CreateChatMessageTransmissionResponse(transmissionCode);
                    //        break;
                    //    default:
                    //        break;

                    //}
                    string xmlAsString = response.GetXml().OuterXml;
                    ByteMessage outboxByteMessage = new ByteMessage(
                        ByteConverter.ToByteArray(xmlAsString), xmlAsString.Length, byteMessage.connection);
                    outboxByteMessageStack.Add(outboxByteMessage);
                    log.Debug("Nachricht zur OUTBOX hinzugefügt:" + xmlAsString);
                }
            }
        }

    }
}
