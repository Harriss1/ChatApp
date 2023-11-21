using ChatApp.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ChatApp.Protocol.Engine {
    internal class ProtocolValidator {
        private static LogPublisher msg = new LogPublisher();
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
                if (Selector.Node.Content(doc) == null) {
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
            msg.Publish("[ProtocolValidator:IsBaseProtocolConform:Violation] " + v);
        }
    }
}
