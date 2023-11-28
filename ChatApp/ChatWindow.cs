using ChatApp.ChatClient;
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
        private ServerWindow serverWindow;
        private ChatController chatController = new ChatController();
        public ChatWindow() {
            InitializeComponent();
        }

        private void Button_Server_View_Click(object sender, EventArgs e) {
            if (serverWindow == null) {
                serverWindow = new ServerWindow();
            }
            if (serverWindow.IsDisposed) {
                throw new NotImplementedException();
            }
            if (!serverWindow.Visible) {
                serverWindow.Show();
            } else {
                
                serverWindow.WindowState = FormWindowState.Normal;
                serverWindow.BringToFront();
                serverWindow.Focus();
                return;
            }
            
            serverWindow.StartPosition = FormStartPosition.Manual;

            // Position relative to the right of Main Window
            var chatWindow = new ChatWindow();
            var xOffset = chatWindow.Width;            
            serverWindow.Location = PointToScreen(new Point(chatWindow.Location.X + xOffset, chatWindow.Location.Y-30));
            
            serverWindow.Show();
        }

        private void Button_Login_Click(object sender, EventArgs e) {
            chatController.LoginToServer("Rudi", "127.0.0.1");
            Text_Chatmessages_Placeholder.Text = "connecting to: " + "127.0.0.1:" + Config.ServerPort;
        }
        private void Button_Send_Message_Click(object sender, EventArgs e) {
            chatController.SendMessage(Text_Message_Input.Text);
            string received = chatController.GetLastReceivedMessage();
            Console.WriteLine("Eingegebene Nachricht = " + Text_Message_Input.Text);
            Text_Message_Input.Text = "";
            Text_Chatmessages_Placeholder.Text += received;
        }

        private void Text_Message_Input_MouseDown(object sender, MouseEventArgs e) {
            if (Text_Message_Input.Text.Equals("(neue Nachricht verfassen)")){
                Text_Message_Input.Text = "";
            }
        }
    }
}
