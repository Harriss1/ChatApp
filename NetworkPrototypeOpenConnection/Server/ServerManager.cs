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
        public delegate CommunicationClerk NewConnectionEvent();
        private NewConnectionEvent _eventClerk;
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
        public void DefineNewConnectionEvent(NewConnectionEvent _newConnectionEvent) {
            this._eventClerk = _newConnectionEvent;
        }
        /// <summary>
        /// Startet einen neuen Server-Thread, und immer einen weiteren 
        /// neuen Thread nach erfolgten Verbindungsaufbau mittels Accept()
        /// </summary>
        public void AcceptConnections() {

            System.Console.WriteLine("ThreadCounter: " + threadCounter++);

            // Achtung Unterschied Signal zu Callback:
            // Signal kann von jeden Thread erhalten werden, Callback nur von Elternthread
            // Starte die eigene Methode bei Erhalt des Callback-Signals nach erfolgten Accept (ähnlich einer Rekursion)           
            ConnectionAcceptedCallback _newConnectionAcceptedCallback = new ConnectionAcceptedCallback(AcceptConnections);
            CommunicationClerk clerk = _eventClerk();
            Thread serverHandler = new Thread(() => tcpServer.Accept(_newConnectionAcceptedCallback, clerk));
            serverHandler.Start();
            
            // TODO Abbruchbedingung für rekursives Verhalten
            // TODO Limit für Verbindungen
        }
    }
}
