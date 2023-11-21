using ChatApp.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ChatApp.Protocol.Engine;

namespace ChatApp.Protocol {
    internal class ProtocolMessage {
        private static LogPublisher msg = new LogPublisher();
        XmlDocument document;
        private string protocolVersion;
        private string messageType;
        private string messageSource;
        private bool alreadySet = false;
        public ProtocolMessage() { 
            document = new XmlDocument();
        }
        private XmlElement Root() {
            return document.DocumentElement;
        }
        private XmlNode Source() {
            return Root().ChildNodes[0];
        }
        public void SetSourceType(string type) {
            Source().InnerText = type;
            this.messageSource = type;
        }
        private XmlNode Type() {
            return Root().ChildNodes[1];
        }
        public XmlNode Content() {
            return Root().ChildNodes[2];
        }
        public void AppendStatusCodeIntoContent(string statusCode) {
            string nodeName = NodeDescription.Message.Content.StatusCode.NAME;        
            AppendNewContentChild(nodeName, statusCode);
        }
        internal string GetSenderUsername() {
            foreach (XmlNode node in Content().ChildNodes) {
                if (node.Name.Equals(NodeDescription.Message.Content.Sender.NAME)) {
                    return node.InnerText;
                }
            }
            return null;
        }
        internal void AppendSenderIntoContent(string username) {
            string nodeName = NodeDescription.Message.Content.Sender.NAME;
            AppendNewContentChild(nodeName, username);
        }
        private void AppendNewContentChild(string nodeName, string innerText) {
            ThrowIfContentChildNodeExists(nodeName);

            XmlNode newNode = document.CreateElement(nodeName);
            newNode.InnerText = innerText;
            Content().AppendChild(newNode);
        }

        private void ThrowIfContentChildNodeExists(string nodeDescriptionName) {
            foreach (XmlNode node in Content().ChildNodes) {
                if (node.Name.Equals(NodeDescription.Message.Content.Sender.NAME)) {
                    throw new InvalidOperationException(nodeDescriptionName+ " bereits angefügt");
                }
            }
        }

        public XmlDocument GetXml() {
            return document;
        }
        public string GetMessageType() {
            return messageType;
        }
        public void SetMessageType(string messageType) {
            Type().InnerText = messageType;
            this.messageType = messageType;
        }
        public string GetProtocolVersion() {
            return protocolVersion;
        }
        public void SetProtocolVersion(string version) {
            this.protocolVersion = version;
            Root().SetAttribute(NodeDescription.Message.PROTOCOLVERSION, version);
        }
        public string GetSource() {
            return messageSource;
        }
        public ProtocolMessage LoadAndValidate(string message) {
            if(alreadySet) {
                msg.Publish("FEHLER Create(): Würde Daten überschreiben, bitte neue Instanz erzeugen.");
                return this;
            }
            XmlDocument loadedDocument = new XmlDocument();
            try {
                loadedDocument.LoadXml(message);
            }
            catch (XmlException e) {
                msg.Publish("FEHLER ProtocolMessage.Create(): XmlDocument-Bibliothek kann String nicht zu Dokument umwandeln: " + e.Message);
                return null;
            }
            if (!ProtocolValidator.IsBaseProtocolConform(loadedDocument)) {
                msg.Publish("FEHLER XML INVALID: \r\n" + loadedDocument.OuterXml);
                return null;
            }
            
            this.document = loadedDocument;
            messageType = Selector.Value.TypeText(loadedDocument);
            protocolVersion = Selector.Value.ProtocolVersion(loadedDocument);
            messageSource = Selector.Value.SourceText(loadedDocument);
            alreadySet = true;
            return this;
        }
        public ProtocolMessage CreateBaseMessage() {
            if (alreadySet) {
                msg.Publish("FEHLER Create(): Würde Daten überschreiben.");
                return this;
            }
            alreadySet = true;
            // RootNode = <message protocolVersion="1">
            XmlNode root = document.CreateElement(NodeDescription.Message.NAME);
            document.AppendChild(root);
            XmlAttribute protocolVersion = document.CreateAttribute(NodeDescription.Message.PROTOCOLVERSION);
            protocolVersion.Value = Config.ProtocolVersion;
            root.Attributes.Append(protocolVersion);

            // 1. Node = <source>clientRequest</source>
            XmlNode messageSource = document.CreateElement(NodeDescription.Message.Source.NAME);
            messageSource.InnerText = MessageSourceEnum.UNDEFINED.ToString();
            root.AppendChild(messageSource);

            // 2. Node = <type>Login</Type>
            XmlNode messageType = document.CreateElement(NodeDescription.Message.Type.NAME);
            messageType.InnerText = MessageTypeEnum.UNDEFINED.ToString();
            root.AppendChild(messageType);

            // 3. Node = <content>[variable structure with optional elements]</content>
            XmlNode content = document.CreateElement(NodeDescription.Message.Content.NAME);
            root.AppendChild(content);

            msg.Publish("Created:\r\n" + document.OuterXml);
            return this;
        }

    }
}
