using System;

namespace NetworkPrototype {
    internal class NetworkClient {
        private string username;
        private string ipAdress;

        public NetworkClient(string username, string ipAdress) {
            this.username = username;
            this.ipAdress = ipAdress;
        }

        internal void sendMessageTo(string username, string content) {
            throw new NotImplementedException();
        }
    }
}