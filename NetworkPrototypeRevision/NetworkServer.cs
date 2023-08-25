using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkPrototypeRevision {
    internal class NetworkServerOld {
        int maxRequestLimit = 100;
        
        public NetworkServerOld() {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAdress">Format: 123.123.123.123</param>
        /// <param name="port">Format: 12345</param>
        public void StartListening(string ipAddress, string port) {
            IPAddress endpointIp = IPAddress.Parse(ipAddress);
            int portNum = Int32.Parse(port);
            IPEndPoint localEndPoint = new IPEndPoint(endpointIp, portNum);

            try {
                // Create a Socket that will use Tcp protocol
                Socket listener = new Socket(endpointIp.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // "Festlegen auf den Endpoint": A Socket must be associated with an endpoint using the Bind method
                // Dieses Objekt muss immer neu instanziiert werden pro Kommunikation.
                listener.Bind(localEndPoint);

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

        /// <summary>
        /// Original Beispielcode
        /// </summary>
        public void StartListeningStatic() {
            // Get Host IP Address that is used to establish a connection
            // In this case, we get one IP address of localhost that is IP : 127.0.0.1
            // If a host has multiple addresses, you will get a list of addresses
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 3);

            try {

                // Create a Socket that will use Tcp protocol
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // A Socket must be associated with an endpoint using the Bind method
                listener.Bind(localEndPoint);
                // Specify how many requests a Socket can listen before it gives Server busy response.
                // We will listen 10 requests at a time
                listener.Listen(10);

                Console.WriteLine("Waiting for a connection...");
                Socket handler = listener.Accept();

                // Incoming data from the client.
                string data = null;
                byte[] bytes = null;

                while (true) {
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1) {
                        break;
                    }
                }

                Console.WriteLine("Text received : {0}", data);

                byte[] msg = Encoding.ASCII.GetBytes(data);
                handler.Send(msg);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();

        }
    }
}