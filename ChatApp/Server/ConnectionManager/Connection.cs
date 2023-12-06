using ChatApp.Server.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.Server.ConnectionManager {
    internal class Connection {
        public Client Client { get; set; }
        public Thread Thread { get; }
        public bool FlaggedToCancel { get; set; }
        public Connection(Thread thread) {
            FlaggedToCancel = false;
            Thread = thread;
        }
        public bool IsLoggedIn() {
            if (Client != null && Thread != null && Client.Name != null && !Client.Name.Equals("")) {
                return true;
            } else {
                return false;
            }
        }
    }
}
