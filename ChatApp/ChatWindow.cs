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
    public partial class ChatWindow : Form {
        public ChatWindow() {
            InitializeComponent();
        }

        private void Button_Server_View_Click(object sender, EventArgs e) {
            ServerWindow serverWindow = new ServerWindow();
            serverWindow.StartPosition = FormStartPosition.Manual;

            // Position relative to the right of Main Window
            var chatWindow = new ChatWindow();
            var xOffset = chatWindow.Width;            
            serverWindow.Location = PointToScreen(new Point(chatWindow.Location.X + xOffset, chatWindow.Location.Y-30));
            
            serverWindow.Show();
        }
    }
}
