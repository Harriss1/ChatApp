using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.ChatClient {
    internal class TcpClient {
        LogPublisher log = new LogPublisher("TcpClient");
        /// <summary>
        /// Senden einer Nachricht an eine spezifische IP-Adresse, Port
        /// </summary>
        /// <param name="ipAddressText"></param>
        /// <param name="port"></param>
        /// <param name="content">message string without eof suffix</param>
        public string Send(string ipAddressText, string port, string content) {
            int portNum = Int32.Parse(port);
            string received = "[nothing received]";
            log.Debug("[start send] Zu übermittelnde Nachricht:" + content);
            log.Debug("Client Kontrollpunkt 1");
            IPAddress endpointAdress = IPAddress.Parse(ipAddressText);
            IPEndPoint remoteEndpoint = new IPEndPoint(endpointAdress, portNum);
            log.Debug("Client Kontrollpunkt 2");
            byte[] bytes = new byte[1024];
            try {
                // Connect to a Remote server

                // Create a TCP/IP  socket.
                Socket sender = new Socket(endpointAdress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                log.Debug("Client Kontrollpunkt 3");
                // Connect the socket to the remote endpoint. Catch any errors.
                try {
                    // Connect to Remote EndPoint
                    sender.Connect(remoteEndpoint);

                    log.Debug("Client Socket verbunden zu "+ sender.RemoteEndPoint.ToString()+ 
                        " \r\n [quit] beendet Verbindung.");
                    
                    // Encode the data string into a byte array.
                        
                    byte[] msg = Encoding.ASCII.GetBytes(content);

                    log.Debug("Client Kontrollpunkt 4");
                    // Send the data through the socket.
                    int bytesSent = sender.Send(msg);
                    log.Debug("Client Kontrollpunkt 5 - byteanzahl gesendet:" + bytesSent);

                    // Receive the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    // Hier beginnt ein Loop, da der Host die Verbindung / das Senden nicht beendet.
                    log.Debug("Client Kontrollpunkt 6"); // hier kommen wir nicht hin :D
                    received = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    log.Debug("Client Kontrollpunkt 7");
                    log.Debug("Echo = " + received);
                    // Release the socket.

                    log.Debug("Client Kontrollpunkt 8");
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                    log.Debug("Client Kontrollpunkt 9");
                }
                // Falls ein Null-String übergeben wurde
                catch (ArgumentNullException ane) {
                    log.Debug("ArgumentNullException : " + ane.ToString());
                }
                // Verbindungsfehler
                catch (SocketException se) {
                    log.Debug("SocketException : " + se.ToString());
                }
                // Nicht vorhergesehene Fehler
                catch (Exception e) {
                    log.Debug("Unexpected exception : " + e.ToString());
                }
            }
            catch (Exception e) {
                log.Debug(e.ToString());
            }
            return received;
        }
    }
}
