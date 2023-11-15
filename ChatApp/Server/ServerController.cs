using NetworkPrototypeOpenConnection.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Server {
    internal class ServerController {
        internal void Start() {
            ServerRunner serverRunner = new ServerRunner();
            serverRunner.StartServer(Config.ServerAddress, Config.ServerPort);
        }

        internal void GracefullyShutdown() {
            throw new NotImplementedException();
        }

        internal void Abort() {
            throw new NotImplementedException();
        }
    }
}
