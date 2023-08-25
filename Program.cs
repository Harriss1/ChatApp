using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Netzwerk Prototyp mittels Sockets Ansatz
/// Es handelt sich nicht um den vom Lehrer vorgegeben TCPClient Code
/// Es wird aber eine gewöhnliche TCP Verbindung aufgebaut.
/// 
/// Client und Server Code Quelle:
/// https://www.c-sharpcorner.com/article/socket-programming-in-C-Sharp/
/// Karl Klotz, 25.8.2023
/// </summary>
namespace NetworkPrototypeRevision {
    internal class Program {
        static void Main(string[] args) {
            int localhostPort = 10015;
            string localhostAddress = "127.0.0.1";
            System.Console.WriteLine("Network Prototype: Chat via sending string-messages");
            // IP-Adressen zur Einrichtung des Clienten oder Servers sowie zum Debuggen
            System.Console.WriteLine("IP-Adresses of this computer:");
            PrintIpAdresses();

            // Auswahl Client/Server, dynamic/static
            System.Console.WriteLine("\nPress Key:");
            System.Console.WriteLine(
                "W - configure and start Server\n" +
                "S - use preset: "+ localhostAddress + ":"+ localhostPort + "and start Server" +
                "\n\n" +
                "D - configure and start Client\n" +
                "C - use preset:  " + localhostAddress + ":" + localhostPort + " and start Client\n");
            ConsoleKey key = System.Console.ReadKey().Key;

            // Configure Client Connection
            if (key == ConsoleKey.D) {
                System.Console.WriteLine("CLIENT (configure and start) selected\n");
                System.Console.WriteLine("\tIP-Adress (example: 123.123.123.123):");
                String ipAdress = Console.ReadLine();

                System.Console.WriteLine("\tPort (example: 10010):");
                String port = Console.ReadLine();

                do {
                    System.Console.WriteLine("Message to send:");
                    String content = Console.ReadLine();

                    Console.WriteLine("Message:\n\t" + content + "\tAdress+Port: " + ipAdress + ":" + port + "\n");

                    NetworkClient client = new NetworkClient();
                    client.Send(ipAdress, port, content); 
                    Console.WriteLine("Press Any Key to send next message or ESC to Stop");
                    key = System.Console.ReadKey().Key;
                }
                while(key !=ConsoleKey.Escape);

            }

            // localhost:10010 client connection
            if (key == ConsoleKey.C) {
                System.Console.WriteLine("CLIENT (localhost:10010) selected\n");
                
                
                do {
                    System.Console.WriteLine("Message to send:");
                    String content = Console.ReadLine();

                    Console.WriteLine("Message:\n\t" + content + "\tAdress+Port: " + localhostAddress + ":" + localhostPort + "\n");
                    NetworkClient client = new NetworkClient();
                    client.Send(localhostAddress, localhostPort.ToString(), content);
                    Console.WriteLine("Press Any Key to send next message or ESC to Stop");
                    key = System.Console.ReadKey().Key;
                }
                while (key != ConsoleKey.Escape);
            }

            // configure and start Server
            if (key == ConsoleKey.W) {
                System.Console.WriteLine("SERVER (configure and start) selected\n");
                System.Console.WriteLine("IP-Adress (example: 123.123.123.123):");
                String ipAdress = Console.ReadLine();
                System.Console.WriteLine("Port (example: 10010):");
                String port = Console.ReadLine();

                do {
                    NetworkServer server = new NetworkServer();
                    server.StartListening(ipAdress, port);
                    Console.WriteLine("Press Any Key to send next message or ESC to Stop");
                    key = System.Console.ReadKey().Key;
                }
                while (key != ConsoleKey.Escape) ;
            }
            // localhost:10010 Server connection
            if (key == ConsoleKey.S) {
                System.Console.WriteLine("SERVER ("+ localhostAddress + ":" + localhostPort + ") selected\n");
                do {
                    NetworkServer server = new NetworkServer();
                    server.StartListening(localhostAddress, localhostPort.ToString());
                    Console.WriteLine("Press Any Key to send next message or ESC to Stop");
                    key = System.Console.ReadKey().Key;
                }
                while (key != ConsoleKey.Escape) ;
            }

            // Programm Ende
            Console.WriteLine("\nTastendruck beendet Programm.");
            Console.ReadKey();
        }

        private static void PrintIpAdresses() {
            // Quelle: https://stackoverflow.com/questions/9487452/obtain-ip-address-of-wifi-connected-system
            string[] strIP = null;
            int count = 0;

            IPHostEntry HostEntry = Dns.GetHostEntry((Dns.GetHostName()));
            if (HostEntry.AddressList.Length > 0) {
                strIP = new string[HostEntry.AddressList.Length];
                foreach (IPAddress ip in HostEntry.AddressList) {
                    if (ip.AddressFamily == AddressFamily.InterNetwork) {
                        strIP[count] = ip.ToString();
                        Console.WriteLine(ip.ToString());
                        count++;
                    }
                }
            }

        }
    }

    internal class NetworkClient {
        public NetworkClient() {
        }
        /// <summary>
        /// Senden einer Nachricht an eine spezifische IP-Adresse, Port
        /// </summary>
        /// <param name="ipAdress"></param>
        /// <param name="endpointPort"></param>
        /// <param name="content">message string without eof suffix</param>
        public void Send(string ipAddressText, string port, string content) {
            int portNum = Int32.Parse(port);

            IPAddress endpointAdress = IPAddress.Parse(ipAddressText);
            IPEndPoint remoteEndpoint = new IPEndPoint(endpointAdress, portNum);
            byte[] bytes = new byte[1024];
            try {
                // Connect to a Remote server

                // Create a TCP/IP  socket.
                Socket sender = new Socket(endpointAdress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try {
                    // Connect to Remote EndPoint
                    sender.Connect(remoteEndpoint);

                    Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes(content + "<EOF>");

                    // Send the data through the socket.
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    Console.WriteLine("Echoed test = {0}",
                        Encoding.ASCII.GetString(bytes, 0, bytesRec));

                    // Release the socket.
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                // Falls ein Null-String übergeben wurde
                catch (ArgumentNullException ane) {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                // Verbindungsfehler
                catch (SocketException se) {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                // Nicht vorhergesehene Fehler
                catch (Exception e) {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Original Beispielcode
        /// Demonstriert das Senden an eine DNS-Adresse, hier "localhost"
        /// Es ist ergo keine IP-Adresse nötig.
        /// 
        /// Man könnte aber das Konvertieren der DNS zu einer IP auslagern.
        /// </summary>
        public void SendWithStaticParameters() {
            byte[] bytes = new byte[1024];

            try {
                // Connect to a Remote server
                // Get Host IP Address that is used to establish a connection
                // In this case, we get one IP address of localhost that is IP : 127.0.0.1
                // If a host has multiple addresses, you will get a list of addresses
                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 3);

                // Create a TCP/IP  socket.
                Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try {
                    // Connect to Remote EndPoint
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

                    // Send the data through the socket.
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    Console.WriteLine("Echoed test = {0}",
                        Encoding.ASCII.GetString(bytes, 0, bytesRec));

                    // Release the socket.
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (ArgumentNullException ane) {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se) {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e) {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }


    }

    internal class NetworkServer {
        int maxRequestLimit = 100;

        public NetworkServer() {
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
