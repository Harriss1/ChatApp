using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Server {
    // Falls dies ein Listener wäre, wäre das Ziel: TcpServer soll von den Objekten die ihm zuhören unabhängig bleiben.
    // Daten über Listener Schnittstelle an TcpServer zu senden fühlt sich falsch an, da mehrere Zuhören könnten
    // Namen: Subscriber, ClientCommunicationEvents, ClientCommunicationListener, CommunicationBridge, Mediator
    // 
    public class CommunicationEventClerk {
        public bool flagConnectionShouldClose = false;
        public bool flagTransmissionShouldClose = false;

        public delegate void OnEvent_ReceiveByteArray(byte[] bytes, int receivedBytes);
        /// <summary>
        /// Wiederholung der Prüfung auf Bytes to Send
        /// Ende der Prüfungsschleife falls:
        /// 1) Null übergeben wird
        /// 2) Das übergebene Array eine Länge von 0 hat.
        /// </summary>
        /// <returns></returns>
        public delegate byte[] OnEvent_CheckForBytesToSend();
        public delegate bool OnEvent_CheckAbortTransmission();
        public delegate bool OnEvent_CheckCancelConnection();

        private OnEvent_ReceiveByteArray _onReceiveByteArray;
        private OnEvent_CheckForBytesToSend _onCheckForBytesToSend;
        private OnEvent_CheckAbortTransmission _onCheckAbortTransmission;
        private OnEvent_CheckCancelConnection _onCheckToCancelConnection;

        public CommunicationEventClerk(
            OnEvent_ReceiveByteArray _onReceiveByteArray,
            OnEvent_CheckForBytesToSend _onCheckForBytesToSend,
            OnEvent_CheckAbortTransmission _onCheckToAbortTransmission,
            OnEvent_CheckCancelConnection _onCheckToCancelConnection)
        {

            this._onReceiveByteArray = _onReceiveByteArray;
            this._onCheckForBytesToSend = _onCheckForBytesToSend;
            this._onCheckAbortTransmission = _onCheckToAbortTransmission;
            this._onCheckToCancelConnection = _onCheckToCancelConnection;
        }

        public void PublishEvent_ReceiveByteArray(byte[] bytes, int receivedBytes) {
            _onReceiveByteArray(bytes, receivedBytes);
        }
        public byte[] PublishEvent_CheckForBytesToSend() {
            return _onCheckForBytesToSend();
        }

        /// <summary>
        /// Dieses Event prüft, ob eine einzelne Verbindung zw. Client und Server beendet werden soll.
        /// 
        /// Nach Beenden der Verbindung sollte auch der zugehörige Thread für den einzelnen Client beendet werden.
        /// </summary>
        /// <returns></returns>
        public bool PublishEvent_CheckForCancelConnection() {
            return _onCheckToCancelConnection();
        }

        public bool PublishEvent_OnCheckToStopCurrentTransmission() {
            return _onCheckAbortTransmission();
        }
    }
}
