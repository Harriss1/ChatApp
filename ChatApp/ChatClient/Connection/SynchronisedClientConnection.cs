using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ChatApp.ChatClient.Connection.Implementation;

namespace ChatApp.ChatClient.Connection {
    internal class SynchronisedClientConnection {
        private static ClientConnection clientConnection;
        private static Thread clientThread;
        private static Thread handlerThread;
        public SynchronisedClientConnection() {
            if (clientConnection != null) {
                throw new InvalidOperationException("[ConnectionThreadHandler] Instanz darf nur einmalig erstellt und verwendet werden.");
            }
            clientConnection = new ClientConnection();
            handlerThread = Thread.CurrentThread;
        }
        internal void StartTcpConnection(string ipAddress, string serverPort) {
            Thread clientHandler = new Thread(() => RunTcpClientLoop(ipAddress, serverPort));
            clientHandler.Start();
            clientThread = clientHandler;
        }
        private void RunTcpClientLoop(string ipAddress, string port) {
            tcpClient.Connect(ipAddress, port);
            bool cancelConnection = false;
            while (!cancelConnection) {
                string outboxMessage = "todo";
                string received = tcpClient.Send(outboxMessage);

                Thread.Sleep(100);
            }
        }
        internal void EnqueueMessageToOutBox(string message) {
            Check_IsTcpClientThreadRunning();
        }
        internal string DequeueMessageFromInbox() {
            Check_IsTcpClientThreadRunning();
            return message;
        }


        private static void Check_IsTcpClientThreadRunning() {
            if (clientThread == null) {
                throw new InvalidOperationException("[TcpClientThread] Der Thread muss zuerst mittels StartTcpConnection gestartet werden.");
            }
        }

        /// <summary>
        /// ////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="onIncommingString"></param>
        /// <exception cref="InvalidOperationException"></exception>
        //public void RegisterToIncommingString(OnIncommingString onIncommingString) {
        //    if (_onIncommingString != null) {
        //        throw new InvalidOperationException("[TcpClientHandler] [Thread Id=" + Thread.CurrentThread.ManagedThreadId + "] " +
        //            "onIncommingString darf nur einmal registriert werden");
        //    }
        //    int currentId = Thread.CurrentThread.ManagedThreadId;
        //    if (currentId == threadIdHandler) {
        //        throw new InvalidOperationException("[TcpClientHandler] [Thread Id=" + Thread.CurrentThread.ManagedThreadId + "] " +
        //            "onIncommingString muss von einem anderen Thread als dem TcpClientHandler-Thread gesetzt werden.\r\n" +
        //            "threadIdTcpClient=" + threadIdHandler + " currentThreadId=" + currentId);
        //    }
        //    _onIncommingString = onIncommingString;
        //}

        //public void DoSomething() {
        //    if (_onIncommingString == null) {
        //        throw new InvalidOperationException("[TcpClient] [Thread Id=" + Thread.CurrentThread.ManagedThreadId + "] " +
        //            "onIncommingString muss vor Nutzung der Verbindung registriert werden.");
        //    }
        //}
    }
}
