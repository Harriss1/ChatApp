using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

/// <summary>
/// techn. Prototyp für Socket-Netzwerkverbindung
/// Client und Server auswählbar
/// 
/// Client und Server Code von: https://www.c-sharpcorner.com/article/socket-programming-in-C-Sharp/
/// 
/// Die Klassen sind wiederverwendbar gestalltet, und haben
/// keine zusätzlichen Funktionen wie z.B. Benutzernamen oder Nachrichtenauswertung
/// </summary>

namespace NetworkPrototype {
    internal class Program {
        static void Main(string[] args) {
            // IP-Adressen zur Einrichtung des Clienten oder Servers sowie zum Debuggen
            PrintIpAdresses();

            // Auswahl Client/Server
            System.Console.WriteLine("C - Client with programmatic settings\n" +
                "S - Dynamically setup Server\n" +
                "D - Dynamically setup Client\n");
            ConsoleKey key = System.Console.ReadKey().Key;

            // Connection is set programmatically
            if (key == ConsoleKey.C) {
                System.Console.WriteLine("Client selected\n");
               
                NetworkClient client = new NetworkClient();
                client.SendWithStaticParameters();
            }

            // Setup client connection via user input
            if (key == ConsoleKey.D) {
                System.Console.WriteLine("Client selected\n");
                System.Console.WriteLine("\tInsert message to send to Hermine:");
                String content = Console.ReadLine();

                System.Console.WriteLine("\tIP-Adress:");
                String ipAdress = Console.ReadLine();

                System.Console.WriteLine("\tPort:");
                String port = Console.ReadLine();

                Console.WriteLine("Message:\n\t" + content + "\tAdress+Port: " + ipAdress + ":" + port);
                NetworkClient client = new NetworkClient();
                client.SendMessageToAdress(ipAdress, port, content);
            }

            // Setup server via user input
            if (key == ConsoleKey.S) {
                System.Console.WriteLine("Server selected\n\n\tIP-Adresse:");
                String ipAdress = Console.ReadLine();
               
                System.Console.WriteLine("\n\tPort:");
                String port = Console.ReadLine();
                
                NetworkServer server = new NetworkServer();
                server.StartListening(ipAdress, port);
            }

            // Programm Ende
            Console.WriteLine("Tastendruck beendet Programm.");
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
