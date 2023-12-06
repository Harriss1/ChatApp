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
        private Timer updateTimer;
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
            if (Button_Login.Text.Equals("Anmelden") && 
                !chatController.IsLoggedIn()) {
                string username = Text_Username.Text;
                string ipAddress = "127.0.0.1";
                chatController.LoginToServer(username, ipAddress);
                Text_Chatmessages_Placeholder.Text += "\r\n#\r\n Verbinde zu: " + ipAddress + ":" + Config.ServerPort;
            } else {
                chatController.LogoutFromServer();
                Text_Chatmessages_Placeholder.Text += "\r\n#\r\nAbmeldevorgang gestartet...";
            }

            updateTimer = new Timer();
            updateTimer.Tick += new EventHandler(UpdateUI);
            updateTimer.Interval = 2000;
            updateTimer.Start();            
        }

        private void UpdateUI(object sender, EventArgs e) {            
            string received = chatController.DequeueReceivedChatMessage();
            if (received != null) {
                Text_Chatmessages_Placeholder.Text += received;
            }

            string serverlinkStatusMessage = chatController.GetServerlinkStatusMessage();
            Text_Connection_Status.Text = serverlinkStatusMessage;

            if (chatController.IsLoggedIn()){
                Button_Login.Text = "Abmelden";
                Text_Connection_Status.Text = "Server online und erfolgreich angemeldet.";
            }
            else {
                Button_Login.Text = "Anmelden";
            }
        }

        private void Button_Send_Message_Click(object sender, EventArgs e) {
            if(Text_Message_Input.Text.Length > Config.maxChatMessageTextLength) {
                MessageBox.Show(
                    "Eine Nachricht darf maximal "+ Config.maxChatMessageTextLength + " Zeichen lang sein.", 
                    "Warnung", 
                    MessageBoxButtons.OK);
                return;
            }
            chatController.SendMessage(Text_Message_Input.Text, Text_Chat_Partner.Text);
            Console.WriteLine("Eingegebene Nachricht = " + Text_Message_Input.Text);
            Text_Message_Input.Text = "";
        }

        private void Text_Message_Input_MouseDown(object sender, MouseEventArgs e) {
            if (Text_Message_Input.Text.Equals("(neue Nachricht verfassen)")){
                Text_Message_Input.Text = "";
            }
        }
    }
}
