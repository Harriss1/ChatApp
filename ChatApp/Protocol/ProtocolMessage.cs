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
        private static LogPublisher log = new LogPublisher("ProtocollMessage");
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
        private XmlNode StatusCode() {
            return Content().ChildNodes[0];
        }
        private XmlNode ResultCode() {
            return Content().ChildNodes[0];
        }

        public void AppendResultCodeIntoContent(string code) {
            string nodeName = NodeDescription.Message.Content.ResultCode.NAME;
            AppendNewContentChild(nodeName, code);
        }
        public string GetResultCodeFromContent() {
            return GetTextValueFromContentChild(NodeDescription.Message.Content.ResultCode.NAME);
        }
        public void AppendStatusCodeIntoContent(string statusCode) {
            string nodeName = NodeDescription.Message.Content.StatusCode.NAME;
            AppendNewContentChild(nodeName, statusCode);
        }
        public string GetStatusCodeFromContent() {
            return GetTextValueFromContentChild(NodeDescription.Message.Content.StatusCode.NAME);
        }

        internal void AppendTextMessageIntoContent(string textMessage) {
            string nodeName = NodeDescription.Message.Content.TextMessage.NAME;
            AppendNewContentChild(nodeName, textMessage);
        }
        internal string GetTextMessageFromContent() {
            return GetTextValueFromContentChild(NodeDescription.Message.Content.TextMessage.NAME);
        }

        internal string GetReceiverUsername() {
            return GetTextValueFromContentChild(NodeDescription.Message.Content.Receiver.NAME);
        }
        internal void AppendReceiverIntoContent(string receiver) {
            string nodeName = NodeDescription.Message.Content.Receiver.NAME;
            AppendNewContentChild(nodeName, receiver);
        }

        internal string GetSenderUsername() {
            return GetTextValueFromContentChild(NodeDescription.Message.Content.Sender.NAME);
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

        /// <summary>
        /// Übergabewert muss einem Wert aus der Klasse NodeDescription.Name entsprechen
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns>null falls Node nicht gefunden im Content</returns>
        private string GetTextValueFromContentChild(string nodeName) {
            string nodeText = null;
            foreach (XmlNode node in Content().ChildNodes) {
                if (node.Name.Equals(nodeName)) {
                    return node.InnerText;
                }
            }
            return nodeText;
        }
        private void ThrowIfContentChildNodeExists(string nodeDescriptionName) {
            foreach (XmlNode node in Content().ChildNodes) {
                if (node.Name.Equals(nodeDescriptionName)) {
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
                log.Warn("FEHLER Create(): Würde Daten überschreiben, bitte neue Instanz erzeugen.");
                return this;
            }
            XmlDocument loadedDocument = new XmlDocument();
            try {
                loadedDocument.LoadXml(message);
            }
            catch (XmlException e) {
                log.Warn("FEHLER ProtocolMessage.Create(): XmlDocument-Bibliothek kann String nicht zu Dokument umwandeln: " + e.Message);
                return null;
            }
            if (!ProtocolValidator.IsBaseProtocolConform(loadedDocument)) {
                log.Warn("FEHLER XML INVALID: \r\n" + loadedDocument.OuterXml);
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
                log.Warn("FEHLER Create(): Würde Daten überschreiben.");
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

            log.Trace("Created:\r\n" + document.OuterXml);
            return this;
        }

    }
}
