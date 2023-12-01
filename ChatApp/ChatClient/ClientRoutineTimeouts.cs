using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ChatApp.ChatClient {
    internal class ClientRoutineTimeouts {
        private Timer timer;
        private static ClientRoutineTimeouts instance;
        private ClientRoutineTimeouts() {
            timer = new Timer(2000);

        }
        internal static ClientRoutineTimeouts GetInstance() {
            object _lock = new object();
            lock (_lock) {
                if (instance == null) {
                    instance = new ClientRoutineTimeouts();
                }
            }
            return instance;
        }
        internal bool ReadyToExchangeStatus(bool resetTimeout) {
            return false;
        }
    }
}
