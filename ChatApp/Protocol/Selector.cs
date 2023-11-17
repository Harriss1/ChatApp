using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ChatApp.Protocol {
    internal class Selector {
        public static string Type(XmlDocument doc) {
            return doc.DocumentElement.ChildNodes[0].InnerText;
        }
        public static string ProtocolVersion(XmlDocument doc) {
            return doc.DocumentElement.Attributes[
                        NodeDescription.Message.PROTOCOLVERSION
                        ].Value;
        }
    }
}
