using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.ChatClient.Network.Serverlink {
    internal class Serverlink {
        LogPublisher log = new LogPublisher("Serverlink");
        private Queue<string> outboxMessages = new Queue<string>();
        private Queue<string> inboxMessages = new Queue<string>();
        internal bool TransmissionFlaggedToCancel { get; set; }
        internal bool ConnectionFlaggedToShutdown { get; set; }
        internal bool SocketFlaggedToAbort { get; set; }
        private static TcpSocket socket;
        private static bool stopSendReceiveLoop = false;
        internal Serverlink() { 
            if(socket != null) {
                throw new InvalidOperationException(
                    "[ClientConnection] Instanz darf nur einmalig erstellt und verwendet werden.");
            }
            TransmissionFlaggedToCancel = false;
            ConnectionFlaggedToShutdown = false;
            SocketFlaggedToAbort = false;

            socket = new TcpSocket();
        }
        internal void Connect(string ipAddress, string port) {
            socket.Connect(ipAddress, port);
        }
        internal void RunConnectionLoop() {
            log.Info("STARTE LOOP");
            // TODO FLAG TO CANCEL und STOP muss von hier ausgelesen werden.
            while (!stopSendReceiveLoop) {
                log.Info("Send");
                string received = null;
                if (outboxMessages.Count > 0) {
                    string outmsg = outboxMessages.Dequeue();
                    log.Info("outmsg=" + outmsg);
                    received = socket.Send(outmsg);
                }
                log.Info("enqueue");
                if (received != null) {
                    inboxMessages.Enqueue(received);
                }
                Thread.Sleep(200);
                CheckFlags();
            }
            log.Info("Send Receive Loop beendet.");
        }
        private void CheckFlags() {
            log.Info("check Flags");
            if (TransmissionFlaggedToCancel) {
                log.Info("stop");
                StopSendReceiveLoop();
                Thread.Sleep(1000);
            }
            if (ConnectionFlaggedToShutdown) {
                log.Info("shutdown");
                StopConnection();
            }
        }
        private void StopSendReceiveLoop() {
            stopSendReceiveLoop = true;
        }
        private void StopConnection() {
            if (stopSendReceiveLoop) {
                socket.Stop();
            }
        }
        internal string DequeueMessageFromInbox() {
            if (inboxMessages.Count == 0) {
                return null;
            }
            return inboxMessages.Dequeue();
        }
        internal void EnqueueMessageToOutbox(string message) {
            outboxMessages.Enqueue(message);
        }
    }
}
