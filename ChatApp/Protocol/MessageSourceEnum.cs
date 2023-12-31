﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Protocol {
    internal class MessageSourceEnum {
        private static string[] enumValues = {
            UNDEFINED,
            CLIENT_REQUEST,
            CLIENT_RESPONSE,
            SERVER_REQUEST,
            SERVER_RESPONSE
        };

        public const string UNDEFINED = "undefined";
        public const string CLIENT_REQUEST = "clientRequest";
        public const string CLIENT_RESPONSE = "clientResponse";
        public const string SERVER_REQUEST = "serverRequest";
        public const string SERVER_RESPONSE = "serverResponse";
        public static List<string> Values() {
            List<string> values = new List<string>();
            foreach (string value in enumValues) {
                values.Add(value);
            }
            return values;
        }
    }
}
