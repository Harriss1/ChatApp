﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

// Zusätzlicher Namespace um zu erklären, dass dies eine Unterklasse ist
namespace NetworkPrototypeOpenConnection.Server.Listener {
    // Rename to TcpListener?
    internal class TcpServer {
        private const int maxRequestLimit = 100;
        private Socket listener;
        private static bool alreadyStarted = false;

        public delegate void ConnectionAcceptedCallback();
        
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
            }
            catch (Exception e) {
                System.Console.WriteLine(e);
            }
        }
        /// <summary>
        /// Beendet den Server
        /// </summary>
        public void Stop() {
            this.listener.Close(); // Entbindet alle Ressourcen für den aktuellen Socket
            this.listener.Dispose(); // Entbindet alle Ressourcen für die aktuelle listener-Instanz
            TcpServer.alreadyStarted = false;
            Console.WriteLine("Socket Shutdown completed");
        }
        public void Accept(ConnectionAcceptedCallback _newConnectionEstablishedCallback, CommunicationEventClerk clerk) {
            try {
                // Programm stoppt hier bis eine Verbindung aufgebaut wird.
                Console.WriteLine("Waiting for a connection...");
                Socket handler = this.listener.Accept();
                _newConnectionEstablishedCallback();
                Console.WriteLine("connection established in TCPserver");
                bool closeConnection = false;
                closeConnection = clerk.PublishEvent_CheckForCancelConnection();
                while (!closeConnection) {
                    
                    string receivedData = ReceiveText(handler, clerk);
                    Console.WriteLine("Wait 1000");
                    System.Threading.Thread.Sleep(1000);
                    
                    // bug?
                    byte[] bytesToSend = clerk.PublishEvent_CheckForBytesToSend();
                    while (bytesToSend != null && bytesToSend.Length > 0) {
                        handler.Send(bytesToSend);
                        bytesToSend = clerk.PublishEvent_CheckForBytesToSend();
                    }
                    if (CheckTextForQuitMessage(receivedData)
                        || CheckForDisconnectEvent()
                        || clerk.PublishEvent_CheckForCancelConnection()) {
                        closeConnection = true;
                    }
                }
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }

        private bool CheckForDisconnectEvent() {
            return false;
        }

        private bool CheckTextForQuitMessage(string receivedData) {
            if(receivedData.Contains("quit") || receivedData.Contains("<MessageType>logout</MessageType>")) {
                return true;
            }
            return false;
        }

        private string ReceiveText(Socket handler, CommunicationEventClerk clerk) {

            // Incoming data from the client.
            // receivedData soll nun in ByteStreamClerk ausgewertet werden.
            string receivedData = null;

            byte[] bytes = null;
            bool endOfFileReached = false;
            // Bug
            bool cancelTransmission = clerk.PublishEvent_OnCheckToStopCurrentTransmission();
            System.Console.WriteLine("receive loop start");
            while (!endOfFileReached || cancelTransmission) {
                                
                bytes = new byte[1024];
                int bytesRec = handler.Receive(bytes);
                clerk.PublishEvent_ReceiveByteArray(bytes, bytesRec);
                Console.WriteLine("ran loop once");
                receivedData += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                Console.WriteLine("ThreadID TcpServer = " + Thread.CurrentThread.ManagedThreadId);
                clerk.PublishEvent_AppendString(receivedData);
                if (receivedData.IndexOf("<EOF>") > -1) {
                    endOfFileReached = true;
                }
                cancelTransmission = clerk.PublishEvent_OnCheckToStopCurrentTransmission();
            }

            Console.WriteLine("Text received : {0}", receivedData);

            byte[] msg = Encoding.ASCII.GetBytes(receivedData);
            handler.Send(msg);
            return receivedData;
        }

        private string TransmittString(string text) {
            return "from tcpserver, text parameter=" + text;
        }
    }
}