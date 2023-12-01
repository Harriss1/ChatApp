using ChatApp.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp {
    public partial class ServerWindow : Form {
        ServerController server = new ServerController();
        public ServerWindow() {
            InitializeComponent();
            LogPublisher.OnEvent_PublishServerMessage onEvent_PublishServerMessage =
                new LogPublisher.OnEvent_PublishServerMessage(OnEvent_AddConsoleMessage);
            LogPublisher.SubscribeTo_PublishServerMessage(onEvent_PublishServerMessage);
        }

        private void OnEvent_AddConsoleMessage(string message) {
            // Fix: Der Zugriff auf das Steuerelement Text_Console_Output erfolgte von einem anderen
            // Thread als dem Thread, für den es erstellt wurde.
            // Quelle: https://stackoverflow.com/questions/661561/how-do-i-update-the-gui-from-another-thread
            object _lock = new object();
            lock (_lock) {
                Text_Console_Output.Invoke((MethodInvoker)delegate {

                    // Running on the UI thread

                    if (message.Contains("Details:")) {
                        Console.WriteLine("UI Thread =" + Thread.CurrentThread.ManagedThreadId);
                    }
                    Text_Console_Output.Text += message + "\r\n";
                    Text_Console_Output.SelectionStart = Text_Console_Output.Text.Length;
                    Text_Console_Output.ScrollToCaret();
                });
            }
        }

        private void Button_Start_Server_Click(object sender, EventArgs e) {
            server.Start();
        }

        private void Button_Shutdown_Server_Click(object sender, EventArgs e) {
            server.GracefullyShutdown();
        }

        private void Button_Abort_Server_Click(object sender, EventArgs e) {
            server.Abort();
        }
        /// <summary>
        /// Die Wiederherstellung und Re-Fokusierung des Server-Fensters wird im ChatWindow behandelt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerWindow_FormClosing_1(object sender, FormClosingEventArgs e) {
            // erweiterter Code: https://stackoverflow.com/a/17796192
            if (e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
                MessageBox.Show("Der Server wird im Hintergrund weiter ausgeführt.", "Information", MessageBoxButtons.OK);
                this.Hide();
            }
        }
    }
}
