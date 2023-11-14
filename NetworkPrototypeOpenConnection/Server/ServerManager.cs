using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NetworkPrototypeOpenConnection.Server.Listener;
using static NetworkPrototypeOpenConnection.Server.Listener.TcpServer;

namespace NetworkPrototypeOpenConnection.Server {
    /// <summary>
    /// Einzelne Verbindungen werden durch den ConnectionClerk gemanaged.
    /// Für einen GracefullyShutdown ist es nötig alle offenen Verbindungen mittels ConnectionClerk zu schließen.
    /// Ausführungsreihenfolge:
    /// 1: StartServer
    /// 2: Subscribe to OnNewConnectionEvent
    /// 3: Set OnDefineConnectionEventClerk
    /// </summary>
    public class ServerManager {
        TcpServer tcpServer;
        int threadCounter = 0;
        public delegate void OnAcceptedNewConnectionEvent();
        public delegate CommunicationEventClerk OnDefineConnectionClerkEvent();
        private OnAcceptedNewConnectionEvent _publishAcceptedNewConnectionEvent;
        private OnDefineConnectionClerkEvent _defineConnectionClerk;
        private List<Thread> connectionThreads = new List<Thread> ();
        public ServerManager() {
        }

        public void StartServer(string ipAddress, string port) {
            System.Console.WriteLine("Starting server at: " + ipAddress + ":" + port);            
            tcpServer = new TcpServer();
            tcpServer.StartAndListen(ipAddress,port);
        }

        // Nutzung eines Delegate als Funktionspointer
        // Zweck ist ein Callback vom ServerHandler-Thread zu implementieren
        private delegate string PrintTextCallback(string text);
        public void SubscribeToOnNewConnectionEvent(OnAcceptedNewConnectionEvent _newConnectionEvent) {
            // Zweck: mehrere Observer sollen informiert werden können, falls es eine neue Verbindung gibt.
            this._publishAcceptedNewConnectionEvent += _newConnectionEvent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_registerClerkEvent"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void SetOnDefineConnectionClerkEvent(OnDefineConnectionClerkEvent _registerClerkEvent) {
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
            ConnectionAcceptedCallback _newConnectionAcceptedCallback = new ConnectionAcceptedCallback(AcceptConnections);
            _publishAcceptedNewConnectionEvent();
            CommunicationEventClerk clerk = _defineConnectionClerk();
            Thread serverHandler = new Thread(() => tcpServer.Accept(_newConnectionAcceptedCallback, clerk));
            serverHandler.Start();
            connectionThreads.Add(serverHandler);
            _defineConnectionClerk = null;
            // TODO Abbruchbedingung für rekursives Verhalten
            // TODO Limit für Verbindungen
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
                    Console.WriteLine("Warnng: Gracefully Shutdown nicht möglich, Thread einer Verbindung ist noch offen.");
                    return false;
                }
            }
            tcpServer.Stop();
            return true;
        }
    }
}
