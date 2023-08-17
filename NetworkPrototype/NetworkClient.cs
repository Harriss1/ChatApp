using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkPrototype {
    internal class NetworkClient {
        private string username;
        private string ipAdress;

        public NetworkClient(string username, string ipAdress) {
            this.username = username;
            this.ipAdress = ipAdress;
        }

        internal void sendMessageTo(string username, string content) {
            
            send("10.91.59.56", 3, content);
        }

        internal void sendMessageToAdress(string adress, string port, string content) {
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
                        content += "\n\t - " + ip.ToString();
                    }
                }
            }
            int portNum = Int32.Parse(port);
            send(adress, portNum, content);
        }

        private void send() {
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dnsAdress"></param>
        /// <param name="port"></param>
        /// <param name="message">message string without eof suffix</param>
        private void send(string dnsAdress, int port, string message) {
            byte[] bytes = new byte[1024];

            try {
                // Connect to a Remote server
                // Get Host IP Address that is used to establish a connection
                // In this case, we get one IP address of localhost that is IP : 127.0.0.1
                // If a host has multiple addresses, you will get a list of addresses
                IPHostEntry host = Dns.GetHostEntry(dnsAdress);
                //IPAddress ipAddress = host.AddressList[0];
                IPAddress ipAddress = IPAddress.Parse(dnsAdress);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

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
                    byte[] msg = Encoding.ASCII.GetBytes(message + "<EOF>");

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
}