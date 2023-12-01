using ChatApp.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ChatApp.Protocol.Engine {
    internal class ProtocolValidator {
        private static LogPublisher msg = new LogPublisher("ProtocolValidator");

        /// <summary>
        /// Eine Nachricht hat Protokoll-konformenen Content falls:
        /// # source: client
        /// type: login
        /// content: sender
        /// type: status_exchange
        /// content: sender, status_code
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        internal static bool HasValidContent(XmlDocument doc) {
            string source = Selector.Node.Source(doc).InnerText;
            
            switch (source) {
                case MessageSourceEnum.CLIENT_REQUEST:
                    if (!HasValidClientRequest(doc)) {
                        PublishContentValidationError("Client Request fehlerhaft");
                        return false;
                    }
                    break;
                case MessageSourceEnum.SERVER_RESPONSE:
                    if (!HasValidServerResponse(doc)) {
                        PublishContentValidationError("Server Response fehlerhaft");
                        return false;
                    }
                    break;
                //default:
            }
            return true;
        }

        private static bool HasValidServerResponse(XmlDocument doc) {
            string messageType = Selector.Node.Type(doc).InnerText;
            switch (messageType) {
                case MessageTypeEnum.STATUS_EXCHANGE:
                    XmlNode StatusCodeNode = Selector.Node.Content.ServerStatusCode(doc);
                    if (!StatusCodeNode.Name.Equals(NodeDescription.Message.Content.StatusCode.NAME)) {
                        return false;
                    }
                    break;
            }
            return true;
        }

        private static bool HasValidClientRequest(XmlDocument doc) {
            string messageType = Selector.Node.Type(doc).InnerText;
            switch (messageType) {
                case MessageTypeEnum.LOGIN:
                    XmlNode SenderNode = Selector.Node.Content.Sender(doc);
                    if (!SenderNode.Name.Equals(NodeDescription.Message.Content.Sender.NAME)) {
                        return false;
                    }
                    break;
            }
            return true;
        }

        /// <summary>
        /// Eine Nachricht ist Basis-Protokoll-konform falls:
        /// 1) Hauptelement Message mit protocolVersion existiert
        /// 2) Nodes Source, Type und Content existieren
        /// 3) Node Source und Type ihren möglichen Enum-Werten entsprechen
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        internal static bool IsBaseProtocolConform(XmlDocument doc) {
            if (!HasValidRootNode(doc)) {
                PublishBaseValidationError("Haupt-Knoten <message> falsch definiert");
                return false;
            }
            if (!HasValidSourceNode(doc)) {
                PublishBaseValidationError("Erster Unter-Knoten <source> falsch definiert");
                return false;
            }
            if (!HasValidTypeNode(doc)) {
                PublishBaseValidationError("Zweiter Unter-Knoten <type> falsch definiert");
                return false;
            }
            if (!HasValidContentNode(doc)) {
                PublishBaseValidationError("Dritter Unter-Knoten <content> falsch definiert");
                return false;
            }
            return true;
        }

        private static bool HasValidRootNode(XmlDocument doc) {
            try {
                if (Selector.Node.Root(doc) != null) {
                    if (!Selector.Value.RootNodeName(doc).Equals(
                        NodeDescription.Message.NAME
                        )) {
                        PublishBaseValidationError("First node has not Name=Message");
                        return false;
                    }
                    if (Selector.Value.ProtocolVersion(doc) == null) {
                        PublishBaseValidationError("First node has not Attribute ProtocolVersion");
                        return false;
                    }
                }
                else {
                    PublishBaseValidationError("Dokument komplett leer");
                    return false;
                }
            }
            catch (Exception e) {
                PublishBaseValidationError("Exception: " + e.Message);
                return false;
            }
            return true;
        }

        private static bool HasValidSourceNode(XmlDocument doc) {
            try {
                if (Selector.Node.Source(doc) == null) {
                    PublishBaseValidationError("ChildNode[0] (Source) existiert nicht");
                    return false;
                }
                if (!Selector.Value.SourceNodeName(doc).Equals(
                    NodeDescription.Message.Source.NAME
                    )) {
                    PublishBaseValidationError("First Sub-Node <Source> falscher Name");
                    return false;
                }
                string allowedMessageSources = Selector.Value.SourceText(doc);
                if (!MessageSourceEnum.Values().Contains(allowedMessageSources)) {
                    PublishBaseValidationError("<Source> Wert ist kein gültiger Enum-Wert");
                    return false;
                }
            }
            catch (Exception e) {
                PublishBaseValidationError("Exception: " + e.Message);
                return false;
            }
            return true;
        }

        private static bool HasValidTypeNode(XmlDocument doc) {
            try {
                if (Selector.Node.Type(doc) == null) {
                    PublishBaseValidationError("ChildNode[1] (Type) existiert nicht");
                    return false;
                }
                if (!Selector.Value.TypeNodeName(doc).Equals(
                    NodeDescription.Message.Type.NAME
                    )) {
                    PublishBaseValidationError("Zweiter Sub-Node <Type> falscher Name");
                    return false;
                }
                string allowedMessageTypes = Selector.Value.TypeText(doc);
                if (!MessageTypeEnum.Values().Contains(allowedMessageTypes)) {
                    PublishBaseValidationError("<Type> Wert ist kein gültiger Enum-Wert");
                    return false;
                }
            }
            catch (Exception e) {
                PublishBaseValidationError("Exception: " + e.Message);
                return false;
            }
            return true;
        }

        private static bool HasValidContentNode(XmlDocument doc) {
            try {
                if (Selector.Node.ContentNode(doc) == null) {
                    PublishBaseValidationError("ChildNode[2] (Content) existiert nicht");
                    return false;
                }
                if (!Selector.Value.ContentNodeName(doc).Equals(
                    NodeDescription.Message.Content.NAME
                    )) {
                    PublishBaseValidationError("Dritter Sub-Node <Content> falscher Name");
                    return false;
                }
            }
            catch (Exception e) {
                PublishBaseValidationError("Exception: " + e.Message);
                return false;
            }
            return true;
        }
        private static void PublishBaseValidationError(string v) {
            msg.Debug("[ProtocolValidator:IsBaseProtocolConform:Violation] " + v);
        }
        private static void PublishContentValidationError(string v) {
            msg.Debug("[ProtocolValidator:HasValidContent:Violation] " + v);
        }
    }
}
