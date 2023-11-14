using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NetworkPrototypeOpenConnection.Server.Listener;
using static NetworkPrototypeOpenConnection.Server.Listener.TcpServer;

namespace NetworkPrototypeOpenConnection.Server {
    public class ServerManager {
        TcpServer tcpServer;
        int threadCounter = 0;
        public delegate void OnNewConnectionEvent();
        public delegate CommunicationEventClerk OnRegisterConnectionClerkEvent();
        private OnNewConnectionEvent _publishNewConnectionEvent;
        private OnRegisterConnectionClerkEvent _onRegisterConnectionClerkEvent;
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
        public void SubscribeToOnNewConnectionEvent(OnNewConnectionEvent _newConnectionEvent) {
            // Zweck: mehrere Observer sollen informiert werden können, falls es eine neue Verbindung gibt.
            this._publishNewConnectionEvent += _newConnectionEvent;
        }
        public void TakeOnRegisterConnectionClerkEvent(OnRegisterConnectionClerkEvent _registerClerkEvent) {
            if (this._onRegisterConnectionClerkEvent != null) {
                throw new InvalidOperationException("Eine offene Verbindung darf nur von einer Instanz kontrolliert werden.");
            }
            this._onRegisterConnectionClerkEvent = _registerClerkEvent;
        }
        /// <summary>
        /// Startet einen neuen Server-Thread, und immer einen weiteren 
        /// neuen Thread nach erfolgten Verbindungsaufbau mittels Accept()
        /// </summary>
        public void AcceptConnections() {
            if (_publishNewConnectionEvent == null) {
                Console.WriteLine("Es wurde nicht definiert was bei Verbindungsanfragen geschehen soll");
            }
            Console.WriteLine("ThreadID ServerManager.AcceptConnections = " + Thread.CurrentThread.ManagedThreadId);
            System.Console.WriteLine("ThreadCounter: " + threadCounter++);

            // Achtung Unterschied Signal zu Callback:
            // Signal kann von jeden Thread erhalten werden, Callback nur von Elternthread
            // Starte die eigene Methode bei Erhalt des Callback-Signals nach erfolgten Accept (ähnlich einer Rekursion)           
            ConnectionAcceptedCallback _newConnectionAcceptedCallback = new ConnectionAcceptedCallback(AcceptConnections);
            _publishNewConnectionEvent();
            CommunicationEventClerk clerk = _onRegisterConnectionClerkEvent();
            Thread serverHandler = new Thread(() => tcpServer.Accept(_newConnectionAcceptedCallback, clerk));
            serverHandler.Start();
            _onRegisterConnectionClerkEvent = null;
            // TODO Abbruchbedingung für rekursives Verhalten
            // TODO Limit für Verbindungen
        }
    }
}
