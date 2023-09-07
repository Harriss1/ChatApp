using System;
using System.Collections.Generic;
using System.Threading;

/// <summary>
/// Multithreading Prototyp
/// Karl Klotz, IA 121, 7.9.2023
/// 
/// Zuerst werden zwei Threads gestartet: Server und Test
/// Danach wird in der Hauptroutine eine Schleife aller 5 Sekunden durchlaufen.
/// 
/// Die Hauptroutine gibt somit aller 5 Sekunden den Zustand verschiedener Variablen und Objekte aus.
/// 
/// Die Childthreads verändern den Zustand globaler und lokaler Werte in kleineren Zeitabständen kontinuierlich.
/// 
/// Ziel des Serverthreads: Änderung globaler und lokaler Thread-Variablen verfolgen
/// Ziel des Testthreads: Locking eines Codeabschnitts demonstrieren.
/// 
/// Das Locking dient zur Sperrung von Objekten und Codeabschnitten, die von mehreren Threads zeitgleich
/// aufgerufen werden können. Ohne Locking ist die Integrität dieser Daten bei simultanen Zugriff 
/// nicht gewährleistet.
/// </summary>
namespace ThreadedServerPrototype {
    internal class Program {
        // wird von zwei Threads an unterschiedlichen Stellen bearbeitet
        private static string globalText = "start |";

        // wird von allen drei Threads (Haupt und zwei Child) bearbeitet, es soll aber eine Warnung kommen
        // falls das Objekt bereits bearbeitet wird.
        private static LockTestObject lockTestObject = new LockTestObject("startvalue");

        // Nutzung eines Delegate als Funktionspointer
        private delegate string PrintTextCallback(string text);

        private static LocalDataStoreSlot testSlotString = Thread.AllocateNamedDataSlot("testSlotString");

        static void Main(string[] args) {
            const string ipAddress = "127.0.0.1";
            const string port = "10015";

            StartServerThreadLambdavised(ipAddress, port);
            int count = 0;

            // purpose of main thread: running the loop every 5 seconds and printing object states
            while (true) {
                System.Threading.Thread.Sleep(5000);
                System.Console.WriteLine("still active: " + count++);
                if(count == 4) {
                    Thread.SetData(testSlotString, "changed in 4th iteration");
                    System.Console.WriteLine("new value = " + Thread.GetData(testSlotString));
                }
                globalText += "(" + count+")";
                System.Console.WriteLine("[main] global String =" + globalText);
               
                lockTestObject.SetValueWithDelay("main thread altered the locked value", "Main");
                lockTestObject.Print();
            }
        }

        private static void StartServerThreadLambdavised(string ipAddress, string port) {
            PrintTextCallback _printTextCallback = new PrintTextCallback(PrintFancyText);

            // https://stackoverflow.com/questions/1195896/threadstart-with-parameters
            Thread serverHandler = new Thread(() => ServerHandlerThread(ipAddress, port, _printTextCallback));
            serverHandler.Start();
            
        }
        private static void ServerHandlerThread(string ipAddress, string port, PrintTextCallback _callback) {
            // Thread.SetDate let us handle individual variables and objects for each thread sperately
            Thread.SetData(testSlotString, "testStringofthisSlot");
            System.Console.WriteLine("Starting server at: " + ipAddress + ":" + port);
            TcpServer server = new TcpServer(ipAddress, port);
            while (true) {
                System.Console.WriteLine("start loop part");
                // code execution stops here and waits for a client request
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
                lockTestObject.SetValueWithDelay("Thread altered the locked value", "ServerHandler");
                lockTestObject.Print();
                System.Console.WriteLine("end loop part");
            }
        }

        // Method that demonstrates working callback
        public static string PrintFancyText(string text) {
            // https://csharp-video-tutorials.blogspot.com/2014/03/part-91-retrieving-data-from-thread_13.html
            System.Console.WriteLine("**** #### Callback Message ### ****");
            System.Console.WriteLine("\n|\n|\t" + text);
            return "returned Text=" + text;
        }

        [Obsolete("deprecated - instanciates and releases a socket object on each call")]
        private static void StartServerThreadObjectivised(string ipAddress, string port) {
            Thread serverHandler = new Thread(ServerHandler);
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> kvIpAddress = new KeyValuePair<string, string>("ipAddress", ipAddress);
            KeyValuePair<string, string> kvPort = new KeyValuePair<string, string>("port", port);
            parameters.Add(kvIpAddress);
            parameters.Add(kvPort);

            serverHandler.Start(parameters);
        }
        [Obsolete("deprecated")]
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
