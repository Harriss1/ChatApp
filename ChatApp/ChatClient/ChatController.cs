using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatApp.ChatClient.Connection;

namespace ChatApp.ChatClient {
    internal class ChatController {
        private static SynchronisedClientConnection connection = new SynchronisedClientConnection();
        internal void LoginToServer(string username, string ipAddress) {
            connection.StartConnection(ipAddress, Config.ServerPort);
        }

        internal void SendMessage(string message) {
            connection.EnqueueMessageToOutBox(message);
        }

        internal string GetLastReceivedMessage() {
            return connection.DequeueMessageFromInbox();
        }
    }
}
