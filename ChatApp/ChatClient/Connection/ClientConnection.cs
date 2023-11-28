using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.ChatClient.Connection.Implementation {
    internal class ClientConnection {
        private Queue<string> outboxMessages = new Queue<string>();
        private Queue<string> inboxMessages = new Queue<string>();
        private static ClientTcpSocket socket;
        private static bool stopSendReceiveLoop = false;
        internal ClientConnection() { 
            if(socket != null) {
                throw new InvalidOperationException(
                    "[ClientConnection] Instanz darf nur einmalig erstellt und verwendet werden.");
            }
            socket = new ClientTcpSocket();
        }
        internal void Connect(string ipAddress, string port) {
            socket.Connect(ipAddress, port);
        }
        internal void RunSendReceiveLoop() {
            while (!stopSendReceiveLoop) {
                string received = null;
                if (outboxMessages.Count > 0) {
                    received = socket.Send(outboxMessages.Dequeue());
                }
                if (received != null) {
                    inboxMessages.Enqueue(received);
                }
                Thread.Sleep(200);
            }
        }
        internal void StopSendReceiveLoop() {
            stopSendReceiveLoop = true;
        }
        internal void StopConnection() {
            if (stopSendReceiveLoop) {
                socket.Stop();
            }
        }
        internal string DequeueMessageFromInbox() {
            if (outboxMessages.Count == 0) {
                return null;
            }
            return inboxMessages.Dequeue();
        }
        internal void EnqueueMessageToOutbox(string message) {
            outboxMessages.Enqueue(message);
        }
    }
}
