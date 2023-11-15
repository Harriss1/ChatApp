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
        public static readonly string ServerPort = Key("ServerPort");
        public static readonly string ServerAddress = Key("ServerAddress");
    }
}
