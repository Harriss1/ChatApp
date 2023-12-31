﻿using System;
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
        const char overwriteDelimiter = '.';
        int maxGapLength = 25;
        bool publishToSubscribers = true;
        bool alwaysPublishLogToSubscribers = Config.alwaysPublishLogToSubscribers;
        Level level;
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
        public LogPublisher(string sourceIdentifier, bool publishToSubscribers) {
            this.sourceIdentifier = sourceIdentifier;
            if (alwaysPublishLogToSubscribers) {
                this.publishToSubscribers = true;
            } else {
                this.publishToSubscribers = publishToSubscribers;
            }
            level = ReadLevelFromConfig();
        }

        public static void SubscribeTo_PublishServerMessage(OnEvent_PublishServerMessage _publishServerMessage) {
            LogPublisher._publishServerMessage = _publishServerMessage;
            //LogPublisher._publishServerMessage += _publishServerMessage;
        }
        private void Publish(string message) {
            string time = (System.DateTime.Now.TimeOfDay).ToString().Substring(0, 8);
            int gapLength = maxGapLength - sourceIdentifier.Length;
            string gap = "";
            for (int i = 0; i < gapLength; i++) {
                gap += " ";
            }
            string threadNumber = Thread.CurrentThread.ManagedThreadId.ToString();
            if(threadNumber.Length == 1) {
                threadNumber = " " + threadNumber;
            }
            string entryText =
                "[" + time + "]" +
                "(" + threadNumber + ")" +
                "[" + sourceIdentifier + "]" +
                gap +
                message;
            Console.WriteLine(entryText);
            if (_publishServerMessage != null && publishToSubscribers) {
                _publishServerMessage(entryText);
            }
        }
        
        internal void Error(string message) {
            if (level <= Level.ERROR) {
                Publish("[ERROR] " + message);
            }
        }
        internal void Warn(string message) {
            if (level <= Level.WARN) {
                Publish(" [WARN] " + message);
            }
        }
        internal void Info(string message) {
            if (level <= Level.INFO) {
                Publish(" [INFO] " + message);
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
            string logLevelText = Config.LogLevel;
            string overwriteLogLevelText = SearchOverwriteLogLevelTrigger();
            if ( overwriteLogLevelText != null) {
                logLevelText = overwriteLogLevelText;
                Publish(" [INFO] Overwrite LogLevel Eintrag gefunden: Level [" + logLevelText + "] für Quellklasse: " + sourceIdentifier);
            }
            switch (logLevelText) {
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
        private string SearchOverwriteLogLevelTrigger() {
            foreach (string overwriteText in Config.OverwriteLogLevel) {
                string customSourceIdentifier = overwriteText.Split(overwriteDelimiter)[0];
                string customLogLevel = overwriteText.Split(overwriteDelimiter)[1];
                if (customSourceIdentifier.Equals(sourceIdentifier)) {
                    return customLogLevel;
                }
            }
            return null;
        }
    }
}
