using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkPrototypeOpenConnection {
    internal class TcpServer {
        private const int maxRequestLimit = 100;
        private Socket listener;
        private static bool alreadyStarted = false;
        private TcpServer() {
        }
        /// <summary>
        /// Startet den Server und beginnt den Port abzuhören.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        public TcpServer(string ipAddress, string port) {
            StartAndListen(ipAddress, port);
        }

        public void StartAndListen(string ipAddress, string port) {
            // Es ist zwingend hier die Listener Instanz einmalig
            // für das Programm zu instanziieren, ansonsten kommt der FEhler:
            // System.Net.Sockets.SocketException (0x80004005): Normalerweise darf jede
            // Socketadresse (Protokoll, Netzwerkadresse oder Anschluss) nur jeweils
            // einmal verwendet werden
            // Es geht darum, dass der Socket auch nicht verwendbar ist, falls er wieder freigegeben wurde.
            // Todo:Recherche: bei Thread auch?
            if (alreadyStarted) {
                throw new InvalidOperationException("already started - listener is only allowed to run once during the execution of the server application");
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
                // Länge der Warteliste für Anfragen, die versch. Clienten stellen.
                // Es wird je Durchlauf nur ein Client abgearbeitet
                this.listener.Listen(maxRequestLimit);
            }
            catch (Exception e) {
                System.Console.WriteLine(e);
            }
        }
        /// <summary>
        /// Beendet den Server
        /// </summary>
        public void Stop() {
            this.listener.Close(); // Entbindet alle Ressourcen für den aktuellen Socket
            this.listener.Dispose(); // Entbindet alle Ressourcen für die aktuelle listener-Instanz
            TcpServer.alreadyStarted = false;
            Console.WriteLine("Socket Shutdown completed");
        }
        public void Accept() {
            try {
                // Programm stoppt hier bis eine Verbindung aufgebaut wird.
                Console.WriteLine("Waiting for a connection...");
                Socket handler = this.listener.Accept();
                bool abortCondition = false;
                while (!abortCondition) {
                    string receivedData = ReceiveText(handler);
                    if(CheckTextForQuitMessage(receivedData) || CheckForDisconnectEvent()) {
                        abortCondition = true;
                    }
                }
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }

        private bool CheckForDisconnectEvent() {
            throw new NotImplementedException();
        }
        public void Logoff(string username) {

        }

        private bool CheckTextForQuitMessage(string receivedData) {
            if(receivedData.Contains("exit") || receivedData.Contains("<MessageType>logout</MessageType>")) {
                return true;
            }
            return false;
        }

        private static string ReceiveText(Socket handler) {

            // Hier würde ja ein neuer Thread gestartet werden?
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
            return receivedData;
        }
    }
}
