using ChatApp.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Server.Listener;

namespace ChatApp.Server {
    internal class ServerController {
        private LogPublisher msg = new LogPublisher();
        private static ServerRunner serverRunner = new ServerRunner();
        private static ConnectionManagerService connectionManager;
        public ServerController() {

        }

        internal void Start() {
            serverRunner.StartServer(Config.ServerAddress, Config.ServerPort);
            msg.Publish("Started Server at " + Config.ServerAddress + ":" + Config.ServerPort);
            connectionManager = new ConnectionManagerService(serverRunner);
            connectionManager.Run();
        }

            

        internal void GracefullyShutdown() {
            bool success = serverRunner.GracefullyShutdown();
            if (success) {
                msg.Publish("Verbindungen wurden ordnungsgemäß abgebaut und der Server korrekt beendet.");
            } else {
                msg.Publish("Server konnte nicht heruntergefahren werden. Bitte Logs konsultieren und ggf. Stop erzwingen.");
            }
        }

        internal void Abort() {
            msg.Publish("Erzwinge Stop des Servers indem alle Threads beendet werden.");
            serverRunner.Abort();
        }
    }
}
