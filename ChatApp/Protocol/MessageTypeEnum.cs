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
            STATUS_EXCHANGE,
            CHAT_REQUEST,
            CHAT_MESSAGE_TRANSMISSION_STATUS,
            CHAT_PARTNER_STATUS
        };

        public const string UNDEFINED = "undefined";
        public const string CHAT_MESSAGE = "chatMessage";
        public const string CHAT_MESSAGE_TRANSMISSION_STATUS = "chatMessageTransmissionStatus";
        public const string LOGIN = "login";
        public const string LOGOUT = "logout";
        public const string LOGOUT_ORDER = "logoutOrder";
        public const string STATUS_EXCHANGE = "statusExchange";
        public const string CHAT_REQUEST = "chatRequest";
        public const string CHAT_PARTNER_STATUS = "chatPartnerStatus";
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
