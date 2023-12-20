using ChatApp.Server;
using ChatApp.Server.ConnectionManager;
using ChatApp.Server.MessageMediator;
using System;
using System.Text;
using System.Threading;
using ChatApp.Server.Listener;
using static ChatApp.Server.Listener.CommunicationEventClerk;

namespace ChatApp.Server.Listener {
    internal class ConnectionManagerService {

        private static ConnectionRegister connectionRegister = ConnectionRegister.GetInstance();
        MessageService messageService = MessageService.GetInstance();
        private LogPublisher log = new LogPublisher("ConnectionManagerService");
        private ServerRunner serverRunner;
        private ServerRunner.OnDefineConnectionClerkEventForEachNewConnection _onEvent_DefineConnectionClerk;
        private ServerRunner.OnAcceptedNewConnectionEvent _onAcceptedNewConnectionEvent;
        private ServerRunner.OnEvent_PublishConnectionThread _onEvent_PublishConnectionThread;

        private ConnectionManagerService() { 
            
        }
        public ConnectionManagerService(ServerRunner serverRunner) {
            this.serverRunner = serverRunner;
            _onEvent_DefineConnectionClerk = new ServerRunner.OnDefineConnectionClerkEventForEachNewConnection(OnEvent_DefineConnectionClerk);
            _onAcceptedNewConnectionEvent = new ServerRunner.OnAcceptedNewConnectionEvent(OnEvent_AcceptedNewConnection);
            _onEvent_PublishConnectionThread = new ServerRunner.OnEvent_PublishConnectionThread(OnEvent_PublishStartedThread);
        }

        public void Run() {
            log.Debug("Bereite ThreadRunner.AcceptConnections vor...");
            
            serverRunner.SubscribeTo_OnNewConnectionEvent(_onAcceptedNewConnectionEvent);
            serverRunner.SubscribeTo_PublishConnectionThread(_onEvent_PublishConnectionThread);
            serverRunner.AcceptConnectionsLoop();
            log.Debug("AcceptConnections ThreadRunner Start erfolgreich abgeschlossen und ersten wartenden Socket im Thread geöffnet.");
        }

        public void AbortAllConnections() {
            foreach (Connection conn in connectionRegister.connections) {
                conn.Thread.Abort();
            }
        }

        private void OnEvent_PublishStartedThread(Thread thread) {
            log.Info("neuer Thread für eine eben geöffnete Verbindung gestartet. ID=" + thread.ManagedThreadId);
            Connection connection = new Connection(thread);
            connectionRegister.Add(connection);
        }

        private void OnEvent_AcceptedNewConnection() {
            log.Debug("Bereite Verbindungsempfang vor...");
            log.Debug("AcceptedNewConnection Event empfangen. Thread ID = " + Thread.CurrentThread.ManagedThreadId);
            // Nach jedem Verbindungsaufbau ist ein neuer ConnectionClerk notwendig.
            serverRunner.SubscribeTo_OnDefineConnectionClerkEvent(_onEvent_DefineConnectionClerk);
        }
        private bool On_CheckCancelConnection() {
            // TODO <issue>1</issue>
            Connection currentConnection = connectionRegister.FindConnectionByThread(Thread.CurrentThread.ManagedThreadId);
            if (currentConnection == null) {
                log.Warn("Kann Verbindung nicht beenden, da sie nicht im connectionRegister exisitert");
                return false;
            }
            return currentConnection.FlaggedToCancel;
        }

        private bool On_CheckAbortTransmission() {
            Connection currentConnection = connectionRegister.FindConnectionByThread(Thread.CurrentThread.ManagedThreadId);
            if (currentConnection == null) {
                log.Warn("Kann Verbindung nicht beenden, da sie nicht im connectionRegister exisitert");
                return false;
            }
            return currentConnection.FlaggedToCancel;
        }
        private byte[] On_CheckForBytesToSendLoopUntilAllSent() {
            log.Debug("Prüfe ob Server Nachrichten zum versenden hat... ThreadId= " + Thread.CurrentThread.ManagedThreadId);
            byte[] response = messageService.GetNextOutboxByteArray(connectionRegister.FindConnectionByThread(Thread.CurrentThread.ManagedThreadId));
            if (response == null) {
                log.Debug("[keine Nachrichten]");
                return null;
            } else {
                log.Debug("[Outbox Nachricht gefunden:]");
                log.Debug(ByteConverter.ToString(response, response.Length));
                return response;
            }
            
        }

        private void On_ReceiveByteArray(byte[] bytes, int receivedBytes) {
            string message = "[unable to parse]";
            log.Trace("On_ReceiveByteArray Message received in ThreadId= " + Thread.CurrentThread.ManagedThreadId);
            try {
                message = Encoding.ASCII.GetString(bytes, 0, receivedBytes);
            }
            catch (Exception e) {
                log.Warn("FEHLER On_ReceiveByteArray Kann bytes nicht verarbeiten: " + e.ToString());
                return;
            }
            if (message == null ||
                message.Equals("") ||
                message.Length <= Config.MessageMinLength // Mindestlänge für Protkollkonformität
                ) {
                log.Warn("Leeres ByteArray erhalten");
                return;
            }

            log.Debug("Details: ERHALTEN: " + message);
            log.Trace("Details: int receivedBytes=" + receivedBytes);
            log.Trace("Details: byte[] Länge=" + bytes.Length);
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