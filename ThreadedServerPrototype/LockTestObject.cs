using System;
using System.Threading;

namespace ThreadedServerPrototype {
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
            lock (obj) {
                System.Console.WriteLine("warte "+ compututationDuration.TotalSeconds+ " Sekunden für TestLock Änderung[Threadsource={0}]", source);
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