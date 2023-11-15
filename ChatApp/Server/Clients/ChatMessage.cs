using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Server.Clients {
    internal class ChatMessage {
        public Client Sender { get; set; }
        public Client Receiver { get; set; }
        public MessageType Type { get; set}
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
    }
}