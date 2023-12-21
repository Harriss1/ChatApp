using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

// Zusätzlicher Namespace um zu erklären, dass dies eine Unterklasse ist
namespace ChatApp.Server.Listener {
    // Rename to TcpListener?
    internal class TcpServer {
        private const int maxRequestLimit = 100;
        private Socket listener;
        private static bool alreadyStarted = false;
        private Stopwatch cancelMainLoopStopwatch;
        private readonly TimeSpan maxCancelMainLoopLookup = TimeSpan.FromSeconds(5);
        public readonly bool useTimeoutForResponse = true;
        public readonly TimeSpan maxResponseWaitTimeout = TimeSpan.FromSeconds(4);

        public delegate void ConnectionAcceptedCallback();
        private LogPublisher log = new LogPublisher("TcpServer");
        
        public TcpServer() {
        }


        // Idee: Adresse und Port mittels Setter? Ist hier nicht nötig, also JANGI
        // Man kann halt nicht den Server instanziieren
        // Vielleicht braucht ja jemand Start und Adresse setzen an unterschiedlichen Stellen?

        /// <summary>
        /// Startet den Server und beginnt den Port abzuhören.
        /// 
        /// IP-Adresse und Port sind nicht änderbar während der Server aktiv ist,
        /// deshalb kann diese nur beim Start festgelegt werden, und eine Änderung
        /// ist nur nach Stop möglich.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        public void StartAndListen(string ipAddress, string port) {
            // Es ist zwingend hier die Listener Instanz einmalig
            // für das Programm zu instanziieren, ansonsten kommt der Fehler:
            // System.Net.Sockets.SocketException (0x80004005): Normalerweise darf jede
            // Socketadresse (Protokoll, Netzwerkadresse oder Anschluss) nur jeweils
            // einmal verwendet werden
            // Es geht darum, dass der Socket auch nicht verwendbar ist, falls er wieder freigegeben wurde.
            // Todo:Recherche: bei Thread auch?
            if (alreadyStarted) {
                throw new InvalidOperationException("already started - listener is only allowed to run once during the execution of the server application");
            }
            else {
                alreadyStarted = true;
            }
            try {
                IPAddress endpointIp = IPAddress.Parse(ipAddress);
                int portNum = Int32.Parse(port);
                IPEndPoint localEndPoint = new IPEndPoint(endpointIp, portNum);
                // Create a Socket that will use Tcp protocol
                this.listener = new Socket(endpointIp.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // "Festlegen auf den Endpoint": A Socket must be associated with an endpoint using the Bind method
                // Dieses Objekt muss immer neu instanziiert werden pro Kommunikation.
                this.listener.Bind(localEndPoint);
                // Länge der Warteliste für Anfragen, die versch. Clienten stellen.
                // Es wird je Durchlauf nur ein Client abgearbeitet
                this.listener.Listen(maxRequestLimit);

                log.Info("Server Socket geöffnet");
            }
            catch (Exception e) {
                log.Debug(e.ToString());
            }
        }
        /// <summary>
        /// Beendet den Server
        /// </summary>
        public void Stop() {
            this.listener.Close(); // Entbindet alle Ressourcen für den aktuellen Socket
            this.listener.Dispose(); // Entbindet alle Ressourcen für die aktuelle listener-Instanz
            TcpServer.alreadyStarted = false;
            log.Info("Socket Shutdown completed");
        }
        public void Accept(ConnectionAcceptedCallback _newConnectionEstablishedCallback, CommunicationEventClerk clerk, CancellationTokenSource cancelToken) {
            try {
                // Programm stoppt hier bis eine Verbindung aufgebaut wird.
                log.Debug("Stopping Thread " + Thread.CurrentThread.ManagedThreadId+
                    " and waiting for a connection... ThreadId= " + Thread.CurrentThread.ManagedThreadId);
                Socket handler = this.listener.Accept();
                log.Debug("Resuming Thread " + Thread.CurrentThread.ManagedThreadId);
                
                _newConnectionEstablishedCallback();
                log.Trace("connection established in TCPserver ThreadId= " + Thread.CurrentThread.ManagedThreadId);
                bool closeConnection = false;
                closeConnection = clerk.PublishEvent_CheckForCancelConnection();
                while (!closeConnection) {

                    log.Trace("Kontrolpunkt 3 [Start des Empfangs-Sende-Loop] ThreadId= " + Thread.CurrentThread.ManagedThreadId);
                    string receivedData = ReceiveText(handler, clerk);
                    log.Trace("Kontrolpunkt 3.1: Wait 1000");
                    System.Threading.Thread.Sleep(1000);
                    // bug
                    log.Trace("Kontrolpunkt 4: erster Callback CheckForBytesToSend");
                    byte[] bytesToSend = clerk.PublishEvent_CheckForBytesToSend();

                    while (bytesToSend != null && bytesToSend.Length > 0 ) {
                        log.Debug("Kontrolpunkt 5  [Start Sende-Loop] ThreadId= " + Thread.CurrentThread.ManagedThreadId);

                        log.Trace("Sending bytes...");
                        handler.Send(bytesToSend);
                        log.Trace("Kontrolpunkt 5.1: Loop Callback von CheckForBytesToSend");
                        bytesToSend = clerk.PublishEvent_CheckForBytesToSend();
                    }
                    if (ShouldStopMainLoop(clerk)) {
                        closeConnection = true;
                    }
                }
                log.Info("Verbindung zu einem Client wird nun geschlossen.");
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
                log.Info("Verbindungs-Objekte heruntergefahren.");
            }
            catch (Exception e) {
                log.Debug(e.ToString());
            }
            log.Info("Sollte jetzt Thread id{" + Thread.CurrentThread.ManagedThreadId + "} beenden (rufe CancelToken auf).");
            cancelToken.Cancel();
        }

        private bool ShouldStopMainLoop(CommunicationEventClerk clerk) {
            if (clerk.PublishEvent_CheckForCancelConnection() || clerk.flagConnectionShouldClose) {
                // da das PublishEvent eventuell später wieder false ist, gehe ich mit
                // der ODER Verzweigung sicher, dass die Verbindung auch bei einmaligem Signal geschlossen wird.
                clerk.flagConnectionShouldClose = true;
                clerk.flagTransmissionShouldClose = true;
                if (cancelMainLoopStopwatch == null) {
                    log.Debug("Verbindungsabbau: Starte Timeout");
                    cancelMainLoopStopwatch = new Stopwatch();
                    cancelMainLoopStopwatch.Start();
                }
                else {
                    log.Debug("Verbindungsabbau: Timeout schreitet voran");
                    if (cancelMainLoopStopwatch.Elapsed > maxCancelMainLoopLookup) {
                        cancelMainLoopStopwatch.Stop();
                        cancelMainLoopStopwatch = null;
                        log.Debug("Verbindungsabbau: Beenden des Verbindungsloops erfolgt nun");
                        return true;
                    }
                }
            }
            return false;
        }

        private string ReceiveText(Socket handler, CommunicationEventClerk clerk) {

            // Incoming data from the client.
            // receivedData soll nun in ByteStreamClerk ausgewertet werden.
            string receivedData = null;

            byte[] bytes = null;
            bool bytesToReceiveExist = true;
            // Bug
            bool shouldCancelTransmission = clerk.PublishEvent_OnCheckToStopCurrentTransmission();
            log.Debug("receive loop start");
            while (bytesToReceiveExist && !shouldCancelTransmission && !clerk.flagTransmissionShouldClose) {
                log.Trace("Kontrolpunkt 0-1");
                
                bytes = new byte[1024];
                int receivedBytesCount = 0;
                //int receivedBytesCount = handler.Receive(bytes);
                if (useTimeoutForResponse) {
                    log.Trace("Benutze Timeout für Response");
                    IAsyncResult result;
                    Action action = () =>
                    {
                        try {
                            // Exception: Ein Blockierungsvorgang wurde
                            // durch einen Aufruf von WSACancelBlockingCall unterbrochen
                            receivedBytesCount = handler.Receive(bytes);
                        }
                        catch (SocketException e) {
                            log.Warn("Eine Übertragung wurde abgebrochen.");
                        }
                    };
                    result = action.BeginInvoke(null, null);
                    if (result.AsyncWaitHandle.WaitOne(maxResponseWaitTimeout))
                        log.Trace("Method successful.");
                    else
                        log.Debug("Method timed out.");
                    log.Debug("Breche Empfang ab, da Client nichts gesendet hat");
                }
                else {
                    log.Debug("erhalte Response (ohne Timeout)");
                    receivedBytesCount = handler.Receive(bytes);
                }

                log.Trace("Kontrolpunkt 0-2 EVENT PUBLISH BYTES RECEIVED");
                try {
                    clerk.PublishEvent_ReceiveByteArray(bytes, receivedBytesCount);
                }
                catch (Exception e) {
                    throw new InvalidOperationException("Clerk Referenzierung defekt" + e.ToString());
                }
                log.Trace("Kontrolpunkt 1");
                receivedData += Encoding.ASCII.GetString(bytes, 0, receivedBytesCount);
                log.Trace("ThreadID TcpServer = " + Thread.CurrentThread.ManagedThreadId);
                //if (receivedData.IndexOf("<EOF>") > -1) {
                //    endOfFileReached = true;
                //}
                log.Trace("Received Bytes=" + receivedBytesCount);

                int availableBytes = handler.Available;
                if (availableBytes <= 0) {
                    bytesToReceiveExist = false;
                    // Achtung: Falls viele Datenpakete auf einmal geschickt werden, ist auch ein EndOfTransmission nötig
                    // Ansonsten werden die Bytes direkt an die jetzige Sendung angehangen :)
                }
                log.Trace("Kontrolpunkt 2");
                if (clerk.PublishEvent_OnCheckToStopCurrentTransmission()) {
                    shouldCancelTransmission = true;
                    clerk.flagTransmissionShouldClose = true;
                }
                log.Trace("Kontrolpunkt 2-1");
                                 
            }
            log.Trace("Kontrolpunkt 2-2");
            log.Trace("Text received : " + receivedData);

            return receivedData;
        }
    }
}
