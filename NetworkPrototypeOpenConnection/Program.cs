using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NetworkPrototypeOpenConnection.Server;

namespace NetworkPrototypeOpenConnection {
    internal class Program {
        static List<string>  sendToAllClients = new List<string>();
        static ServerManager serverManager;
        static void Main(string[] args) {
            Console.WriteLine("ThreadID Main at Start = " + Thread.CurrentThread.ManagedThreadId);
            // Child-Thread ServerHandler starten
            const string ipAddress = "127.0.0.1";
            const string port = "10015";
            serverManager = new ServerManager();
            serverManager.StartServer(ipAddress, port);
            ServerManager.OnNewConnectionEvent _newConnectionEvent = new ServerManager.OnNewConnectionEvent(OnNewConnection);
            serverManager.SubscribeToOnNewConnectionEvent(_newConnectionEvent);

            serverManager.AcceptConnections();
            for(int i = 0; i<10; i++) {

                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("ThreadID Main in Waitloop = " + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("Nachricht an alle <3");
                string inputMessage = Console.ReadLine();
                sendToAllClients.Add(inputMessage);
            }


            //ServerManager serverManager = new ServerManager();
            //serverManager.StartServerThreadLambdavised(ipAddress, port);
        }

        private static string ReceiveBytePackageEventHandle(string test) {
            return "test";
        }
        
        private static void OnNewConnection() {
            CommunicationEventClerk.OnReceiveString _receiveStringEvent = new CommunicationEventClerk.OnReceiveString(ReceiveBytePackageEventHandle);
            CommunicationEventClerk.OnReceivedByteArray _receiveBytesEvent
                = new CommunicationEventClerk.OnReceivedByteArray(OnBytesReceived);
            CommunicationEventClerk.OnCheckForBytesToSendEvent _checkBytesToSendEvent =
                new CommunicationEventClerk.OnCheckForBytesToSendEvent(OnCheckForBytesToSendEvent);
            CommunicationEventClerk clerk = new CommunicationEventClerk(_receiveStringEvent, _receiveBytesEvent, _checkBytesToSendEvent);

            ServerManager.OnRegisterConnectionClerkEvent _registerConnectionClerk = new ServerManager.OnRegisterConnectionClerkEvent(() =>
            {
                return clerk;
            });
            serverManager.TakeOnRegisterConnectionClerkEvent(_registerConnectionClerk);

        }

        private static void OnBytesReceived(byte[] bytes, int receivedBytes) {
            Console.WriteLine("this event was handled from main.OnBytesReceived testval=" + Encoding.ASCII.GetString(bytes, 0, receivedBytes));
        }

        private static byte[] OnCheckForBytesToSendEvent() {
            byte[] bytes = new byte[1024];
            return bytes;
        }
    }
}
