using NetworkPrototypeOpenConnection.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Server {
    internal class ServerController {
        public delegate void OnEvent_PublishServerMessage(string message);
        private OnEvent_PublishServerMessage _publishServerMessage;
        public ServerController() {

        }

        public void SubscribeTo_PublishServerMessage(OnEvent_PublishServerMessage _publishServerMessage) {
            this._publishServerMessage += _publishServerMessage;
        }

        internal void Start() {
            ServerRunner serverRunner = new ServerRunner();
            serverRunner.StartServer(Config.ServerAddress, Config.ServerPort);
            PublishMessage("started Server");
        }

        public void PublishMessage(string message) {
            _publishServerMessage(message);
        }
            

        internal void GracefullyShutdown() {
            throw new NotImplementedException();
        }

        internal void Abort() {
            throw new NotImplementedException();
        }
    }
}
