using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ChatApp.ChatClient.Connection.Implementation;

namespace ChatApp.ChatClient.Connection {
    /// <summary>
    /// Verwaltet den Zugriff auf die im Thread laufende Client-Verbindung
    /// </summary>
    internal class SynchronisedClientConnection {
        private static ClientConnection clientConnection;
        private static Thread clientThread;
        private static Thread handlerThread;

        public SynchronisedClientConnection() {
            if (clientConnection != null) {
                throw new InvalidOperationException(
                    "[SynchronisedClientConnection] Instanz darf nur einmalig erstellt und verwendet werden.");
            }
            clientConnection = new ClientConnection();
            handlerThread = Thread.CurrentThread;
        }

        internal void StartConnection(string ipAddress, string serverPort) {
            clientThread = new Thread(() => RunTcpClientLoop(ipAddress, serverPort));
            clientThread.Start();            
        }

        /// <summary>
        /// Startet den Tcp Client Socket, im Anschluss sendet und empfängt
        /// der Socket kontinuierlich Daten.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        private void RunTcpClientLoop(string ipAddress, string port) {
            clientConnection.Connect(ipAddress, port);
            clientConnection.RunSendReceiveLoop();
        }
        internal void EnqueueMessageToOutBox(string message) {
            ValidateThreadSafety();

            object _lock = new object();
            lock (_lock) {
                clientConnection.EnqueueMessageToOutbox(message);
            }
        }


        internal string DequeueMessageFromInbox() {
            ValidateThreadSafety();
            string received = null;
            object _lock = new object();
            lock (_lock) {
                received = clientConnection.DequeueMessageFromInbox();
            }
            return received;
        }

        /// <summary>
        /// Stoppt den fortwährenden Übertragungsloop (Senden+Empfangen)
        /// </summary>
        internal void CancelTransmission() {
            ValidateThreadSafety();

            clientConnection.StopSendReceiveLoop();
        }

        /// <summary>
        /// Beendet die Verbindung und den Client-Thread
        /// 
        /// Blockiert die Ausführung bis der Client Thread beendet wurde.
        /// </summary>
        internal void ShutdownConnection() {
            ValidateThreadSafety();

            CancelTransmission();
            clientConnection.StopConnection();
            clientThread.Join(TimeSpan.FromSeconds(2));
            if (clientThread.ThreadState != ThreadState.Stopped) {
                throw new InvalidProgramException("[SynchronisedClientConnection] Client-Thread konnte nicht erfolgreich beendet werden.");
            }
        }


        private static void ValidateThreadSafety() {
            Check_IsTcpClientThreadRunning();
            Check_IsNotAccessedFromClientThread();
        }
        private static void Check_IsNotAccessedFromClientThread() {
            if (Thread.CurrentThread == clientThread) {
                throw new InvalidOperationException(
                    "[SynchronisedClientConnection] Die Verbindung darf nicht vom Client Thread aus gesteuert werden.");
            }
        }
        private static void Check_IsTcpClientThreadRunning() {
            if (clientThread == null) {
                throw new InvalidOperationException("[TcpClientThread] Der Thread muss zuerst mittels StartTcpConnection gestartet werden.");
            }
        }
    }
}
