using ChatApp.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Server.Listener;
using ChatApp.Server.ConnectionManager;

namespace ChatApp.Server {
    internal class ServerController {
        private LogPublisher log = new LogPublisher("ServerController");
        private static ServerRunner serverRunner = new ServerRunner();
        private static ConnectionManagerService connectionManager;
        public ServerController() {
        }

        internal void Start() {
            serverRunner.StartServer(Config.ServerAddress, Config.ServerPort);
            log.Debug("Started Server at " + Config.ServerAddress + ":" + Config.ServerPort);
            connectionManager = new ConnectionManagerService(serverRunner);
            connectionManager.Run();
        }

            

        internal void GracefullyShutdown() {
            bool success = serverRunner.GracefullyShutdown();
            if (success) {
                log.Debug("Verbindungen wurden ordnungsgemäß abgebaut und der Server korrekt beendet.");
            } else {
                log.Debug("FEHLER: Server konnte nicht heruntergefahren werden. Bitte Logs konsultieren und ggf. Stop erzwingen.");
            }
        }

        internal void Abort() {
            log.Debug("Erzwinge Stop des Servers indem alle Threads beendet werden.");
            connectionManager.AbortAllConnections();
            serverRunner.Abort();
        }
    }
}
