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
                log.Trace("AddByteArrayToInbox()");
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
            if (incommingString == null
                || incommingString.Length <= Config.MessageMinLength
                || incommingString.Equals("")) {
                log.Warn("Nachricht wurde möglicherweise unvollständig übermittelt.");
                log.Warn("Breche Bearbeitung des Segments ab.");
                return;
            }
            log.Debug("ProcessSingleByteSegment() erhaltener String in INBOX: " + incommingString);
            List<string> incommingMessages = new List<string>();
            // In vielen Fällen erhalten wir mehrere Nachrichten in einem byteMessage-Packet:
            if (incommingString.Substring(Config.protocolMsgStart.Length).Contains(Config.protocolMsgStart)) {
                log.Warn("ProcessSingleInboxByteSegment() mehrere Nachrichten in einem Segment: " + incommingString);
                
                foreach (string soloMessage in SplitIntoSoloMessages(incommingString)) {
                    incommingMessages.Add(soloMessage);
                    log.Info("Erhaltene Einzelnachricht: " + soloMessage);
                }
            }
            else {
                incommingMessages.Add(incommingString);
            }

            foreach (string message in incommingMessages) {
                AdministrateSingleInboxMessage(senderConnection, message);
            }            
        }

        private string[] SplitIntoSoloMessages(string incommingString) {
            string separator = Config.protocolMsgEnd + Config.protocolMsgStart;

            string[] result = incommingString.Split(
                new string[] { separator }, 
                StringSplitOptions.RemoveEmptyEntries);

            // seperator muss wieder hinzugefügt werden.
            for (int i = 0; i < result.Length; i++) {
                string finalMessage = result[i];
                if (!finalMessage.Contains(Config.protocolMsgStart)) {
                    finalMessage = Config.protocolMsgStart + finalMessage;
                }
                if (!finalMessage.Contains(Config.protocolMsgEnd)) {
                    finalMessage = finalMessage + Config.protocolMsgEnd;
                }
                result[i] = finalMessage;
            }

            return result;
        }

        private void AdministrateSingleInboxMessage(Connection senderConnection, string incommingString) {
            ProtocolMessage incommingMessage = new ProtocolMessage();
            incommingMessage.LoadAndValidate(incommingString);
            if (incommingMessage == null) {
                log.Error("AdministrateSingleInboxMessage(): leere Nachricht erhalten" +
                    ", breche Verarbeitung dieser einzelnen Nachricht ab.");
                return;
            }

            log.Debug("AdministrateSingleInboxMessage : verwalte die erhaltene Nachricht (Weiterleitung, Modelupdate)");

            foreach (ByteMessage outboxMessage
                in PostalWorker.RedistributeInboxMessage(incommingMessage, senderConnection)) {

                outboxByteMessageStack.Add(outboxMessage);

                log.Debug("AdministrateSingleInboxMessage Nachricht zur Outbox hinzugefügt:" + ByteConverter.ToString(outboxMessage.Data, outboxMessage.Data.Length));
            }
        }
    }
}
