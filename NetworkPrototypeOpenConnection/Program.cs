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
        private static int action = 1;

        static void Main(string[] args) {
            Console.WriteLine("ThreadID Main at Start = " + Thread.CurrentThread.ManagedThreadId);
            // Child-Thread ServerHandler starten
            const string ipAddress = "127.0.0.1";
            const string port = "10015";
            serverManager = new ServerManager();
            serverManager.StartServer(ipAddress, port);
            ServerManager.OnAcceptedNewConnectionEvent _newConnectionEvent 
                = new ServerManager.OnAcceptedNewConnectionEvent(OnNewConnection);
            serverManager.SubscribeTo_OnNewConnectionEvent(_newConnectionEvent);

            serverManager.AcceptConnections();

            for(int i = 0; i<10; i++) {

                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("ThreadID Main in Waitloop = " + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("Nachricht an alle <3");
                string inputMessage = Console.ReadLine();
                sendToAllClients.Add(inputMessage);
            }
        }

        private static string ReceiveBytePackageEventHandle(string test) {
            return "test";
        }
        
        private static void OnNewConnection() {
            CommunicationEventClerk.OnEvent_ReceiveString _receiveStringEvent 
                = new CommunicationEventClerk.OnEvent_ReceiveString(ReceiveBytePackageEventHandle);
            CommunicationEventClerk.OnEvent_ReceiveByteArray _receiveBytesEvent
                = new CommunicationEventClerk.OnEvent_ReceiveByteArray(OnBytesReceived);
            CommunicationEventClerk.OnEvent_CheckForBytesToSend _checkBytesToSendEvent 
                = new CommunicationEventClerk.OnEvent_CheckForBytesToSend(OnCheckForBytesToSendEvent);

            CommunicationEventClerk.OnEvent_CheckCancelConnection _cancelConnectionEvent
                = new CommunicationEventClerk.OnEvent_CheckCancelConnection(OnCheck_CancelConnection);

            CommunicationEventClerk.OnEvent_CheckAbortTransmission _checkAbortTransmissionEvent
                // großes Problem: Lesbarkeit!
                // ich habe hier OnCheckShouldCancelConnection als Parameter übergeben
                // kann ich dies mit einem Interface vermeiden?
                = new CommunicationEventClerk.OnEvent_CheckAbortTransmission(OnCheck_AbortTransmission);

            CommunicationEventClerk clerk = new CommunicationEventClerk(
                    _receiveStringEvent,
                    _receiveBytesEvent,
                    _checkBytesToSendEvent,
                    _checkAbortTransmissionEvent,
                    _cancelConnectionEvent);

            ServerManager.OnDefineConnectionClerkEvent _registerConnectionClerk = new ServerManager.OnDefineConnectionClerkEvent(() =>
            {
                return clerk;
            });
            serverManager.SetOnDefineConnectionClerkEvent(_registerConnectionClerk);

        }

        private static bool OnCheck_CancelConnection() {
            Console.WriteLine("## Counter Increase ##");
            if (action++ == 5) {
                Console.WriteLine("########## CancelConnection #############");
                return true;
            }
            return false;
        }

        private static bool OnCheck_AbortTransmission() {
            return false;
        }
        private static byte[] receivedBytes;
        private static byte[] bytesToSend;
        private static void OnBytesReceived(byte[] bytes, int receivedBytesCount) {
            bytesToSend = bytes;
            Console.WriteLine("this event was handled from main.OnBytesReceived testval=" + Encoding.ASCII.GetString(bytes, 0, receivedBytesCount));
            //if(action++ == 5) Thread.CurrentThread.Abort();
        }

        private static byte[] OnCheckForBytesToSendEvent() {
            byte[] bytes = bytesToSend;
            bytesToSend = null;
            return bytes;
        }
    }
}
