using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkPrototype {
    internal class Program {
        static void Main(string[] args) {
            System.Console.WriteLine("Insert message to send:");
            String content = Console.ReadLine();
            Console.WriteLine("Message:\n\t" + content);
            Console.ReadKey();
        }
    }
}
