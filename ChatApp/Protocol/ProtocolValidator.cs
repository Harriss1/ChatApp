using ChatApp.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ChatApp.Protocol {
    internal class ProtocolValidator {
        private static LogPublisher msg = new LogPublisher();
        internal static bool IsBaseProtocolConform(XmlDocument doc) {
            if (!HasValidFirstNode(doc)) {
                PublishBaseValidationError("Haupt-Knoten <message> falsch definiert");
                return false;
            }
            if (!HasValidTypeNode(doc)) {
                PublishBaseValidationError("Erster Unter-Knoten <type> falsch definiert");
                return false;
            }
            if (!HasValidContentNode(doc)) {
                PublishBaseValidationError("Zweiter Unter-Knoten <content> falsch definiert");
                return false;
            }
            return true;
        }

        private static bool HasValidContentNode(XmlDocument doc) {
            try {
                if (doc.DocumentElement.ChildNodes[1] == null) {
                    PublishBaseValidationError("ChildNode[1] (Content) existiert nicht");
                    return false;
                }
                if (!doc.DocumentElement.ChildNodes[1].Name.Equals(
                    NodeDescription.Message.Content.NAME
                    )) {
                    PublishBaseValidationError("Second Sub-Node <Content> falscher Name");
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
                if (doc.DocumentElement.ChildNodes[0] == null) {
                    PublishBaseValidationError("ChildNode[0] (Type) existiert nicht");
                    return false;
                }
                if (!doc.DocumentElement.ChildNodes[0].Name.Equals(
                    NodeDescription.Message.Type.NAME
                    )) {
                    PublishBaseValidationError("First Sub-Node <Type> falscher Name");
                    return false;
                }
                string messageType = Selector.Type(doc);
                if (!MessageType.Values().Contains(messageType)) {
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

        private static bool HasValidFirstNode(XmlDocument doc) {
            try {
                if (doc.DocumentElement != null) {
                    if (!doc.DocumentElement.Name.Equals(
                        NodeDescription.Message.NAME
                        )) {
                        PublishBaseValidationError("First node has not Name=Message");
                        return false;
                    }
                    if (Selector.ProtocolVersion(doc) == null) {
                        PublishBaseValidationError("First node has not Attribute ProtocolVersion");
                        return false;
                    }
                } else {
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

        private static void PublishBaseValidationError(string v) {
            msg.Publish("[ProtocolValidator:IsBaseProtocolConform:Violation] " + v);
        }
    }
}
