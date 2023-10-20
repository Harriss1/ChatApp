using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkPrototypeOpenConnection {
    internal class ServerManager {
        TcpServer server;
        int threadCounter = 0;
        public ServerManager() { }
        public ServerManager(string ipAddress, string port) {
            this.server = new TcpServer(ipAddress, port);
            System.Console.WriteLine("Starting server at: " + ipAddress + ":" + port);
        }

        // Nutzung eines Delegate als Funktionspointer
        // Zweck ist ein Callback vom ServerHandler-Thread zu implementieren
        private delegate string PrintTextCallback(string text);

        public void StartServerThreadLoop() {
            System.Console.WriteLine("ThreadCounter: " + threadCounter++);
            TcpServer.ConnectionAcceptedCallback _acceptedSignal = new TcpServer.ConnectionAcceptedCallback(StartServerThreadLoop);

            Thread serverHandler = new Thread(() => AcceptConnection(_acceptedSignal));
            serverHandler.Start();
            
            

        }
        private void AcceptConnection(TcpServer.ConnectionAcceptedCallback _signal) {
            server.Accept(_signal);
        }

        public void StartServerThreadLambdavised(string ipAddress, string port) {
            PrintTextCallback _printTextCallback = new PrintTextCallback(PrintFancyText);

            // https://stackoverflow.com/questions/1195896/threadstart-with-parameters
            Thread serverHandler = new Thread(() => ServerHandlerThread(ipAddress, port, _printTextCallback));
            serverHandler.Start();

        }

        private static void ServerHandlerThread(string ipAddress, string port, PrintTextCallback _callback) {
            System.Console.WriteLine("Starting server at: " + ipAddress + ":" + port);
            TcpServer server = new TcpServer(ipAddress, port);
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
