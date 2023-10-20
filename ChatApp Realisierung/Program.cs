using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ChatApp_Realisierung {
    internal static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("text");
            string textual = xmlDocument.ToString();
            System.Console.WriteLine(xmlDocument.ToString());
        }
    }
}
