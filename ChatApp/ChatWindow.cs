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
        private TableLayoutPanel lastPanel = null;
        LogPublisher logPublisher = new LogPublisher("ChatWindow", false);
        private ServerWindow serverWindow;
        private ChatController chatController = new ChatController();
        private Timer updateTimer;
        private bool popupWarningTransmissionFailureAlreadyShown = false;
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
            } else {
                chatController.LogoutFromServer();
            }
            updateTimer = new Timer();
            updateTimer.Tick += new EventHandler(UpdateUI);
            updateTimer.Interval = 2000;
            updateTimer.Start();
        }

        private void Button_Tabbed_Chat_Request_Click(object sender, EventArgs e) {
            string chatpartnerName = Text_Tabbed_Chatpartner.Text;
            if (ChatTabPage.TabListContainsChatPartner(chatpartnerName)) {
                MessageBox.Show(
                    "Chatpartner '" + Text_Tabbed_Chatpartner.Text + "' wurde bereits hinzugefügt.",
                    "Warnung",
                    MessageBoxButtons.OK);
                return;
            }
            chatController.SendChatPermissionRequest(chatpartnerName);
            AddChatTab(chatpartnerName);
            Text_Tabbed_Chatpartner.Text = "(Benutzername des Chatpartner eingeben)";
        }

        private void AddChatTab(string chatpartnerName) {
            ChatTabPage chatTabPage = new ChatTabPage(chatpartnerName, Tab_Control_Chats, chatController);
            Tab_Control_Chats.TabPages.Insert(Tab_Control_Chats.TabPages.Count - 1, chatTabPage.TabPage);
            Tab_Control_Chats.SelectedTab = chatTabPage.TabPage;
        }

        private void UpdateUI(object sender, EventArgs e) {            
            ProtocolMessage received = chatController.DequeueReceivedChatMessage();
            if (received != null) {
                if (chatController.permittedChatPartners.Contains(received.GetSenderUsername())) {
                    ChatTabPage chatTabPage = null;
                    foreach(ChatTabPage search in ChatTabPage.TabList) {
                        if (search.ChatPartner.Equals(received.GetSenderUsername())) {
                            chatTabPage = search;
                            break;
                        }
                    }
                    if(chatTabPage == null) {
                        chatTabPage = new ChatTabPage(received.GetSenderUsername(), Tab_Control_Chats, chatController);
                        chatTabPage.ActivateControls();
                    }
                    chatTabPage.AddSingleMessageTablePanel(received, false);
                }
            }

            string serverlinkStatusMessage = chatController.GetServerlinkStatusMessage();
            Text_Connection_Status.Text = serverlinkStatusMessage;

            if (chatController.IsLoggedIn()){
                Button_Login.Text = "Abmelden";
                Text_Connection_Status.Text += " => erfolgreich angemeldet";
                Button_Tabbed_Chat_Request.Enabled = true;
                Text_Tabbed_Chatpartner.Enabled = true;
            }
            else {
                Button_Login.Text = "Anmelden";
                Text_Connection_Status.Text += " => nicht angemeldet";
                Button_Tabbed_Chat_Request.Enabled = false;
                Text_Tabbed_Chatpartner.Enabled = false;
            }
            if(chatController.LastChatMessageTransmitted && chatController.IsLoggedIn()) {
                if (!chatController.LastChatMessageSuccessfullySent && !popupWarningTransmissionFailureAlreadyShown) {
                    popupWarningTransmissionFailureAlreadyShown = true;
                    MessageBox.Show(
                        "Bei der letzten Nachrichtenübertragung trat ein Fehler auf.",
                        "Warnung",
                        MessageBoxButtons.OK);
                }
            } else {
                popupWarningTransmissionFailureAlreadyShown = false;
            }
            foreach(ChatTabPage page in ChatTabPage.TabList) {
                if (chatController.permittedChatPartners.Contains(page.ChatPartner)) {
                    page.ActivateControls();
                } else {
                    page.DeactivateControls();
                }
            }
            foreach (string chatPartner in chatController.permittedChatPartners) {
                if (!ChatTabPage.TabListContainsChatPartner(chatPartner)) {
                    AddChatTab(chatPartner);

                }
            }
        }


        private void On_Focus_Text_Username(object sender, EventArgs e) {
            if (Text_Username.Text.Equals("(Benutzername eingeben)")) {
                Text_Username.Text = "";
            }
        }

        private void On_Focus_Text_IpAddress(object sender, EventArgs e) {
            if (Text_Server_Ip.Text.Equals("(Server-IP eingeben)")) {
                Text_Server_Ip.Text = "";
            }
        }

        private void On_Leave_Focus_Text_IpAddress(object sender, EventArgs e) {
            if (Text_Server_Ip.Text.Equals("")) {
                Text_Server_Ip.Text = "(Server-IP eingeben)";
            }
        }

        private void On_Leave_Focus_Text_Username(object sender, EventArgs e) {
            if (Text_Username.Text.Equals("")) {
                Text_Username.Text = "(Benutzername eingeben)";
            }
        }

        private void Text_Tabbed_Chatpartner_Enter(object sender, EventArgs e) {
            if (Text_Tabbed_Chatpartner.Text.Equals("(Benutzername des Chatpartner eingeben)")) {
                Text_Tabbed_Chatpartner.Text = "";
            }
        }

        private void Text_Tabbed_Chatpartner_Leave(object sender, EventArgs e) {
            if (Text_Tabbed_Chatpartner.Text.Equals("")) {
                Text_Tabbed_Chatpartner.Text = "(Benutzername des Chatpartner eingeben)";
            }
        }
    }
}
