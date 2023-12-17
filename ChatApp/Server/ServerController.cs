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
        private static ServerController instance;
        private static ServerRunner serverRunner = ServerRunner.GetInstance();
        private static ConnectionManagerService connectionManager;
        internal bool IsInShutdownProcess { get; private set; }
        private ServerController() {
            IsInShutdownProcess = false;
        }
        internal static ServerController GetInstace() {
            object _lock = new object();
            lock (_lock) {
                if (instance == null) {
                    instance = new ServerController();
                }
            }
            return instance;
        }
        internal void Start() {
            IsInShutdownProcess = false;
            serverRunner.StartServer(Config.ServerAddress, Config.ServerPort);
            log.Info("Started Server at " + Config.ServerAddress + ":" + Config.ServerPort);
            connectionManager = new ConnectionManagerService(serverRunner);
            connectionManager.Run();
        }

        internal bool IsGracefullyShutdown() {
            return serverRunner.IsGracefullyShutdown();
        }  

        internal void ShutdownGracefully() {
            if (serverRunner.IsGracefullyShutdown()) {
                IsInShutdownProcess = false;
                log.Info("Der Server wurde bereits Server korrekt beendet.");
            } else {
                IsInShutdownProcess = true;
                serverRunner.BeginGracefulShutdown(TimeSpan.FromSeconds(60));
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
