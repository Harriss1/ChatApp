using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Server.Clients {
    internal class Client {
        public int Id { get; set; }
        public string Name { get; set; }
        public ConnectionStatus ConnectionStatus { get; set; }
        public string ipAddress { get; set; }
        public List<ChatPartner> Partners { get; set; } // Vorsicht, darf nur Tiefe von 1 haben
    }
}
