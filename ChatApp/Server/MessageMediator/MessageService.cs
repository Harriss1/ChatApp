using ChatApp.Protocol;
using ChatApp.Server.ConnectionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Server.MessageMediator {
    internal class MessageService {
        List<ByteMessage> byteMessageStack = new List<ByteMessage>();
        LogPublisher msg = new LogPublisher();
        public void ProcessBytes(byte[] data, int length, Connection connection) {
            byteMessageStack.Add(new ByteMessage(data, length, connection));
            ProcessByteMessageStack(connection);
        }

        private void ProcessByteMessageStack(Connection connection) {
            foreach(ByteMessage message in byteMessageStack) {
                if(message.connection == connection) {
                    // TODO Auf mehrere Segmente verteilte Nachrichten verarbeiten.
                    ProcessSingleByteSegment(message, connection);
                }
            }
        }

        private void ProcessSingleByteSegment(ByteMessage byteMessage, Connection connection) {
            String message = ByteConverter.ToString(byteMessage.Data, byteMessage.Length);
            ProtocolMessage doc = new ProtocolMessage();
            doc.CreateBaseMessage();
            ProtocolMessage test = new ProtocolMessage();
            test.Load(doc.GetXml().OuterXml);
            //doc.Create(message);
            if (doc != null) {
                if (!byteMessage.connection.HasDefinedClient()) {
                    if(test.GetMessageType().Equals(MessageTypeEnum.UNDEFINED)) {
                        msg.Publish("FEHLER Nachrichten Typ ist nicht definiert");
                    }
                }
            }
        }
    }
}
