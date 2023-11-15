using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Server {
    internal class LogPublisher {
        public delegate void OnEvent_PublishServerMessage(string message);
        private static OnEvent_PublishServerMessage _publishServerMessage;
        public LogPublisher() {
        }

        public static void SubscribeTo_PublishServerMessage(OnEvent_PublishServerMessage _publishServerMessage) {
            LogPublisher._publishServerMessage += _publishServerMessage;
        }
        public void Publish(string message) {
            _publishServerMessage(message);
        }
    }
}
