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

        public delegate void OnReceivedByteArray(byte[] bytes, int receivedBytes);
        public delegate byte[] OnCheckForBytesToSendEvent();
        public delegate bool OnCheckIsStopCurrentTransmissionEvent();
        public delegate bool OnCheckIsCancelConnectionEvent();

        private OnReceivedByteArray _onReceivedByteArray;
        private OnCheckForBytesToSendEvent _onCheckForBytesToSendEvent;
        private OnCheckIsStopCurrentTransmissionEvent _onCheckIsStopCurrentTransmissionEvent;
        private OnCheckIsCancelConnectionEvent _onCheckIsCancelConnectionEvent;

        public CommunicationEventClerk(
            OnReceiveString _onReceiveAppendString,

            OnReceivedByteArray _onReceiveByteArray,
            OnCheckForBytesToSendEvent _onCheckForBytesToSendEvent)
        {
            this._onReceiveAppendString = _onReceiveAppendString;

            this._onReceivedByteArray = _onReceiveByteArray;
            this._onCheckForBytesToSendEvent = _onCheckForBytesToSendEvent;
        }

        public string OnAppendStringEvent(string test) {
            _onReceiveAppendString(test);
            return "EventHandler String";
        }

        public void OnReceiveByteArray(byte[] bytes, int receivedBytes) {
            _onReceivedByteArray(bytes, receivedBytes);
        }
        public byte[] OnCheckForBytesToSend() {
            return _onCheckForBytesToSendEvent();
        }

        //public byte[] OnCheckForBytesToSendEvent() {
        //    return _onCheckForBytesToSendEvent();
        //}

        public void RegisterListener(OnCheckForBytesToSendEvent _dele) {
        
        }
}
}
