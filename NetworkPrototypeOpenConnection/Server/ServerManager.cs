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
        public ServerManager() { }

        public void StartServer(string ipAddress, string port) {
            System.Console.WriteLine("Starting server at: " + ipAddress + ":" + port);
            tcpServer = new TcpServer();
            tcpServer.StartAndListen(ipAddress,port);
        }

        // Nutzung eines Delegate als Funktionspointer
        // Zweck ist ein Callback vom ServerHandler-Thread zu implementieren
        private delegate string PrintTextCallback(string text);

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

            Thread serverHandler = new Thread(() => tcpServer.Accept(_newConnectionAcceptedCallback));
            serverHandler.Start();
            
            // TODO Abbruchbedingung für rekursives Verhalten
            // TODO Limit für Verbindungen
        }





        /////////////////////////////////////////////////////////////////
        /// ungenutzter Code

        public void StartServerThread(string ipAddress, string port) {
            PrintTextCallback _printTextCallback = new PrintTextCallback(PrintFancyText);

            // https://stackoverflow.com/questions/1195896/threadstart-with-parameters
            Thread serverHandler = new Thread(() => ServerHandlerThread(ipAddress, port, _printTextCallback));
            serverHandler.Start();

        }

        private static void ServerHandlerThread(string ipAddress, string port, PrintTextCallback _callback) {
            System.Console.WriteLine("Starting server at: " + ipAddress + ":" + port);
            TcpServer server = new TcpServer();
            server.StartAndListen(ipAddress, port);
            while (true) {
                System.Console.WriteLine("start loop part");
                // code execution stops here and waits for a client request
                //server.Accept();

                if (_callback != null) {
                    string receivedValue = _callback("nothing");
                }
                else {
                    throw new InvalidOperationException("callbackmethod doesnt work");
                }
                System.Console.WriteLine("end loop part");
            }
        }
        /// <summary>
        /// Dient zur Demonstration des Aufruf des Callbacks aus dem Kinderthread heraus
        /// Quelle: https://csharp-video-tutorials.blogspot.com/2014/03/part-91-retrieving-data-from-thread_13.html
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string PrintFancyText(string text) {
            // Die folgenden Anweisungen werden im Elternthread ausgeführt
            System.Console.WriteLine("**** #### Callback Message ### ****");
            System.Console.WriteLine("\n|\n|\t" + text);
            return "returned Text=" + text;
        }
    }
}
