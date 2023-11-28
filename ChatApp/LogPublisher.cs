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

        internal void Debug(string v) {
            Publish(v);
        }
    }
}