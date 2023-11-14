using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkPrototypeOpenConnection.Server {
    // Falls dies ein Listener wäre, wäre das Ziel: TcpServer soll von den Objekten die ihm zuhören unabhängig bleiben.
    // Daten über Listener Schnittstelle an TcpServer zu senden fühlt sich falsch an, da mehrere Zuhören könnten
    // Namen: Subscriber, ClientCommunicationEvents, ClientCommunicationListener, CommunicationBridge, Mediator
    // 
    public class CommunicationEventClerk {

        public delegate string OnReceiveString(string test);
        private OnReceiveString _onReceiveAppendString;

        public delegate void OnReceiveByteArrayEvent(byte[] bytes, int receivedBytes);
        public delegate byte[] OnCheckForBytesToSendEvent();
        public delegate bool OnCheckIsStopCurrentTransmissionEvent();
        public delegate bool OnCheckIsCancelConnectionEvent();

        private OnReceiveByteArrayEvent _onReceiveByteArray;
        private OnCheckForBytesToSendEvent _onCheckForBytesToSendEvent;
        private OnCheckIsStopCurrentTransmissionEvent _onCheckIsStopCurrentTransmissionEvent;
        private OnCheckIsCancelConnectionEvent _onCheckIsCancelConnectionEvent;

        public CommunicationEventClerk(
            OnReceiveString _onReceiveAppendString,

            OnReceiveByteArrayEvent _onReceiveByteArray,
            OnCheckForBytesToSendEvent _onCheckForBytesToSendEvent,
            OnCheckIsCancelConnectionEvent _onCheckIsCancelConnectionEvent)
        {
            this._onReceiveAppendString = _onReceiveAppendString;

            this._onReceiveByteArray = _onReceiveByteArray;
            this._onCheckForBytesToSendEvent = _onCheckForBytesToSendEvent;
            this._onCheckIsCancelConnectionEvent = _onCheckIsCancelConnectionEvent;
        }

        public string PublishAppendStringEvent(string test) {
            _onReceiveAppendString(test);
            return "EventHandler String";
        }

        public void PublishReceiveByteArray(byte[] bytes, int receivedBytes) {
            _onReceiveByteArray(bytes, receivedBytes);
        }
        public byte[] PublishCheckForBytesToSend() {
            return _onCheckForBytesToSendEvent();
        }

        public bool PublishCheckForCancelConnectionEvent() {
            return _onCheckIsCancelConnectionEvent();
        }
    }
}
