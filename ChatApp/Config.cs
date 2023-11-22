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
        internal static readonly string ProtocolVersion = "2023_11_21";
    }
}
