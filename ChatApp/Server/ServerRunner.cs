using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ChatApp.Server.Listener;

namespace ChatApp.Server {
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

        private List<Thread> connectionThreads = new List<Thread> ();
        public ServerRunner() {
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
        public void AcceptConnections() {
            if (_publishAcceptedNewConnectionEvent == null) {
                log.Error("Fehler: NewConnectionEvent ohne Subscriber");
            }
            log.Debug("ThreadID ServerManager.AcceptConnections = " + Thread.CurrentThread.ManagedThreadId);
            log.Debug("ThreadCounter: " + threadCounter++);

            // Starte die eigene Methode bei Erhalt des Callback-Events nach erfolgten Accept (ähnlich einer Rekursion)           
            TcpServer.ConnectionAcceptedCallback _newConnectionAcceptedCallback = new TcpServer.ConnectionAcceptedCallback(AcceptConnections);
            Thread.Sleep(1000);
            _publishAcceptedNewConnectionEvent(); // notwendig um immer einen neuen Callback zu erstellen, welcher immer einen neuen Clerk haben.
            log.Trace("NewConnectionEvent() signaled");
            CommunicationEventClerk clerk = GetClerk();

            log.Trace("DELEGATE subscribe AcceptConnections(): Übergebe Clerk with registered Threads");
            Thread serverHandler = new Thread(() => tcpServer.Accept(_newConnectionAcceptedCallback, clerk));
            serverHandler.Start();
            log.Info("einzelner Accept-Thread gestartet");
            connectionThreads.Add(serverHandler);
            if (_publishConnectionThread != null)
                _publishConnectionThread(serverHandler);
            _defineConnectionClerk = null;
            // TODO Abbruchbedingung für rekursives Verhalten
            // TODO Limit für Verbindungen
        }
        private static CommunicationEventClerk lastClerk;
        private CommunicationEventClerk GetClerk() {
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

        public bool GracefullyShutdown() {
            foreach (Thread connection in connectionThreads) {
                if(connection.IsAlive) {
                    log.Error("Fehler: Gracefully Shutdown nicht möglich, Thread einer Verbindung ist noch offen.");
                    return false;
                }
            }
            tcpServer.Stop();
            return true;
        }
    }
}
