using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkPrototype {
    internal class Program {
        static void Main(string[] args) {
            System.Console.WriteLine("Client (c) or Server (s)?");
            ConsoleKey key = System.Console.ReadKey().Key;
            if (key == ConsoleKey.C) {
                System.Console.WriteLine("Client selected\n");
                System.Console.WriteLine("\tInsert message to send to Hermine:");
                String content = Console.ReadLine();
                Console.WriteLine("Message:\n\t" + content);
                NetworkClient client = new NetworkClient("Max Muster", "127.0.0.1:4");
                client.sendMessageTo("Hermine", content);
            }
            if(key == ConsoleKey.S) {
                System.Console.WriteLine("Server selected\n");
                NetworkServer server = new NetworkServer();
                server.startListening();
            }
            Console.ReadKey();
        }
    }
}
