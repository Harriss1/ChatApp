using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Protocol {
    internal class MessageType {
        private static string[] enumValues = {
            CHAT_MESSAGE, 
            LOGIN_REQUEST, 
            UNDEFINED
        };

        public const string CHAT_MESSAGE = "ChatMessage";
        public const string UNDEFINED = "Undefined";
        public const string LOGIN_REQUEST = "LoginRequest";
        //ChatMessageSendStatus,
        //ClientStatusChange, //Login Logout or BRB usw...
        //ServerStatusRequest,
        //ChatPartnerRequest,
        //ChatPartnerStatusRequest,
        //Undefined,
        public static List<string> Values() {
            List<string> values = new List<string>();
            foreach (string value in enumValues) {
                values.Add(value);
            }
            return values;
        }
    }
}
