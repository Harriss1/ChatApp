using ChatApp.ChatClient;
using ChatApp.Protocol;
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
            ProtocolMessage received = chatController.DequeueReceivedChatMessage();
            if (received != null) {
                Text_Chatmessages_Placeholder.Text += received.GetXml().OuterXml;
                AddSingleMessagePanel(received);
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
        private Panel lastPanel = null;
        private void AddSingleMessagePanel(ProtocolMessage message) {
            Point locator = new Point(0, 0);
            if (lastPanel != null) {
                locator = lastPanel.Location;
                locator.X = 0;
                locator.Offset(0, lastPanel.Height + 2);
            }

            Panel panel = new Panel();
            TextBox nameBox = new TextBox();
            nameBox.Text = message.GetSenderUsername();

            TextBox messageBox = new TextBox();
            messageBox.Multiline = true;
            messageBox.Text = message.GetTextMessageFromContent();
            messageBox.Width = ChatPanelScroller.Width - 60;
            Size size = TextRenderer.MeasureText(messageBox.Text, messageBox.Font);
            messageBox.Height = size.Height + 6;
            messageBox.Top = nameBox.Height + 2;
            panel.Controls.Add(nameBox);
            panel.Controls.Add(messageBox);
            panel.Location = locator;
            panel.Height = messageBox.Height + nameBox.Height + 4;
            panel.Width = messageBox.Width + 6;
            ChatPanelScroller.Controls.Add(panel);
            lastPanel = panel;
            ChatPanelScroller.VerticalScroll.Value = ChatPanelScroller.VerticalScroll.Maximum;
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
