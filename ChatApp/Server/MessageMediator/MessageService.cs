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
        private static MessageService instance;
        object _lock = new object();

        private List<ByteMessage> inboxByteMessageStack = new List<ByteMessage>();
        private List<ByteMessage> outboxByteMessageStack = new List<ByteMessage>();
        LogPublisher log = new LogPublisher("MessageService");
        public List<Connection> connections = new List<Connection>();
        private MessageService() {
        }
        internal static MessageService GetInstance() {
            object _lock = new object();
            lock (_lock) {
                if (instance == null) {
                    instance = new MessageService();
                }
            }
            return instance;
        }

        public void AddByteArrayToInbox(byte[] data, int length, Connection connection) {
            lock (_lock) {
                log.Debug("AddByteArrayToInbox");
                inboxByteMessageStack.Add(new ByteMessage(data, length, connection));
                ProcessInboxByteMessageStack(connection);
            }
        }

        public byte[] GetNextOutboxByteArray(Connection connection) {
            lock (_lock) {
                log.Debug("### GetNextOutboxByteArray ThreadId= " + Thread.CurrentThread.ManagedThreadId);
                ByteMessage response = PopNextOutboxByteMessage(connection);
                if (response == null) {
                    return null;
                }
                return response.Data;
            }
        }

        private ByteMessage PopNextOutboxByteMessage(Connection connection) {
            ByteMessage response = null;
            foreach (ByteMessage message in outboxByteMessageStack) {
                // Hier liegt vielleicht der Fehler: Die Nachricht entspricht nicht der Verbindung des aktuellen server-Threads!
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

        private void ProcessInboxByteMessageStack(Connection connection) {
            //<issue>3</issue> Die Sammlung wurde geändert...
            // welche der beiden Sammlungen?
            // Fehler ist bei Zeile 68, also inboxByteMessageStack
            List<ByteMessage> processedMessages = new List<ByteMessage>();
            foreach (ByteMessage message in inboxByteMessageStack) {
                if (message.connection == connection) {
                    // TODO Auf mehrere Segmente verteilte Nachrichten verarbeiten.
                    processedMessages.Add(message);
                    ProcessSingleInboxByteSegment(message, connection);
                }
            }
            RemoveProcessedInboxMessages(processedMessages);
        }

        private void RemoveProcessedInboxMessages(List<ByteMessage> processedSegments) {
            foreach (ByteMessage finished in processedSegments) {
                log.Trace("inbox_length = " + inboxByteMessageStack.Count);
                log.Trace("Entferne INBOX Nachricht nach Processing:"
                    + ByteConverter.ToString(finished.Data, finished.Length));
                inboxByteMessageStack.Remove(finished);
                log.Trace("inbox_length = " + inboxByteMessageStack.Count);
            }
        }

        private void ProcessSingleInboxByteSegment(ByteMessage byteMessage, Connection senderConnection) {
            log.Debug("ProcessSingleByteSegment");
            String incommingString = ByteConverter.ToString(byteMessage.Data, byteMessage.Length);
            if(incommingString == null
                || incommingString.Length <= Config.MessageMinLength
                || incommingString.Equals("")) {
                log.Warn("Nachricht wurde möglicherweise unvollständig übermittelt.");
                log.Warn("Breche Bearbeitung des Segments ab.");
                return;
            }
            log.Debug("INBOX ProcessSingleByteSegment : erhaltene Nachricht: " + incommingString);
            
            ProtocolMessage incommingMessage = new ProtocolMessage();
            incommingMessage.LoadAndValidate(incommingString);
            if (incommingMessage == null) {
                log.Debug("ProcessSingleInboxByteSegment(): WARN leere Nachricht erhalten" +
                    ", breche Segment-Bearbeitung ab.");
                return;
            }

            if (incommingString.Substring(Config.protocolMsgStart.Length).Contains(Config.protocolMsgStart)){
                log.Warn("ProcessSingleInboxByteSegment() mehrere Nachrichten in einem Segment: " + incommingString);
                throw new NotImplementedException("mehrere Nachrichten in einem Segment");
            }
            log.Debug("INBOX ProcessSingleByteSegment : erstelle entsprechende Reaktion" +
                " (im Debug: add irgendwas to Outbox)");
                        
            foreach (ByteMessage outboxMessage 
                in PostalWorker.RedistributeInboxMessage(incommingMessage, senderConnection)) {

                outboxByteMessageStack.Add(outboxMessage);

                log.Debug("Nachricht zur OUTBOX hinzugefügt:" + ByteConverter.ToString(outboxMessage.Data, outboxMessage.Data.Length));
            }
        }
    }
}
