using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Protocol {
    internal class StatusCodeEnum {
        private static string[] enumValues = {
            UNDEFINED,
            ONLINE,
            OFFLINE,
            MAINTENANCE
        };

        public const string UNDEFINED = "undefined";
        public const string ONLINE = "online";
        public const string OFFLINE = "offline";
        public const string MAINTENANCE = "maintenance";
        public static List<string> Values() {
            List<string> values = new List<string>();
            foreach (string value in enumValues) {
                values.Add(value);
            }
            return values;
        }
    }
}
