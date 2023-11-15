using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Server.Clients {
    internal class ChatPartner {
        public Client Client { get; set; }
        public Client Parner { get; set; }
        public bool PartnerAccepted { get; set; }
        public bool ClientAccepted { get; set; }
    }
}
