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
        private static ServerRunner serverRunner = ServerRunner.GetInstace();
        private static ConnectionManagerService connectionManager;
        public ServerController() {
        }

        internal void Start() {
            serverRunner.StartServer(Config.ServerAddress, Config.ServerPort);
            log.Info("Started Server at " + Config.ServerAddress + ":" + Config.ServerPort);
            connectionManager = new ConnectionManagerService(serverRunner);
            connectionManager.Run();
        }

            

        internal void GracefullyShutdown() {
            if (serverRunner.IsGracefullyShutdown()) {
                log.Info("Der Server wurde bereits Server korrekt beendet.");
            } else {
                serverRunner.BeginGracefulShutdown(TimeSpan.FromSeconds(30));
                log.Info("Beginne den Server herunter zu fahren.");
            }
        }

        internal void Abort() {
            log.Info("Erzwinge Stop des Servers indem alle Threads beendet werden.");
            connectionManager.AbortAllConnections();
            serverRunner.Abort();
        }
    }
}
