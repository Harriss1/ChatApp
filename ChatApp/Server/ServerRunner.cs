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
    /// 3: Set OnDefineConnectionEventClerk
    /// </summary>
    public class ServerRunner {
        TcpServer tcpServer;
        int threadCounter = 0;
        public delegate void OnAcceptedNewConnectionEvent();
        public delegate CommunicationEventClerk OnDefineConnectionClerkEvent();
        public delegate void OnEvent_PublishConnectionThread(Thread thread);
        private OnAcceptedNewConnectionEvent _publishAcceptedNewConnectionEvent;
        private OnDefineConnectionClerkEvent _defineConnectionClerk;
        private OnEvent_PublishConnectionThread _publishConnectionThread;

        private List<Thread> connectionThreads = new List<Thread> ();
        public ServerRunner() {
        }

        public void StartServer(string ipAddress, string port) {
            System.Console.WriteLine("Starting server at: " + ipAddress + ":" + port);            
            tcpServer = new TcpServer();
            tcpServer.StartAndListen(ipAddress,port);
        }
        // Kann eventuell raus?
        public void SubscribeTo_OnNewConnectionEvent(OnAcceptedNewConnectionEvent _newConnectionEvent) {
            // Zweck: mehrere Observer sollen informiert werden können, falls es eine neue Verbindung gibt.
            this._publishAcceptedNewConnectionEvent += _newConnectionEvent;
        }
        public void SubscribeTo_PublishConnectionThread(OnEvent_PublishConnectionThread _newThreadEvent) {
            _publishConnectionThread += _newThreadEvent;
        }

        /// <summary>
        /// Kann nur einmal registriert werden!
        /// </summary>
        /// <param name="_registerClerkEvent"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void SubscribeTo_OnDefineConnectionClerkEvent(OnDefineConnectionClerkEvent _registerClerkEvent) {
            if (this._defineConnectionClerk != null) {
                throw new InvalidOperationException("Eine offene Verbindung darf nur von einer Instanz kontrolliert werden.");
            }
            this._defineConnectionClerk = _registerClerkEvent;
        }

        /// <summary>
        /// Startet einen neuen Server-Thread, und immer einen weiteren 
        /// neuen Thread nach erfolgten Verbindungsaufbau mittels Accept()
        /// </summary>
        public void AcceptConnections() {
            if (_publishAcceptedNewConnectionEvent == null) {
                Console.WriteLine("Warnung: NewConnectionEvent ohne Subscriber");
            }
            Console.WriteLine("ThreadID ServerManager.AcceptConnections = " + Thread.CurrentThread.ManagedThreadId);
            System.Console.WriteLine("ThreadCounter: " + threadCounter++);

            // Starte die eigene Methode bei Erhalt des Callback-Events nach erfolgten Accept (ähnlich einer Rekursion)           
            TcpServer.ConnectionAcceptedCallback _newConnectionAcceptedCallback = new TcpServer.ConnectionAcceptedCallback(AcceptConnections);
            _publishAcceptedNewConnectionEvent(); // notwendig um immer einen neuen Callback zu erstellen, welcher immer einen neuen Clerk haben.

            CommunicationEventClerk clerk = GetClerk();
            Thread serverHandler = new Thread(() => tcpServer.Accept(_newConnectionAcceptedCallback, clerk));
            serverHandler.Start();
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
                    Console.WriteLine("Warnung: Gracefully Shutdown nicht möglich, Thread einer Verbindung ist noch offen.");
                    return false;
                }
            }
            tcpServer.Stop();
            return true;
        }
    }
}
