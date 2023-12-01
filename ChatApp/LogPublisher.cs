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
        private Level level;
        enum Level {
            TRACE = 0,
            DEBUG = 1,
            INFO = 2,
            WARN= 3,
            ERROR = 4,
        }
        private LogPublisher() { }
        public LogPublisher(string sourceIdentifier) {
            this.sourceIdentifier = sourceIdentifier;
            level = ReadLevelFromConfig();
        }

        public static void SubscribeTo_PublishServerMessage(OnEvent_PublishServerMessage _publishServerMessage) {
            LogPublisher._publishServerMessage = _publishServerMessage;
            //LogPublisher._publishServerMessage += _publishServerMessage;
        }
        private void Publish(string message) {
            Console.WriteLine("[Log " +
                System.DateTime.Now.TimeOfDay + "][" +
                "Thread="
                +Thread.CurrentThread.ManagedThreadId+"]" + 
                "["+ sourceIdentifier + "]" +
                message);
            if (_publishServerMessage != null) {
                _publishServerMessage(message);
            }
            else {
                if (message.Contains("Details:")) {
                    Console.WriteLine("NULL BEREICH");
                }
            }
        }

        internal void Error(string message) {
            if (level <= Level.ERROR) {
                Publish("[ERROR] " + message);
            }
        }
        internal void Warn(string message) {
            if (level <= Level.WARN) {
                Publish("[WARN] " + message);
            }
        }
        internal void Info(string message) {
            if (level <= Level.INFO) {
                Publish("[INFO] " + message);
            }
        }

        internal void Debug(string message) {
            if (level <= Level.DEBUG) {
                Publish("[DEBUG] " + message);
            }
        }

        internal void Trace(string message) {
            if(level <= Level.TRACE) {
                Publish("[TRACE] " + message);
            }
        }

        private Level ReadLevelFromConfig() {
            switch (Config.LogLevel) {
                case "trace":
                    Info("LogLevel=TRACE");
                    return Level.TRACE;
                case "debug":
                    Info("LogLevel=DEBUG");
                    return Level.DEBUG;
                case "info":
                    Info("LogLevel=INFO");
                    return Level.INFO;
                case "warn":
                    Info("LogLevel=WARN");
                    return Level.WARN;
                case "error":
                    Info("LogLevel=ERROR");
                    return Level.ERROR;
                default:
                    Info("LogLevel=default(INFO)"); 
                    return Level.INFO;
            }
        }
    }
}
