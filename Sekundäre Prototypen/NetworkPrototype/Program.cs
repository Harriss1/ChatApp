﻿using System;
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
            System.Console.WriteLine("IP-Adressen des Computers in den Netzwerken:");
            PrintIpAdresses();


            System.Console.WriteLine("\nTastendruck-Auswahl Client oder Server:");
            // Auswahl Client/Server
            System.Console.WriteLine(
                "S - Dynamically setup Server\n" +
                "D - Dynamically setup Client\n" +
                "deprecated: C - Client with programmatic settings\n");
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
                System.Console.WriteLine("\tInsert message to send:");
                String content = Console.ReadLine();

                System.Console.WriteLine("\tnumeric IP-Adress:");
                String ipAdress = Console.ReadLine();

                System.Console.WriteLine("\tPort:");
                String port = Console.ReadLine();

                Console.WriteLine("Message:\n\t" + content + "\tAdress+Port: " + ipAdress + ":" + port);
                Console.WriteLine();
                
                NetworkClient client = new NetworkClient();
                client.SendMessageToAdress(ipAdress, port, content);
            }

            // Setup server via user input
            if (key == ConsoleKey.S) {
                System.Console.WriteLine("Server selected\n\n\tnumerische IP-Adresse:");
                String ipAdress = Console.ReadLine();
               
                System.Console.WriteLine("\nPort:");
                String port = Console.ReadLine();
                System.Console.WriteLine();

                NetworkServer server = new NetworkServer();
                server.StartListening(ipAdress, port);
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

        /////////////////////////////////////////////////////////
        /// 
        /// Auslagern der folgenden Methoden in Helfer-Klasse falls im Projekt benötigt.

        public static IPAddress GetIPAddressFromIP(string ip) {
            IPAddress ipAddress = IPAddress.Parse(ip);
            return ipAddress;
        }
        public static IPAddress GetIPAdressFromDNS(string dnsAddress) {
            // Get Host IP Address that is used to establish a connection
            // In this case, we get one IP address of localhost that is IP : 127.0.0.1
            // If a host has multiple addresses, you will get a list of addresses
            IPHostEntry host = Dns.GetHostEntry(dnsAddress);
            IPAddress ipAddress = host.AddressList[0];
            return ipAddress;
        }
    }
}
