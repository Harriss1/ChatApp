using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.ChatClient {
    internal class ChatClient {
        /// <summary>
        /// Senden einer Nachricht an eine spezifische IP-Adresse, Port
        /// </summary>
        /// <param name="ipAddressText"></param>
        /// <param name="port"></param>
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

                    Console.WriteLine("Socket connected to {0} [quit] beendet Verbindung.",
                        sender.RemoteEndPoint.ToString());
                    bool receivedQuitMessage = false;
                    bool quitEvent = false;
                    while (!receivedQuitMessage || quitEvent) {
                        // Encode the data string into a byte array.
                        if (content == null) {
                            Console.WriteLine("Nächste Nachricht eingeben:");
                            content = Console.ReadLine();
                        }
                        byte[] msg = Encoding.ASCII.GetBytes(content + "<EOF>");

                        // Send the data through the socket.
                        int bytesSent = sender.Send(msg);

                        // Receive the response from the remote device.
                        int bytesRec = sender.Receive(bytes);
                        Console.WriteLine("Echoed test = {0}",
                            Encoding.ASCII.GetString(bytes, 0, bytesRec));
                        if (content.Contains("quit")) {
                            receivedQuitMessage = true;
                            System.Console.WriteLine("### Quitting Connection...");
                        }
                        content = null;
                    }
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
    }
}
