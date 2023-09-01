using System;
using System.Collections.Generic;
using System.Threading;

namespace ThreadedServerPrototype {
    internal class Program {
        private static string globalText = "start |";

        private delegate string PrintTextCallback(string text);
        
        static void Main(string[] args) {
            const string ipAddress = "127.0.0.1";
            const string port = "10015";

            //StartServerThreadObjectivised(ipAddress, port);
            StartServerThreadLambdavised(ipAddress, port);
            int count = 0;
            while (true) {
                System.Threading.Thread.Sleep(5000);
                System.Console.WriteLine("still active: " + count++);
                if(count == 4) {
                    Thread.SetData(testSlotString, "changed in 4th iteration");
                    System.Console.WriteLine("new value = " + Thread.GetData(testSlotString));
                }
                globalText += "(" + count+")";
                System.Console.WriteLine("[main] global String =" + globalText);
            }
            //TcpServer tcpServer = new TcpServer(ipAddress, port);
            //while (true) {
            //    tcpServer.StartListening();
            //}
        }

        private static LocalDataStoreSlot testSlotString = Thread.AllocateNamedDataSlot("testSlotString");
        private static void StartServerThreadLambdavised(string ipAddress, string port) {
            PrintTextCallback _printTextCallback = new PrintTextCallback(PrintFancyText);

            // https://stackoverflow.com/questions/1195896/threadstart-with-parameters
            Thread serverHandler = new Thread(() => ServerHandlerThread(ipAddress, port, _printTextCallback));
            serverHandler.Start();
            
        }
        // Method that demonstrates working callback
        public static string PrintFancyText(string text) {
            // https://csharp-video-tutorials.blogspot.com/2014/03/part-91-retrieving-data-from-thread_13.html
            System.Console.WriteLine("**** #### Callback Message ### ****");
            System.Console.WriteLine("\n|\n|\t" + text);
            return "returned Text=" + text;
        }
        private static void ServerHandlerThread(string ipAddress, string port, PrintTextCallback _callback) {
            Thread.SetData(testSlotString, "testStringofthisSlot");
            System.Console.WriteLine("Starting server at: " + ipAddress + ":" + port);
            TcpServer server = new TcpServer(ipAddress, port);
            while (true) {
                System.Console.WriteLine("start loop part");
                //TcpServerRunOnceAndUnbind tcpServer = new TcpServerRunOnceAndUnbind(ipAddress, port);
                //tcpServer.StartListening();
                server.Accept();
                string retrievedString = (string)Thread.GetData(testSlotString);
                System.Console.WriteLine("serverHandler: retrievedStr=" + retrievedString);
                globalText += " one_loop |";
                System.Console.WriteLine("[thread] global String =" + globalText);
                string customText = "customtext=" + globalText;
                if(_callback != null) {
                    string receivedValue = _callback(customText);
                } else {
                    throw new InvalidOperationException("callbackmethod doesnt work");
                }
                System.Console.WriteLine("end loop part");
            }
        }

        // f. object-start
        private static void StartServerThreadObjectivised(string ipAddress, string port) {
            Thread serverHandler = new Thread(ServerHandler);
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> kvIpAddress = new KeyValuePair<string, string>("ipAddress", ipAddress);
            KeyValuePair<string, string> kvPort = new KeyValuePair<string, string>("port", port);
            parameters.Add(kvIpAddress);
            parameters.Add(kvPort);

            serverHandler.Start(parameters);
        }
        private static void ServerHandler(Object parameters) {
            string ipAddress = null;
            string port = null;
            foreach (KeyValuePair<string, string> parameter in (List<KeyValuePair<string, string>>)parameters) {
                if (parameter.Key.Equals("ipAddress")) {
                    ipAddress = parameter.Value;
                }
                if (parameter.Key.Equals("port")) {
                    port = parameter.Value;
                }
            }
            if (ipAddress == null || port == null) {
                throw new InvalidOperationException("ipAdress and port must be in the list of kvPairs as parameters");
            }
            System.Console.WriteLine("Starting server at: " + ipAddress + ":" + port);
            while (true) {
                TcpServerRunOnceAndUnbind tcpServer = new TcpServerRunOnceAndUnbind(ipAddress, port);
                tcpServer.StartListening();
            }
        }
    }
}
