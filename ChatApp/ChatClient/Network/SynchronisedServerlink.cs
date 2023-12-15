using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ChatApp.ChatClient.Network.Serverlink;

namespace ChatApp.ChatClient.Network {
    /// <summary>
    /// Verwaltet den Zugriff auf die im Thread laufende Client-Verbindung
    /// </summary>
    internal class SynchronisedServerlink {
        LogPublisher log = new LogPublisher("SynchronisedServerlink", false);
        private static Serverlink.Serverlink serverlink;
        private static Thread clientThread;
        public bool GracefullShutdown { get; set; }
        private static Thread handlerThread;
        CancellationTokenSource cancelToken;

        public SynchronisedServerlink() {
            if (serverlink != null) {
                throw new InvalidOperationException(
                    "[SynchronisedClientConnection] Instanz darf nur einmalig erstellt und verwendet werden.");
            }
            serverlink = new Serverlink.Serverlink();
            handlerThread = Thread.CurrentThread;
            GracefullShutdown = false;
        }
        internal bool IsConnectionToServerEstablished() {
            return serverlink != null && serverlink.IsConnectionToServerEstablished();
        }
        internal void StartConnection(string ipAddress, string serverPort) {
            if (cancelToken != null) {
                // Falls wir den Thread erneut starten, befreien wir die Ressourcen des alten
                // Thread-übergreifenden und über das Block-Level hinaus persistenten Token-Objekts
                cancelToken.Dispose();
            }
            GracefullShutdown = false;
            cancelToken = new CancellationTokenSource();
            clientThread = new Thread(() => RunTcpClientWorkerLoop(ipAddress, serverPort, cancelToken));
            clientThread.Start();            
        }

        /// <summary>
        /// Startet den Tcp Client Socket, im Anschluss sendet und empfängt
        /// der Socket kontinuierlich Daten.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        private void RunTcpClientWorkerLoop(string ipAddress, string port, CancellationTokenSource cancelToken) {
            serverlink.Connect(ipAddress, port);
            if (serverlink.IsConnectionToServerEstablished()) {
                serverlink.RunConnectionLoop();
                if (!IsConnectionToServerEstablished()) {
                    GracefullShutdown = true;
                }
                // beendet den Kinder-Thread
                // nachdem seine Hauptschleife ausläuft und hier hin gelangt.
                log.Info("Beende Thread - Rufe Cancel() über CancelationTokenSource auf.");
                cancelToken.Cancel();
            }                       
        }
        internal void EnqueueMessageToOutBox(string message) {
            ValidateThreadSafety();

            object _lock = new object();
            lock (_lock) {
                serverlink.EnqueueMessageToOutbox(message);
            }
        }


        internal string DequeueMessageFromInbox() {
            ValidateThreadSafety();
            string received = null;
            object _lock = new object();
            lock (_lock) {
                received = serverlink.DequeueMessageFromInbox();
            }
            return received;
        }

        /// <summary>
        /// Stoppt den fortwährenden Übertragungsloop (Senden+Empfangen)
        /// </summary>
        internal void CancelTransmission() {
            ValidateThreadSafety();

            serverlink.TransmissionFlaggedToCancel = true;
        }

        /// <summary>
        /// Beendet die Verbindung und den Client-Thread
        /// 
        /// Blockiert die Ausführung bis der Client Thread beendet wurde.
        /// </summary>
        internal void ShutdownConnection() {
            ValidateThreadSafety();

            log.Info("Stoppe Übertragungsloop.");
            CancelTransmission();
            // er setzt die Flags nicht im Thread?!
            serverlink.ConnectionFlaggedToShutdown = true;
            log.Info("Beende Verbindung");
            log.Info("Current Thread ID=" + Thread.CurrentThread.ManagedThreadId + "Client Thread Id = " + clientThread.ManagedThreadId);
            // .Net 4+ erlaubt es nicht mehr Threads zu stoppen ohne Join
            // Deshalb nutze ich ein CTS Token (siehe oben).

            //bool stopped = clientThread.Join(TimeSpan.FromSeconds(5));
            //if (!stopped) {
            //    throw new InvalidProgramException("[SynchronisedClientConnection] Client-Thread konnte nicht erfolgreich beendet werden. Sein Status = " + clientThread.ThreadState);
            //}
            //if(clientThread.ThreadState == ThreadState.WaitSleepJoin) {
            //    clientThread.Interrupt();
            //} else {
            //    throw new InvalidProgramException("[SynchronisedClientConnection] Client-Thread konnte nicht erfolgreich beendet werden. Sein Status = " + clientThread.ThreadState);
            //}
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
