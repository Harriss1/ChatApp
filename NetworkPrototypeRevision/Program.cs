using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkPrototypeRevision {
    internal class Program {
        static void Main(string[] args) {
            int localhostPort = 10010;
            string localhostAddress = "127.0.0.1";
            System.Console.WriteLine("Network Prototype: Chat via sending string-messages");
            // IP-Adressen zur Einrichtung des Clienten oder Servers sowie zum Debuggen
            System.Console.WriteLine("IP-Adresses of this computer:");
            PrintIpAdresses();

            // Auswahl Client/Server, dynamic/static
            System.Console.WriteLine("\nPress Key:");
            System.Console.WriteLine(
                "S - configure and start Server\n" +
                "P - use preset: "+ localhostAddress + ":"+ localhostPort + "and start Server" +
                "\n\n" +
                "C - configure and start Client\n" +
                "D - use preset:  " + localhostAddress + ":" + localhostPort + " and start Client\n");
            ConsoleKey key = System.Console.ReadKey().Key;

            // Configure Client Connection
            if (key == ConsoleKey.C) {
                System.Console.WriteLine("CLIENT (configure and start) selected\n");
                System.Console.WriteLine("\tIP-Adress (example: 123.123.123.123):");
                String ipAdress = Console.ReadLine();

                System.Console.WriteLine("\tPort (example: 10010):");
                String port = Console.ReadLine();

                System.Console.WriteLine("Message to send:");
                String content = Console.ReadLine();

                Console.WriteLine("Message:\n\t" + content + "\tAdress+Port: " + ipAdress + ":" + port + "\n");

                NetworkClient client = new NetworkClient();
                client.SendMessageToAdress(ipAdress, port, content);

            }

            // localhost:10010 client connection
            if (key == ConsoleKey.D) {
                System.Console.WriteLine("CLIENT (localhost:10010) selected\n");
                System.Console.WriteLine("Insert message to send:");
                String content = Console.ReadLine();
                Console.WriteLine("Message:\n\t" + content);
                NetworkClient client = new NetworkClient();
                client.SendMessageToAdress(localhostAddress, localhostPort.ToString(), content);
            }

            // configure and start Server
            if (key == ConsoleKey.S) {
                System.Console.WriteLine("SERVER (configure and start) selected\n");
                System.Console.WriteLine("IP-Adress (example: 123.123.123.123):");
                String ipAdress = Console.ReadLine();
                System.Console.WriteLine("Port (example: 10010):");
                String port = Console.ReadLine();

                NetworkServer server = new NetworkServer();
                server.StartListening(ipAdress, port);
            }
            // localhost:10010 Server connection
            if (key == ConsoleKey.P) {
                System.Console.WriteLine("SERVER (localhost:10010) selected\n");

                NetworkServer server = new NetworkServer();
                server.StartListening(localhostAddress, localhostPort.ToString());
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
}
