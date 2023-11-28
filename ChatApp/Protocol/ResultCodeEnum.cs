using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Protocol {
    internal class ResultCodeEnum {
        private static string[] enumValues = {
            UNDEFINED,
            SUCCESS,
            FAILURE,
            IN_PROGRESS
        };

        public const string UNDEFINED = "undefined";
        public const string SUCCESS = "success";
        public const string FAILURE = "failure";
        public const string IN_PROGRESS = "inProgress";
        public static List<string> Values() {
            List<string> values = new List<string>();
            foreach (string value in enumValues) {
                values.Add(value);
            }
            return values;
        }
    }
}
