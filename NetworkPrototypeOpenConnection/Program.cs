using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkPrototypeOpenConnection {
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
                if (_callback != null) {
                    string receivedValue = _callback(customText);
                }
                else {
                    throw new InvalidOperationException("callbackmethod doesnt work");
                }
                lockTestObject.SetValueWithDelay("Thread altered the locked value", "ServerHandler");
                lockTestObject.Print();
                System.Console.WriteLine("end loop part");
            }
        }

        /// <summary>
        /// Klasse dient zur Demonstration der Locking-Technik
        /// </summary>
        internal class LockTestObject {
            private string threadSafeValue;

            // Zeitspanne, welcher der Thread simuliert zu arbeiten
            private TimeSpan compututationDuration = TimeSpan.FromSeconds(5);

            private LockTestObject() { }
            public LockTestObject(string threadSafeValue) {
                this.threadSafeValue = threadSafeValue;
            }

            public void SetValueWithDelay(string value, string source) {
                object obj = this;
                CheckIfLocked(obj, source);
                // Lock Statement an sich erlaubt es trotzdem die Anwendung zweimal auszuführen.
                lock (obj) {
                    System.Console.WriteLine("warte " + compututationDuration.TotalSeconds + " Sekunden für TestLock Änderung[Threadsource={0}]", source);
                    System.Threading.Thread.Sleep(compututationDuration);
                    this.threadSafeValue = value;
                    System.Console.WriteLine("Lock wieder freigegeben [Threadsource={0}]", source);
                }
            }

            public string GetValueWithDelay(string source) {
                object obj = this;
                CheckIfLocked(obj, source);
                lock (obj) {
                    System.Console.WriteLine("warte " + compututationDuration.TotalSeconds + " Sekunden für TestLock Anzeige [Threadsource={0}]", source);
                    System.Threading.Thread.Sleep(compututationDuration);
                    Console.WriteLine("Lock wieder freigegeben [Threadsource={0}]", source);
                    return this.threadSafeValue;
                }
            }

            private static void CheckIfLocked(object obj, string source) {
                var lockedBySomeoneElse = !Monitor.TryEnter(obj);
                if (lockedBySomeoneElse) {
                    System.Console.WriteLine("#### Achtung ####");
                    System.Console.WriteLine("Versuch den geschützten Bereich zu öffnen durch [Threadsource={0}]", source);
                }
                else {
                    Monitor.Exit(obj); // Muss den Code erneut freigeben.
                }
            }

            public void Print() {
                System.Console.WriteLine("threadsave text = " + this.threadSafeValue);
            }
        }
    }
}
