﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.ChatClient {
    internal class TcpClient {
        private LogPublisher log = new LogPublisher("TcpClient");
        private Socket sender;

        public void Connect(string ipAddressText, string port) {
            int portNum = Int32.Parse(port);
            log.Debug("Connect() Starting...");
            IPAddress endpointAdress = IPAddress.Parse(ipAddressText);
            IPEndPoint remoteEndpoint = new IPEndPoint(endpointAdress, portNum);

            // Connect to a Remote server
            try {
                // Create a TCP/IP  socket.
                sender = new Socket(endpointAdress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                log.Debug("Connect() socket on endpoint created");
                // Connect the socket to the remote endpoint. Catch any errors.
                try {
                    // Connect to Remote EndPoint
                    sender.Connect(remoteEndpoint);

                    log.Debug("Socket() Verbindung aufgebaut zu: " + sender.RemoteEndPoint.ToString());
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
        }
        public void Stop() {
            log.Debug("Client Socket Shutdown initiating...");
            try {
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();

                log.Debug("Client Socket Shutdown komplett");
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

        /// <summary>
        /// Senden einer Nachricht an eine spezifische IP-Adresse, Port
        /// </summary>
        /// <param name="ipAddressText"></param>
        /// <param name="port"></param>
        /// <param name="content">message string without eof suffix</param>
        public string Send(string content) {
            string received = "[nothing received]";
            log.Debug("[start send] Zu übermittelnde Nachricht:" + content);
            log.Debug("Client Kontrollpunkt 1");
            byte[] bytes = new byte[1024];
            try {
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
            return received;
        }
    }
}
