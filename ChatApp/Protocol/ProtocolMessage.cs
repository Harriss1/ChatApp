using ChatApp.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ChatApp.Protocol {
    internal class ProtocolMessage {
        private static LogPublisher msg = new LogPublisher();
        XmlDocument xmlDocument;
        private string protocolVersion;
        private string messageType;

        public ProtocolMessage() { }
        public XmlDocument GetXml() {
            return xmlDocument;
        }
        public string GetMessageType() {
            return messageType;
        }
        public string GetProtocolVersion() {
            return protocolVersion;
        }
        public ProtocolMessage Load(string message) {
            if(this.xmlDocument != null) {
                msg.Publish("FEHLER Create(): Würde Daten überschreiben.");
                return this;
            }
            XmlDocument doc = new XmlDocument();
            try {
                doc.LoadXml(message);
            }
            catch (XmlException e) {
                msg.Publish("FEHLER ProtocolMessage.Create(): XmlDocument-Bibliothek kann String nicht zu Dokument umwandeln: " + e.Message);
                return null;
            }
            if (!ProtocolValidator.IsBaseProtocolConform(doc)) {
                return null;
            }
            this.xmlDocument = doc;
            messageType = Selector.Type(doc);
            protocolVersion = Selector.ProtocolVersion(doc);
            return this;
        }
        public ProtocolMessage CreateBaseMessage() {
            if (this.xmlDocument != null) {
                msg.Publish("FEHLER Create(): Würde Daten überschreiben.");
                return this;
            }
            XmlDocument doc = new XmlDocument();

            XmlNode root = doc.CreateElement(NodeDescription.Message.NAME);
            doc.AppendChild(root);

            XmlAttribute protocolVersion = doc.CreateAttribute(NodeDescription.Message.PROTOCOLVERSION);
            protocolVersion.Value = Config.ProtocolVersion;
            root.Attributes.Append(protocolVersion);

            XmlNode messageType = doc.CreateElement(NodeDescription.Message.Type.NAME);
            messageType.InnerText = MessageType.UNDEFINED.ToString();
            root.AppendChild(messageType);

            XmlNode content = doc.CreateElement(NodeDescription.Message.Content.NAME);
            root.AppendChild(content);

            msg.Publish("Created:\r\n" + doc.OuterXml);
            this.xmlDocument=doc;
            return this;
        }
    }
}
