using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp {
    internal class Config {

        private static string Key(string key) {
            return ConfigurationManager.AppSettings[key];
        }
        //public static readonly string ServerPort = Key("ServerPort");
        //public static readonly string ServerAddress = Key("ServerAddress");
        //internal static readonly string ProtocolVersion = Key("ProtocolVersion");
        public static readonly string ServerPort = "10015";
        public static readonly string ServerAddress = "127.0.0.1";
        public static readonly string ProtocolVersion = "2023_11_21";
        public static readonly string LogLevel = "info";
        public static readonly string[] OverwriteLogLevel = { 
            "Serverlink.error",
            "SynchronisedServerlink.warn",
            "TcpSocket.warn",
            "ChatController.warn"
        };
        // eine protokoll-konforme Nachricht ist immer mindestens 15 lang
        // wegen der Länge des "Anfang"- und "Ende"-Elements
        public static readonly int MessageMinLength = 15;
        public static readonly string protocolMsgStart = "<message protocolVersion=";
        public static readonly string protocolMsgEnd = "</message>";
    }
}
