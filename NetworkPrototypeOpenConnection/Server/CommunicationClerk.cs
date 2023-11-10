using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkPrototypeOpenConnection {
    public class CommunicationClerk {

        public delegate string OnReceiveBytePackage(string test);
        private OnReceiveBytePackage _onReceiveBytePackage;
        public CommunicationClerk(
            OnReceiveBytePackage _onReceiveBytePackage) {
            this._onReceiveBytePackage = _onReceiveBytePackage;
        }

        public string EventHandler(string test) {
            _onReceiveBytePackage(test);
            return "EventHanlder STring";
        }

    }
}
