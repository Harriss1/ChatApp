using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Protocol {
    /// <summary>
    /// Enum Alternative, da ich für .NET 7 keine Möglichkeit
    /// gefunden habe, durch ein Enum zu loopen usw.
    /// </summary>
    internal class MessageTypeEnum {
        private static string[] enumValues = {
            UNDEFINED,
            CHAT_MESSAGE,
            LOGIN,
            LOGOUT,
            LOGOUT_ORDER,
            STATUS_UPDATE,
            CHAT_REQUEST,
            CHAT_MESSAGE_TRANSMISSION_STATUS,
            CHAT_PARTNER_STATUS
        };

        public const string UNDEFINED = "Undefined";
        public const string CHAT_MESSAGE = "ChatMessage";
        public const string CHAT_MESSAGE_TRANSMISSION_STATUS = "ChatMessageTransmissionStatus";
        public const string LOGIN = "Login";
        public const string LOGOUT = "Logout";
        public const string LOGOUT_ORDER = "LogoutOrder";
        public const string STATUS_UPDATE = "StatusUpdate";
        public const string CHAT_REQUEST = "ChatRequest";
        public const string CHAT_PARTNER_STATUS = "ChatPartnerStatus";
        // TODO FileSendRequest usw...
        public static List<string> Values() {
            List<string> values = new List<string>();
            foreach (string value in enumValues) {
                values.Add(value);
            }
            return values;
        }
    }
}
