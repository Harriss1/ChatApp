using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NetworkPrototypeOpenConnection.Server;

namespace NetworkPrototypeOpenConnection {
    internal class Program {

        static void Main(string[] args) {

            // Child-Thread ServerHandler starten
            const string ipAddress = "127.0.0.1";
            const string port = "10015";
            ServerManager serverManager = new ServerManager();
            serverManager.StartServer(ipAddress, port);
            ServerManager.NewConnectionEvent _newConnectionEvent = new ServerManager.NewConnectionEvent(OnNewConnection);
            serverManager.DefineNewConnectionEvent(_newConnectionEvent);

            serverManager.AcceptConnections();



            //ServerManager serverManager = new ServerManager();
            //serverManager.StartServerThreadLambdavised(ipAddress, port);
        }

        private static string ReceiveBytePackageEventHandle(string test) {
            Console.WriteLine("this event was handled from main! testval=" + test);
            return "test";
        }

        private static CommunicationClerk OnNewConnection() {
            CommunicationClerk.OnReceiveBytePackage _event = new CommunicationClerk.OnReceiveBytePackage(ReceiveBytePackageEventHandle);
            return new CommunicationClerk(_event);
        }

    }
}
