using ChatApp.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp {
    public partial class ServerWindow : Form {
        ServerController server = new ServerController();
        public ServerWindow() {
            InitializeComponent();
        }

        private void Button_Start_Server_Click(object sender, EventArgs e) {
            server.Start();
        }

        private void Button_Shutdown_Server_Click(object sender, EventArgs e) {
            server.GracefullyShutdown();
        }

        private void Button_Abort_Click(object sender, EventArgs e) {
            server.Abort();
        }
    }
}
