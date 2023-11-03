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
            serverManager.AcceptConnections();
            //ServerManager serverManager = new ServerManager();
            //serverManager.StartServerThreadLambdavised(ipAddress, port);
        }


    }
}
