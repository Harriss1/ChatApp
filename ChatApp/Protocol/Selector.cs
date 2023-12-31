﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ChatApp.Protocol.Engine {
    /// <summary>
    /// Dient nur zum Selektieren der Werte und Elemente aus einer
    /// Protokoll konformen Xml-Datei
    /// </summary>
    public class Selector {
        public class Node {
            public static XmlElement Root(XmlDocument doc) {
                return doc.DocumentElement;
            }
            public static XmlNode Source(XmlDocument doc) {
                return Root(doc).ChildNodes[0];
            }
            public static XmlNode Type(XmlDocument doc) {
                return Root(doc).ChildNodes[1];
            }
            public static XmlNode ContentNode(XmlDocument doc) {
                return Root(doc).ChildNodes[2];
            }
            public class Content {
                public static XmlNode Sender(XmlDocument doc) {
                    return ContentNode(doc).ChildNodes[0];
                }
                public static XmlNode Receiver(XmlDocument doc) {
                    return ContentNode(doc).ChildNodes[1];
                }
                public static XmlNode ServerStatusCode(XmlDocument doc) {
                    return ContentNode(doc).ChildNodes[0];
                }
                public static XmlNode ResultCode(XmlDocument doc) {
                    return ContentNode(doc).ChildNodes[0];
                }
            }
        }
        public class Value {
            public static string RootNodeName(XmlDocument doc) {
                return Node.Root(doc).Name;
            }
            public static string ProtocolVersion(XmlDocument doc) {
                return Node.Root(doc).Attributes[
                            NodeDescription.Message.PROTOCOLVERSION
                            ].Value;
            }
            internal static object SourceNodeName(XmlDocument doc) {
                return Node.Source(doc).Name;
            }
            public static string SourceText(XmlDocument doc) {
                return Node.Source(doc).InnerText;
            }
            internal static object TypeNodeName(XmlDocument doc) {
                return Node.Type(doc).Name;
            }
            public static string TypeText(XmlDocument doc) {
                return Node.Type(doc).InnerText;
            }
            internal static object ContentNodeName(XmlDocument doc) {
                return Node.ContentNode(doc).Name;
            }
            internal static object ContentText(XmlDocument doc) {
                return Node.ContentNode(doc).InnerText;
            }

        }
    }
}
