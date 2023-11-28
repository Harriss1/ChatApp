using ChatApp.Server;
using ChatApp.Server.ConnectionManager;
using ChatApp.Server.MessageMediator;
using System;
using System.Text;
using System.Threading;
using static ChatApp.Server.CommunicationEventClerk;

namespace ChatApp.Server.Listener{
    internal class ConnectionManagerService {

        MessageService messageService = new MessageService();
        private LogPublisher log = new LogPublisher("ConnectionManagerService");
        private ServerRunner serverRunner;
        private ServerRunner.OnDefineConnectionClerkEventForEachNewConnection _onEvent_DefineConnectionClerk;
        private ServerRunner.OnAcceptedNewConnectionEvent _onAcceptedNewConnectionEvent;
        private ServerRunner.OnEvent_PublishConnectionThread _onEvent_PublishConnectionThread;

        private static ConnectionRegister connectionRegister = new ConnectionRegister();

        private ConnectionManagerService() { 
            
        }
        public ConnectionManagerService(ServerRunner serverRunner) {
            this.serverRunner = serverRunner;
            _onEvent_DefineConnectionClerk = new ServerRunner.OnDefineConnectionClerkEventForEachNewConnection(OnEvent_DefineConnectionClerk);
            _onAcceptedNewConnectionEvent = new ServerRunner.OnAcceptedNewConnectionEvent(OnEvent_AcceptedNewConnection);
            _onEvent_PublishConnectionThread = new ServerRunner.OnEvent_PublishConnectionThread(OnEvent_PublishStartedThread);
        }

        public void Run() {
            log.Publish("Bereite ThreadRunner.AcceptConnections vor...");
            
            serverRunner.SubscribeTo_OnNewConnectionEvent(_onAcceptedNewConnectionEvent);
            serverRunner.SubscribeTo_PublishConnectionThread(_onEvent_PublishConnectionThread);
            serverRunner.AcceptConnections();
            log.Publish("AcceptConnections ThreadRunner Start erfolgreich abgeschlossen und ersten wartenden Socket im Thread geöffnet.");
        }

        public void ShutdownAllConnections() {
            log.Publish("ShutDown all connections not implemented!");
        }

        public void AbortAllConnections() {
            foreach (Connection conn in connectionRegister.connections) {
                conn.Thread.Abort();
            }
        }

        private void OnEvent_PublishStartedThread(Thread thread) {
            log.Publish("neuer Thread für eine eben geöffnete Verbindung gestartet. ID=" + thread.ManagedThreadId);
            Connection connection = new Connection(thread);
            connectionRegister.Add(connection);
        }

        private void OnEvent_AcceptedNewConnection() {
            log.Publish("Bereite Verbindungsempfang vor...");
            log.Publish("AcceptedNewConnection Event empfangen. Thread ID = " + Thread.CurrentThread.ManagedThreadId);
            // Nach jedem Verbindungsaufbau ist ein neuer ConnectionClerk notwendig.
            serverRunner.SubscribeTo_OnDefineConnectionClerkEvent(_onEvent_DefineConnectionClerk);
        }

        private bool On_CheckCancelConnection() {
            return false;
        }

        private bool On_CheckAbortTransmission() {
            return false;
        }
        //string mirrorMessage;
        private byte[] On_CheckForBytesToSendLoopUntilAllSent() {
            log.Publish("Prüfe ob Server Nachrichten zum versenden hat... ThreadId= " + Thread.CurrentThread.ManagedThreadId);
            //if (mirrorMessage == null) {
            //    msg.Publish("[keine Nachrichten]");
            //    return null;
            //}
            //msg.Publish("Nachricht: " + mirrorMessage);
            //string message = mirrorMessage;
            //mirrorMessage = null;
            //return Encoding.ASCII.GetBytes(message);
            byte[] response = messageService.GetNextOutboxByteArray(connectionRegister.FindConnectionByThread(Thread.CurrentThread.ManagedThreadId));
            if (response == null) {
                log.Publish("[keine Nachrichten]");
                return null;
            } else {
                log.Publish("[Outbox Nachricht gefunden:]");
                log.Publish(ByteConverter.ToString(response, response.Length));
                return response;
            }
            
        }

        private void On_ReceiveByteArray(byte[] bytes, int receivedBytes) {
            string message = "[unable to parse]";
            //log.Debug("On_ReceiveByteArray Message received in ThreadId= " + Thread.CurrentThread.ManagedThreadId);
            //log.Debug("Details: int receivedBytes=" + receivedBytes);
            try {
                message = Encoding.ASCII.GetString(bytes, 0, receivedBytes);
            }
            catch (Exception e) {
                log.Debug("FEHLER On_ReceiveByteArray Kann bytes nicht verarbeiten: " + e.ToString());
            }
            log.Debug("Details: ERHALTEN: " + message);

            log.Debug("Details: int receivedBytes=" + receivedBytes);
            log.Debug("Details: byte[] bytes.Length=" + bytes.Length);
            log.Debug("Details: byte[] bytes.ToString=" + bytes.ToString());
            //mirrorMessage = message;
            messageService.AddByteArrayToInbox(bytes, receivedBytes, 
                connectionRegister.FindConnectionByThread(Thread.CurrentThread.ManagedThreadId));
        }

        private CommunicationEventClerk OnEvent_DefineConnectionClerk() {
            log.Debug("definiere neuen connectionClerk");
            OnEvent_ReceiveByteArray _onReceiveByteArray = new OnEvent_ReceiveByteArray(On_ReceiveByteArray);
            OnEvent_CheckForBytesToSend _onCheckForBytesToSend = new OnEvent_CheckForBytesToSend(On_CheckForBytesToSendLoopUntilAllSent);
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