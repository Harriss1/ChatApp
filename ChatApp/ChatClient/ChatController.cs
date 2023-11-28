using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatApp.ChatClient.Connection;

namespace ChatApp.ChatClient {
    internal class ChatController {
        private static SynchronisedClientConnection tcpClientThread = new SynchronisedClientConnection();
        internal void LoginToServer(string username, string ipAddress) {
            tcpClientThread.StartTcpConnection(ipAddress, Config.ServerPort);
        }

        internal void SendMessage(string message) {
            tcpClientThread.EnqueueMessageToOutBox(message);
        }

        internal string GetLastReceivedMessage() {
            return tcpClientThread.DequeueMessageFromInbox();
        }
    }
}
