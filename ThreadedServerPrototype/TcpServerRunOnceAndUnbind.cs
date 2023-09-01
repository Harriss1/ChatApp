using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ThreadedServerPrototype {
    /// <summary>
    /// Ziel: Demonstration - korrektes Herunterfahren und Freigeben des Socket.
    /// Dieser Server beendet sich nach jeder einzelnen Listen-Receive Aktion
    /// Natürlich zerstört dies auch die Warteschlange.
    /// Zusätzliche Clienten in der Warteschlange würden also getrennt werden.
    /// Er ist also für einen ChatClienten nicht geeignet.
    /// </summary>
    internal class TcpServerRunOnceAndUnbind {
        int maxRequestLimit = 100;
        private static bool alreadyStarted = false;

        IPAddress endpointIp;
        IPEndPoint localEndPoint;
        private TcpServerRunOnceAndUnbind() {
        }
        public TcpServerRunOnceAndUnbind(string ipAddress, string port) {

            if (false && alreadyStarted) {
                throw new InvalidOperationException("listener is only allowed to run once during the execution of the server application");
            }
            else {
                alreadyStarted = true;
            }
            try {
                this.endpointIp = IPAddress.Parse(ipAddress);
                int portNum = Int32.Parse(port);
                this.localEndPoint = new IPEndPoint(endpointIp, portNum);
                
            }
            catch (Exception e) {
                System.Console.WriteLine(e);
            }
        }
        public void StartListening() {

            Socket listener;
            try {
                // Länge der Warteliste für Anfragen, die versch. Clienten stellen.
                // Es wird je Durchlauf nur ein Client abgearbeitet
                // Create a Socket that will use Tcp protocol
                listener = new Socket(endpointIp.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // "Festlegen auf den Endpoint": A Socket must be associated with an endpoint using the Bind method
                // Dieses Objekt muss immer neu instanziiert werden pro Kommunikation.
                listener.Bind(localEndPoint);
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
                // Close oder Dispose muss aufgerufen werden
                listener.Close(); // Entbindet alle Ressourcen für den aktuellen Socket
                listener.Dispose(); // Entbindet alle Ressourcen für die aktuelle listener-Instanz
                Console.WriteLine("Socket Shutdown completed");
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
