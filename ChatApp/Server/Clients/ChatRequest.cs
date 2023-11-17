using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Server.Clients {
    internal class ChatRequest {
        public Client Requester { get; set; }
        public Client Receiver { get; set; }
        public bool ReceiverAccepted { get; set; }
        public bool RequesterWithdrew { get; set; }
        /**
         * KISS
        public bool RequesterWithdrew { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime AcceptTime { get; set; }
        public bool IsExpired { get; set; }
        **/
    }
}
