using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.ChatClient {
    internal class TcpClient {
        /// <summary>
        /// Senden einer Nachricht an eine spezifische IP-Adresse, Port
        /// </summary>
        /// <param name="ipAddressText"></param>
        /// <param name="port"></param>
        /// <param name="content">message string without eof suffix</param>
        public string Send(string ipAddressText, string port, string content) {
            int portNum = Int32.Parse(port);
            string received = "[nothing received]";
            Console.WriteLine("[start send] Zu übermittelnde Nachricht:" + content);
            Console.WriteLine("Client Kontrollpunkt 1");
            IPAddress endpointAdress = IPAddress.Parse(ipAddressText);
            IPEndPoint remoteEndpoint = new IPEndPoint(endpointAdress, portNum);
            Console.WriteLine("Client Kontrollpunkt 2");
            byte[] bytes = new byte[1024];
            try {
                // Connect to a Remote server

                // Create a TCP/IP  socket.
                Socket sender = new Socket(endpointAdress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                Console.WriteLine("Client Kontrollpunkt 3");
                // Connect the socket to the remote endpoint. Catch any errors.
                try {
                    // Connect to Remote EndPoint
                    sender.Connect(remoteEndpoint);

                    Console.WriteLine("Client Socket verbunden zu {0} \r\n [quit] beendet Verbindung.",
                        sender.RemoteEndPoint.ToString());
                    
                    // Encode the data string into a byte array.
                        
                    byte[] msg = Encoding.ASCII.GetBytes(content);

                    Console.WriteLine("Client Kontrollpunkt 4");
                    // Send the data through the socket.
                    int bytesSent = sender.Send(msg);
                    Console.WriteLine("Client Kontrollpunkt 5 - byteanzahl gesendet:" + bytesSent);

                    // Receive the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    // Hier beginnt ein Loop, da der Host die Verbindung / das Senden nicht beendet.
                    Console.WriteLine("Client Kontrollpunkt 6"); // hier kommen wir nicht hin :D
                    received = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    Console.WriteLine("Client Kontrollpunkt 7");
                    Console.WriteLine("Echo = {0}", received);
                    // Release the socket.

                    Console.WriteLine("Client Kontrollpunkt 8");
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                    Console.WriteLine("Client Kontrollpunkt 9");
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
            return received;
        }
    }
}
