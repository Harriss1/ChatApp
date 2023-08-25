using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ThreadedServerPrototype {
    internal class TcpServer {
        int maxRequestLimit = 100;
        private Socket listener;
        private static bool alreadyStarted = false;
        private TcpServer() {
        }
        public TcpServer(string ipAddress, string port) {
            // Es ist zwingend hier die Listener Instanz einmalig
            // für das Programm zu instanziieren, ansonsten kommt der FEhler:
            // System.Net.Sockets.SocketException (0x80004005): Normalerweise darf jede
            // Socketadresse (Protokoll, Netzwerkadresse oder Anschluss) nur jeweils
            // einmal verwendet werden
            // Todo:Recherche: bei Thread auch?
            if (alreadyStarted) {
                throw new InvalidOperationException("listener is only allowed to run once during the execution of the server application");
            }
            else {
                alreadyStarted = true;
            }
            try {
                IPAddress endpointIp = IPAddress.Parse(ipAddress);
                int portNum = Int32.Parse(port);
                IPEndPoint localEndPoint = new IPEndPoint(endpointIp, portNum);
                // Create a Socket that will use Tcp protocol
                this.listener = new Socket(endpointIp.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // "Festlegen auf den Endpoint": A Socket must be associated with an endpoint using the Bind method
                // Dieses Objekt muss immer neu instanziiert werden pro Kommunikation.
                this.listener.Bind(localEndPoint);
            }
            catch (Exception e) {
                System.Console.WriteLine(e);
            }
        }
        public void StartListening() {

            try {
                // Specify how many requests a Socket can listen before it gives Server busy response.
                listener.Listen(maxRequestLimit);

                // Programm stoppt hier bis eine Verbindung aufgebaut wird.
                Console.WriteLine("Waiting for a connection...");
                Socket handler = listener.Accept();

                // Incoming data from the client.
                string receivedData = null;
                byte[] bytes = null;
                bool endOfFileReached = false;
                while (!endOfFileReached) {
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    receivedData += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (receivedData.IndexOf("<EOF>") > -1) {
                        endOfFileReached = true;
                    }
                }

                Console.WriteLine("Text received : {0}", receivedData);

                byte[] msg = Encoding.ASCII.GetBytes(receivedData);
                handler.Send(msg);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
