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
        private string messageSource;

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
        public string GetSource() {
            return messageSource;
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
                msg.Publish("FEHLER XML INVALID: \r\n" + doc.OuterXml);
                return null;
            }
            this.xmlDocument = doc;
            messageType = Getter.Value.TypeText(doc);
            protocolVersion = Getter.Value.ProtocolVersion(doc);
            messageSource = Getter.Value.SourceText(doc);
            return this;
        }
        public ProtocolMessage CreateBaseMessage() {
            if (this.xmlDocument != null) {
                msg.Publish("FEHLER Create(): Würde Daten überschreiben.");
                return this;
            }
            XmlDocument doc = new XmlDocument();

            // RootNode = <Message protocolVersion="1">
            XmlNode root = doc.CreateElement(NodeDescription.Message.NAME);
            doc.AppendChild(root);
            XmlAttribute protocolVersion = doc.CreateAttribute(NodeDescription.Message.PROTOCOLVERSION);
            protocolVersion.Value = Config.ProtocolVersion;
            root.Attributes.Append(protocolVersion);

            // 1. Node = <Source>clientRequest</source>
            XmlNode messageSource = doc.CreateElement(NodeDescription.Message.Source.NAME);
            messageSource.InnerText = MessageSourceEnum.UNDEFINED.ToString();
            root.AppendChild(messageSource);

            // 2. Node = <Type>Login</Type>
            XmlNode messageType = doc.CreateElement(NodeDescription.Message.Type.NAME);
            messageType.InnerText = MessageTypeEnum.UNDEFINED.ToString();
            root.AppendChild(messageType);

            // 3. Node = <Content>[variable structure with optional elements]</content>
            XmlNode content = doc.CreateElement(NodeDescription.Message.Content.NAME);
            root.AppendChild(content);

            msg.Publish("Created:\r\n" + doc.OuterXml);
            this.xmlDocument=doc;
            return this;
        }
    }
}
