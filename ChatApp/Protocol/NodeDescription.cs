﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Protocol.Engine {
    internal readonly struct NodeDescription {
        public struct Message {
            public const string NAME = "message";
            public const string PROTOCOLVERSION = "protocolVersion";
            public const bool isOptional = false;
            public struct Source {
                public const string NAME = "source";
                public const bool isOptional = false;
            }
            public struct Type {
                public const string NAME = "type";
                public const bool isOptional = false;
            }
            public struct Content {
                public const string NAME = "content";
                public const bool isOptional = false;
                public struct StatusCode {
                    public const string NAME = "statusCode";
                    public const bool isOptional = true;
                }
                public struct ResultCode {
                    public const string NAME = "resultCode";
                    public const bool isOptional = true;
                }

                public struct Sender {
                    public static string NAME = "sender";
                    public const bool isOptional = true;
                }
                public struct Receiver {
                    public const string NAME = "receiver";
                    public const bool isOptional = true;
                }
                public struct TextMessage {
                    public const string NAME = "TextMessage";
                    public const bool isOptional = true;
                    public const string TEXTVERSION = "TextVersion";
                }
            }
        }
    }
}
