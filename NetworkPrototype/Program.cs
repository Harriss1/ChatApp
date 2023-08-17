using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkPrototype {
    internal class Program {
        static void Main(string[] args) {
            printIpAdresses();
            System.Console.WriteLine("Client (c) or Server (s) or Client-Adress (d)?");
            ConsoleKey key = System.Console.ReadKey().Key;
            if (key == ConsoleKey.C) {
                System.Console.WriteLine("Client selected\n");
                System.Console.WriteLine("\tInsert message to send to Hermine:");
                String content = Console.ReadLine();
                Console.WriteLine("Message:\n\t" + content);
                NetworkClient client = new NetworkClient("Max Muster", "127.0.0.1:4");
                client.sendMessageTo("Hermine", content);
            }
            if (key == ConsoleKey.D) {
                System.Console.WriteLine("Client selected\n");
                System.Console.WriteLine("\tInsert message to send to Hermine:");
                String content = Console.ReadLine();

                System.Console.WriteLine("\tIP-Adress:");
                String ipAdress = Console.ReadLine();
                System.Console.WriteLine("\tPort:");
                String port = Console.ReadLine();
                Console.WriteLine("Message:\n\t" + content + "\tAdress+Port: " + ipAdress + ":" + port);
                NetworkClient client = new NetworkClient("Max Muster", "127.0.0.1:4");
                client.sendMessageToAdress(ipAdress, port, content);
            }
            if (key == ConsoleKey.S) {
                System.Console.WriteLine("Server selected\n\n\tIP-Adresse:");
                String ipAdress = Console.ReadLine();
                System.Console.WriteLine("\n\tPort:");
                String port = Console.ReadLine();
                NetworkServer server = new NetworkServer();
                server.startListening(ipAdress, port);
            }
            Console.ReadKey();
        }

        private static void printIpAdresses() {
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
