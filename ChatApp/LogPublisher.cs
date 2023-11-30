using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp {
    internal class LogPublisher {
        public delegate void OnEvent_PublishServerMessage(string message);
        private static OnEvent_PublishServerMessage _publishServerMessage;
        string sourceIdentifier = "";
        enum Level {
            TRACE,
            DEBUG,
            INFO,
            WARN,
            ERROR
        }
        public LogPublisher() {
        }
        public LogPublisher(string sourceIdentifier) {
            this.sourceIdentifier = "[" + sourceIdentifier + "] ";
        }

        public static void SubscribeTo_PublishServerMessage(OnEvent_PublishServerMessage _publishServerMessage) {
            LogPublisher._publishServerMessage = _publishServerMessage;
            //LogPublisher._publishServerMessage += _publishServerMessage;
        }
        public void Publish(string message) {
            if (message.Contains("Details:")) {
                Console.WriteLine("kurz vorm senden");
            }
            Console.WriteLine("[Log " +
                System.DateTime.Now.TimeOfDay + "][" +
                "Thread="
                +Thread.CurrentThread.ManagedThreadId+"]" + 
                sourceIdentifier + 
                message);
            if (_publishServerMessage != null) {
                if (message.Contains("Details:")) {
                    Console.WriteLine("sende es...");
                }
                _publishServerMessage(message);

                if (message.Contains("Details:")) {
                    Console.WriteLine("habs gesendet!");
                }
            }
            else {
                if (message.Contains("Details:")) {
                    Console.WriteLine("NULL BEREICH");
                }
            }
        }

        internal void Error(string message) {
            if (LogLevel() >= Level.ERROR) {
                Publish(message);
            }
        }
        internal void Warn(string message) {
            if (LogLevel() >= Level.WARN) {
                Publish(message);
            }
        }
        internal void Info(string message) {
            if (LogLevel() >= Level.INFO) {
                Publish(message);
            }
        }

        internal void Debug(string message) {
            if (LogLevel() >= Level.DEBUG) {
                Publish(message);
            }
        }

        internal void Trace(string message) {
            if(LogLevel() >= Level.TRACE) {
                Publish(message);
            }
        }

        private Level LogLevel() {
            switch (Config.LogLevel) {
                case "trace":
                    return Level.TRACE;
                case "debug":
                    return Level.DEBUG;
                case "info":
                    return Level.INFO;
                case "warn":
                    return Level.WARN;
                case "error":
                    return Level.ERROR;
                default: return Level.INFO;
            }
        }
    }
}
