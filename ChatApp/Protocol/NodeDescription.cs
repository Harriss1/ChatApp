using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Protocol {
    internal readonly struct NodeDescription {
        public struct Message {
            public static string NAME = "Message";
            public const string PROTOCOLVERSION = "ProtocolVersion";
            public struct Type {
                public const string NAME = "Type";
            }
            public struct Content {
                public const string NAME = "Content";
                public struct Sender {
                    public static string NAME = "Sender";
                }
                public struct Receiver {
                    public const string NAME = "Receiver";
                }
                public struct TextMessage {
                    public const string NAME = "TextMessage";
                    public const string TEXTVERSION = "TextVersion";
                }
            }
        }
    }
}
