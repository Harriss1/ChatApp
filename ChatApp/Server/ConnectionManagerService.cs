using ChatApp.Server;
using System;
using System.Text;
using System.Threading;
using static ChatApp.Server.CommunicationEventClerk;

namespace ChatApp.Server.Listener{
    internal class ConnectionManagerService {
        private LogPublisher msg = new LogPublisher();
        private ServerRunner serverRunner;
        private ServerRunner.OnDefineConnectionClerkEvent _onEvent_DefineConnectionClerk;
        private ServerRunner.OnAcceptedNewConnectionEvent _newConnectionEvent;
        private ServerRunner.OnEvent_PublishConnectionThread _onEvent_PublishConnectionThread;

        private ConnectionManagerService() { }
        public ConnectionManagerService(ServerRunner serverRunner) {
            this.serverRunner = serverRunner;
            _onEvent_DefineConnectionClerk = new ServerRunner.OnDefineConnectionClerkEvent(OnEvent_DefineConnectionClerk);
            _newConnectionEvent = new ServerRunner.OnAcceptedNewConnectionEvent(OnEvent_AcceptedNewConnection);
            _onEvent_PublishConnectionThread = new ServerRunner.OnEvent_PublishConnectionThread(OnEvent_PublishStartedThread);
        }


        public void Run() {
            msg.Publish("Bereit ThreadRunner.AcceptConnections vor...");
            serverRunner.SubscribeTo_OnDefineConnectionClerkEvent(_onEvent_DefineConnectionClerk);
            serverRunner.SubscribeTo_OnNewConnectionEvent(_newConnectionEvent);
            serverRunner.SubscribeTo_PublishConnectionThread(_onEvent_PublishConnectionThread);
            serverRunner.AcceptConnections();
            msg.Publish("AcceptConnections ThreadRunner gestarted");
        }

        private void OnEvent_PublishStartedThread(Thread thread) {
            msg.Publish("neuer Thread für eine eben geöffnete Verbindung gestartet. ID=" + thread.ManagedThreadId);
        }

        private void OnEvent_AcceptedNewConnection() {
            msg.Publish("Bereite Verbindungsempfang vor...");
        }


        private bool On_CheckCancelConnection() {
            return false;
        }

        private bool On_CheckAbortTransmission() {
            return false;
        }

        private byte[] On_CheckForBytesToSend() {
            msg.Publish("Prüfe ob Server Nachrichten zum versenden hat...");
            string message = "nothing returned <EOF>";
            return Encoding.ASCII.GetBytes(message);
        }

        private void On_ReceiveByteArray(byte[] bytes, int receivedBytes) {
            msg.Publish("Message received:");
            string message = Encoding.ASCII.GetString(bytes, 0, receivedBytes);
            msg.Publish(message);
        }


        private CommunicationEventClerk OnEvent_DefineConnectionClerk() {
            OnEvent_ReceiveByteArray _onReceiveByteArray = new OnEvent_ReceiveByteArray(On_ReceiveByteArray);
            OnEvent_CheckForBytesToSend _onCheckForBytesToSend = new OnEvent_CheckForBytesToSend(On_CheckForBytesToSend);
            OnEvent_CheckAbortTransmission _onCheckToAbortTransmission = new OnEvent_CheckAbortTransmission(On_CheckAbortTransmission);
            OnEvent_CheckCancelConnection _onCheckToCancelConnection = new OnEvent_CheckCancelConnection(On_CheckCancelConnection);

            CommunicationEventClerk clerk = new CommunicationEventClerk(
                _onReceiveByteArray,
                _onCheckForBytesToSend,
                _onCheckToAbortTransmission,
                _onCheckToCancelConnection
                );
            return clerk;
        }
    }
}