using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using ChatApp.Server.Listener;
using ChatApp.Server.ConnectionManager;

namespace ChatApp.Server.Listener {
    /// <summary>
    /// Einzelne Verbindungen werden durch den ConnectionClerk gemanaged.
    /// Für einen GracefullyShutdown ist es nötig alle offenen Verbindungen mittels ConnectionClerk zu schließen.
    /// Ausführungsreihenfolge:
    /// 1: StartServer
    /// 2: Subscribe to OnNewConnectionEvent
    /// 3: Subscribe to PublishConnectionThread
    /// 4: Set OnDefineConnectionEventClerk
    /// --> 4 muss als Unterprozess nach OnNewConnectionEvent ausgeführt werden, da immer ein neuer
    /// CommunicationClerk für jeden Thread notwendig ist.
    /// </summary>
    public class ServerRunner {
        TcpServer tcpServer;
        int threadCounter = 0;
        public delegate void OnAcceptedNewConnectionEvent();
        public delegate CommunicationEventClerk OnDefineConnectionClerkEventForEachNewConnection();
        public delegate void OnEvent_PublishConnectionThread(Thread thread);
        private OnAcceptedNewConnectionEvent _publishAcceptedNewConnectionEvent;
        private OnDefineConnectionClerkEventForEachNewConnection _defineConnectionClerk;
        private OnEvent_PublishConnectionThread _publishConnectionThread;
        private static LogPublisher log = new LogPublisher("ServerRunner");
        private bool alreadyShuttingDown = false;

        private List<Thread> connectionThreads = new List<Thread> ();
        internal static ServerRunner instance;
        private ServerRunner() {}
        internal static ServerRunner GetInstance() {
            object _lock = new object();
            lock (_lock) {
                if (instance == null) {
                    instance = new ServerRunner();
                }
            }
            return instance;
        }

        public void StartServer(string ipAddress, string port) {
            log.Debug("Starting server at: " + ipAddress + ":" + port);            
            tcpServer = new TcpServer();
            tcpServer.StartAndListen(ipAddress,port);
        }
        // Kann eventuell raus?
        public void SubscribeTo_OnNewConnectionEvent(OnAcceptedNewConnectionEvent _newConnectionEvent) {
            // Zweck: mehrere Observer sollen informiert werden können, falls es eine neue Verbindung gibt.
            log.Trace("DELEGATE subscribe SubscribeTo_OnNewConnectionEvent");
            this._publishAcceptedNewConnectionEvent += _newConnectionEvent;
        }
        public void SubscribeTo_PublishConnectionThread(OnEvent_PublishConnectionThread _newThreadEvent) {
            log.Trace("DELEGATE subscribe SubscribeTo_PublishConnectionThread");
            _publishConnectionThread += _newThreadEvent;
        }

        /// <summary>
        /// Kann nur einmal pro Thread registriert werden!
        /// </summary>
        /// <param name="_registerClerkEvent"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void SubscribeTo_OnDefineConnectionClerkEvent(OnDefineConnectionClerkEventForEachNewConnection _registerClerkEvent) {
            if (this._defineConnectionClerk != null) {
                throw new InvalidOperationException("Eine offene Verbindung darf nur von einer Instanz kontrolliert werden.");
            }
            this._defineConnectionClerk = _registerClerkEvent;
            log.Trace("DELEGATE subscribe SubscribeTo_OnDefineConnectionClerkEvent");
        }

        /// <summary>
        /// Startet einen neuen Server-Thread, und immer einen weiteren 
        /// neuen Thread nach erfolgten Verbindungsaufbau mittels Accept()
        /// </summary>
        public void AcceptConnectionsLoop() {
            if (alreadyShuttingDown) {
                return;
            }
            if (_publishAcceptedNewConnectionEvent == null) {
                log.Error("Fehler: NewConnectionEvent ohne Subscriber");
            }
            log.Debug("ThreadID ServerManager.AcceptConnections = " + Thread.CurrentThread.ManagedThreadId);
            log.Debug("ThreadCounter: " + threadCounter++);

            // Starte die eigene Methode bei Erhalt des Callback-Events nach erfolgten Accept (ähnlich einer Rekursion)           
            TcpServer.ConnectionAcceptedCallback _newConnectionAcceptedCallback = new TcpServer.ConnectionAcceptedCallback(AcceptConnectionsLoop);
            Thread.Sleep(delayUntilPublishAccepted);
            _publishAcceptedNewConnectionEvent(); // notwendig um immer einen neuen Callback zu erstellen,
                                                  // welche immer einen neuen Clerk haben.
            log.Trace("NewConnectionEvent() signaled");
            CommunicationEventClerk clerk = CreateClerk();

            log.Trace("DELEGATE subscribe AcceptConnections(): Übergebe Clerk with registered Threads");

            CancellationTokenSource cancelToken = new CancellationTokenSource();
            Thread serverHandler = new Thread(() => tcpServer.Accept(_newConnectionAcceptedCallback, clerk, cancelToken));
            serverHandler.Start();
            log.Info("einzelner Accept-Thread gestartet");
            connectionThreads.Add(serverHandler);
            if (_publishConnectionThread != null) {
                _publishConnectionThread(serverHandler);
            }
            _defineConnectionClerk = null;
            // TODO Abbruchbedingung für rekursives Verhalten
            // TODO Limit für Verbindungen
        }
        private static CommunicationEventClerk lastClerk;
        private int delayUntilPublishAccepted = 1000;

        private CommunicationEventClerk CreateClerk() {
            CommunicationEventClerk newClerk = _defineConnectionClerk();
            if(newClerk == lastClerk) {
                throw new InvalidOperationException("CommunicationClerk muss für jede Verbindung neu erstelt werden. Der benachrichtigende Callback über die Erstellung eines neuen Verbindungssockets ist 'OnAcceptedNewConnectionEvent'");
            }
            lastClerk = newClerk;
            return newClerk;             
        }

        public void Abort() {
            foreach(Thread connection in connectionThreads) {
                connection.Abort();
            }
            tcpServer.Stop();
        }
        public void BeginGracefulShutdown(TimeSpan shutdownTimeout) {
            log.Info("Beginne Gracful Shutdown - maximal erlaubte Zeit (Sekunden) hierfür: " + shutdownTimeout.Seconds);
            alreadyShuttingDown = true;
            CancellationTokenSource cancelToken = new CancellationTokenSource();
            Thread closeWorker = new Thread(() => SecureServerShutdown_Worker(shutdownTimeout, cancelToken));
            closeWorker.Start();
        }
        public bool IsGracefullyShutdown() {
            foreach (Thread connection in connectionThreads) {
                if (connection.IsAlive) {
                    // Thread welcher Accept offen hat fährt nicht herunter...
                    log.Debug("Thread einer Verbindung ist noch offen.");
                    log.Debug("Details: connection ThreadID=" + connection.ManagedThreadId + " ThreadState=" + connection.ThreadState);
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Versucht in einem vorgegebenen Zeitraum zu kontrollieren ob der Server herunter gefahren ist.
        /// </summary>
        private void SecureServerShutdown_Worker(TimeSpan shutdownTimeout, CancellationTokenSource cancelToken) {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            bool success = false;
            bool anyThreadIsStillRunning = true;
            bool stopCalled = false;
            while (stopwatch.Elapsed < shutdownTimeout && anyThreadIsStillRunning) {
                success = true;
                foreach (Thread connectionThread in connectionThreads) {
                    if (connectionThread.IsAlive) {
                        log.Warn("Gracefully Shutdown noch nicht abgeschlossen, Thread einer Verbindung ist noch offen.");
                        log.Warn("Details: " +
                            "connection ThreadID=" + connectionThread.ManagedThreadId + " " +
                            "ThreadState=" + connectionThread.ThreadState + " " +
                            "Zeit verbleibend:" + (shutdownTimeout.Seconds - stopwatch.Elapsed.Seconds)+
                            " elaps=" + stopwatch.Elapsed.Seconds + " timeout=" + shutdownTimeout.Seconds);
                        success = false;
                        if (!(stopwatch.Elapsed > TimeSpan.FromSeconds(10)) ) {
                            
                            ConnectionRegister registry = ConnectionRegister.GetInstance();
                            Connection openConnection = registry.
                                FindConnectionByThread(connectionThread.ManagedThreadId);
                            if (!openConnection.FlaggedToCancel) {
                                openConnection.FlaggedToCancel = true;
                                log.Warn("Verbindung ist nun markiert für Beendigung - Details:");
                                if (openConnection.Client != null) {
                                    log.Warn("Benutzername=" + openConnection.Client.Name);
                                }
                                log.Warn("Thread Id = " + openConnection.Thread.ManagedThreadId);
                            }
                        } else {
                            if (!stopCalled) {
                                tcpServer.Stop();
                                stopCalled = true;
                            }
                        }
                    }
                }
                if (success) {
                    anyThreadIsStillRunning = false;
                    this._defineConnectionClerk = null;
                    this._publishAcceptedNewConnectionEvent = null;
                    this._publishConnectionThread = null;
                }
                Thread.Sleep(4000);
            }
            if (success) {
                log.Info("### ### ### ### ### ### ### ### ### ### ### ### ### ###");
                log.Info("### ");
                log.Info("### Server erfolgreich nach " + stopwatch.Elapsed.Seconds + " Sekunden beendet. ###");
                log.Info("### ");
                log.Info("### ### ### ### ### ### ### ### ### ### ### ### ### ###");
            }
            else {
                log.Error("Server wurde nicht ordnungsgemäß beendet.");
            }
            alreadyShuttingDown = false;
            log.Debug("Beende den Shutdown-Kontroll-Thread.");
            cancelToken.Cancel();
        }

    }
}
