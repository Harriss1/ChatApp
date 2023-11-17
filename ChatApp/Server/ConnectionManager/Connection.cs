﻿using ChatApp.Server.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.Server.ConnectionManager {
    internal class Connection {
        public Client Client { get; set; }
        public Thread Thread { get; set; }
        public bool FlaggedToCancel { get; set; }
        public Connection() {
            FlaggedToCancel = false;
        }
        public bool IsWellDefined() {
            if (Client != null && Thread != null) {
                return true;
            } else {
                return false;
            }
        }
    }
}
