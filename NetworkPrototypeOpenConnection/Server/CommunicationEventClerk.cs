﻿using System;
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

        public delegate string OnEvent_ReceiveString(string test);
        private OnEvent_ReceiveString _onReceiveAppendString;

        public delegate void OnEvent_ReceiveByteArray(byte[] bytes, int receivedBytes);
        public delegate byte[] OnEvent_CheckForBytesToSend();
        public delegate bool OnEvent_CheckToStopCurrentTransmission();
        public delegate bool OnEvent_CheckToCancelConnection();

        private OnEvent_ReceiveByteArray _onReceiveByteArray;
        private OnEvent_CheckForBytesToSend _onCheckForBytesToSend;
        private OnEvent_CheckToStopCurrentTransmission _onCheckToStopCurrentTransmission;
        private OnEvent_CheckToCancelConnection _onCheckToCancelConnection;

        public CommunicationEventClerk(
            OnEvent_ReceiveString _onReceiveAppendString,

            OnEvent_ReceiveByteArray _onReceiveByteArray,
            OnEvent_CheckForBytesToSend _onCheckForBytesToSend,
            OnEvent_CheckToStopCurrentTransmission _onCheckToStopCurrentTransmission,
            OnEvent_CheckToCancelConnection _onCheckToCancelConnection)
        {
            this._onReceiveAppendString = _onReceiveAppendString;

            this._onReceiveByteArray = _onReceiveByteArray;
            this._onCheckForBytesToSend = _onCheckForBytesToSend;
            this._onCheckToStopCurrentTransmission = _onCheckToStopCurrentTransmission;
            this._onCheckToCancelConnection = _onCheckToCancelConnection;
        }

        public string PublishEvent_AppendString(string test) {
            _onReceiveAppendString(test);
            return "EventHandler String";
        }

        public void PublishEvent_ReceiveByteArray(byte[] bytes, int receivedBytes) {
            _onReceiveByteArray(bytes, receivedBytes);
        }
        public byte[] PublishEvent_CheckForBytesToSend() {
            return _onCheckForBytesToSend();
        }

        public bool PublishEvent_CheckForCancelConnection() {
            return _onCheckToCancelConnection();
        }

        public bool PublishEvent_OnCheckToStopCurrentTransmission() {
            return _onCheckToStopCurrentTransmission();
        }
    }
}