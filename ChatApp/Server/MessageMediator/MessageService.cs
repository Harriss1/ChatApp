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
                inboxByteMessageStack.Remove(finished);
            }
        }

        private void ProcessSingleByteSegment(ByteMessage byteMessage, Connection connection) {
            log.Debug("ProcessSingleByteSegment");
            String message = ByteConverter.ToString(byteMessage.Data, byteMessage.Length);
            ProtocolMessage doc = new ProtocolMessage();
            doc.CreateBaseMessage();
            ProtocolMessage incommingMessage = new ProtocolMessage();
            incommingMessage.LoadAndValidate(doc.GetXml().OuterXml);
            //doc.Create(message);
            if (doc != null) {
                if (!byteMessage.connection.HasDefinedClient()) {                    
                    switch (incommingMessage.GetMessageType()) {
                        case MessageTypeEnum.UNDEFINED:
                            log.Publish("Warnung: Request mit Nachrichten Typ 'UNDEFINED'");
                            ProtocolMessage statusResponseXml = ServerMessageCreator.CreateServerStatusResponse();
                            string xmlAsString = statusResponseXml.GetXml().OuterXml;
                            ByteMessage outboxByteMessage = new ByteMessage(
                                ByteConverter.ToByteArray(xmlAsString), xmlAsString.Length, byteMessage.connection);
                            outboxByteMessageStack.Add(outboxByteMessage);
                            break;
                        case MessageTypeEnum.LOGIN:
                            string username = "[nicht extrahiert]";
                            username = incommingMessage.GetSenderUsername();
                            log.Publish("Loginversuch von username="+username);
                            break;
                        default:
                            break;

                    }
                }
            }
        }

    }
}
