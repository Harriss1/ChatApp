using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadedServerPrototype {
    internal class Program {
        static void Main(string[] args) {
            const string ipAddress = "127.0.0.1";
            const string port = "10015";


            StartServerThreadObjectivised(ipAddress, port);
            while (true) {
                System.Threading.Thread.Sleep(5000);
                System.Console.WriteLine("still active");
            }
            //TcpServer tcpServer = new TcpServer(ipAddress, port);
            //while (true) {
            //    tcpServer.StartListening();
            //}
        }

        private static void StartServerThreadObjectivised(string ipAddress, string port) {
            Thread serverHandler = new Thread(ServerHandler);
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> kvIpAddress = new KeyValuePair<string, string>("ipAddress", ipAddress);
            KeyValuePair<string, string> kvPort = new KeyValuePair<string, string>("port", port);
            parameters.Add(kvIpAddress);
            parameters.Add(kvPort);

            serverHandler.Start(parameters);
        }
        private static void StartServerThreadLambdavised(string ipAddress, string port) {
            // https://stackoverflow.com/questions/1195896/threadstart-with-parameters
            Thread serverHandler = new Thread(() => ServerHandler(ipAddress, port));
            serverHandler.Start();
        }
        private static void ServerHandler(Object parameters) {
            string ipAddress = null;
            string port = null;
            foreach(KeyValuePair<string, string> parameter in (List<KeyValuePair<string, string>>) parameters) {
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
                TcpServerInstance tcpServer = new TcpServerInstance(ipAddress, port);
                tcpServer.StartListening();
            }
        }
        private static void ServerHandler(string ipAddress, string port) {
            System.Console.WriteLine("Starting server at: " + ipAddress + ":" + port);
            while (true) {
                TcpServerInstance tcpServer = new TcpServerInstance(ipAddress, port);
                tcpServer.StartListening();
            }
        }
    }
}
