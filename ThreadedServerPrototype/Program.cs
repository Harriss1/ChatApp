using System;
using System.Collections.Generic;
using System.Threading;

/// <summary>
/// Multithreading Prototyp
/// Karl Klotz, IA 121, 8.9.2023
/// 
/// Umngesetzte Techniken: Thread-Erstellung, Callbacks, Locking
/// 
/// # Anwendungsfall
/// Das Programm startet einen TCP-Server. Die Server-Routine ist in einem ChildThread implementiert.
/// 
/// Vom Hauptthread "Main" aus wird der Childthread gestartet: ServerHandler
/// Danach wird im Elternthread die Ausführung fortgesetzt und eine Schleife aller 5 Sekunden durchlaufen.
/// 
/// Die Hauptroutine gibt somit aller 5 Sekunden den Zustand verschiedener Variablen und Objekte aus.
/// 
/// Falls der TCP-Server durch einen Clienten angesprochen wird, werden auch Anweisungen im Kinderthread
/// ausgeführt, welche Objekte und Variablen sowohl in Eltern- und Kinderthread verändern.
/// 
/// # Serverthread
/// Der Serverthread verändert den Zustand globaler und lokaler Werte in kleineren Zeitabständen kontinuierlich.
/// 
/// Ziel des Serverthreads:
/// 1) Änderung globaler und lokaler Thread-Variablen verfolgen, hier: globalText und testSlotString
/// 2) Zugriff auf das Objekt "lockTestObject", in dessen Klasse Locking umgesetzt wird
/// 
/// # Locking
/// Das Locking dient zur Sperrung von Objekten und Codeabschnitten, die von mehreren Threads zeitgleich
/// aufgerufen werden können. Ohne Locking ist die Integrität dieser Daten bei simultanen Zugriff 
/// nicht gewährleistet.
/// 
/// # Callbacks
/// Nach Beendigung des Kinderthread ServerHandler wird ein delegate (Pointer auf eine Methode) aufgerufen, welche
/// im Elternthread einige Anweisungen ausführt. Der Callback dient dazu, eine Anweisung im Elternthread auszuführen, sobald
/// der Kinderthread einen bestimmten Fortschritt erreicht hat.
/// </summary>
namespace ThreadedServerPrototype {
    internal class Program {
        // wird von Eltern- und Kind-Thread an unterschiedlichen Stellen bearbeitet
        private static string globalText = "start |";

        // wird von allen beiden Threads simultan bearbeitet, es soll aber eine Warnung kommen
        // falls das Objekt bereits bearbeitet wird.
        private static LockTestObject lockTestObject = new LockTestObject("startvalue");

        // Nutzung eines Delegate als Funktionspointer
        // Zweck ist ein Callback vom ServerHandler-Thread zu implementieren
        private delegate string PrintTextCallback(string text);

        // Nutzung der Slot-Bibliothek, welche Objekte für jeden Thread separiert
        private static LocalDataStoreSlot testSlotString = Thread.AllocateNamedDataSlot("testSlotString");

        static void Main(string[] args) {
            // Child-Thread ServerHandler starten
            const string ipAddress = "127.0.0.1";
            const string port = "10015";
            StartServerThreadLambdavised(ipAddress, port);

            // Die Hauptschleife wird wartet bei jeden Durchlauf 5 Sekunden
            // Danach zeigt sie den Zustand der Variablen und Objekte an
            int loopCount = 0;
            while (true) {
                System.Threading.Thread.Sleep(5000);
                System.Console.WriteLine("still active: " + loopCount++);
                // Änderung des testSlotString-Objekts
                // Die Ausgabe zeigt an, dass das Objekt unabhängig vom ServerHandler-Thread geändert wird.
                if(loopCount == 4) {
                    Thread.SetData(testSlotString, "changed in 4th iteration");
                    System.Console.WriteLine("new value = " + Thread.GetData(testSlotString));
                }
                globalText += "(" + loopCount+")";
                System.Console.WriteLine("[main] global String =" + globalText);
               
                lockTestObject.SetValueWithDelay("main thread altered the locked value", "Main");
                lockTestObject.Print();
            }
        }

        private static void StartServerThreadLambdavised(string ipAddress, string port) {
            PrintTextCallback _printTextCallback = new PrintTextCallback(PrintFancyText);

            // Zur Erstellung des ServerHandler-Threads wurde kein delegate genutzt.
            // Mit der Lambda-Funktion passiert dies im Hintergrund
            // Grund: Durch die Lambda-Implementierung ist es möglich mehr als einen
            // Parameter an die Thread-Methode zu übergeben, und diese Parameter müssen
            // dann auch keine Objekte von der Klasse "object" sein
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

        /// <summary>
        /// Implementierungsart, welche mittels object-Klasse die Parameter übergibt.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        [Obsolete("deprecated in favor of lambda-implementation")]
        private static void StartServerThreadObjectivised(string ipAddress, string port) {
            // Erstellung der Parameter-Liste
            // Es wird hier ersichtlich, dass die Parameter aufwendig durch eine KeyValuePair-Liste
            // an den Kinderthread "ServerHandler" übergeben werden müssen.
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> kvIpAddress = new KeyValuePair<string, string>("ipAddress", ipAddress);
            KeyValuePair<string, string> kvPort = new KeyValuePair<string, string>("port", port);
            parameters.Add(kvIpAddress);
            parameters.Add(kvPort);

            Thread serverHandler = new Thread(ServerHandler);
            serverHandler.Start(parameters);
        }
        [Obsolete("deprecated")]
        private static void ServerHandler(Object parameters) {
            string ipAddress = null;
            string port = null;
            // Prüfung der Parameter-Liste im parameters-Objekt
            // Eine Prüfung der übergebenen Parameter findet hier statt, um sicherzustellen,
            // dass die Schnittstelle zum Kinderthread korrekte Parameterdaten übermittelt
            // Hier wird ersichtlich, dass der object-parameter weiteren Code verursacht
            // und die Implementierung ohne lambda-Methodik wesentlich aufwendiger ist.
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
