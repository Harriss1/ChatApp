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
        private ProtocolMessage lastProtocolMessage = null;
        private TextBox lastChatTextMessageBox = null;
        ToolTip timeStampHoverText;
        public ChatWindow() {
            InitializeComponent();
            timeStampHoverText = GetToolTip();
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
                //Text_Chatmessages_Placeholder.Text += "\r\n#\r\n Verbinde zu: " + ipAddress + ":" + Config.ServerPort;
            } else {
                chatController.LogoutFromServer();
                //Text_Chatmessages_Placeholder.Text += "\r\n#\r\nAbmeldevorgang gestartet...";
            }

            updateTimer = new Timer();
            updateTimer.Tick += new EventHandler(UpdateUI);
            updateTimer.Interval = 2000;
            updateTimer.Start();            
        }

        private void UpdateUI(object sender, EventArgs e) {            
            ProtocolMessage received = chatController.DequeueReceivedChatMessage();
            if (received != null) {
                //Text_Chatmessages_Placeholder.Text += received.GetXml().OuterXml;
                AddSingleMessageTablePanel(received, false);
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
        private void AddSingleMessageTablePanel(ProtocolMessage message, bool moveToTheRightSide) {
            if (!message.GetMessageType().Equals(MessageTypeEnum.CHAT_MESSAGE)) {
                return;
            }
            string messageText = message.GetTextMessageFromContent();
            string timeInfoText = DateTime.Now.ToString("H:mm");
            string timeStamp = System.DateTime.Now.ToString();
            int fontHeigth = 12;
            Font font = new Font("Calibri", fontHeigth, FontStyle.Regular);

            // Anhängen der Nachricht an die letzte Nachricht statt neues Boxsegment einzufügen
            if (NewMessageBelongsToRecentSender(message)) {
                string replaceText = lastChatTextMessageBox.Text
                                        + "\r\n"
                                        + messageText;
                if (replaceText.Length < Config.maxChatMessageTextLength + 20) {
                    //ExtendRecentTextMessage(messageText, font, replaceText);
                    lastChatTextMessageBox = AddMessageSegmentToRecentPanel(font, messageText, moveToTheRightSide, timeInfoText, timeStamp);
                    lastProtocolMessage = message;
                    lastProtocolMessage = message;
                    return;
                }
            }

            TableLayoutPanel panel = new TableLayoutPanel();
            Point locator = new Point(0, 0);
            if (lastPanel != null) {
                locator = lastPanel.Location;
                locator.X = 0;
                locator.Offset(0, lastPanel.Height + 2);
            }
            panel.Location = locator;

            panel.ColumnCount = 1;
            panel.RowCount = 2;
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            TextBox nameBox = CreateNameBox(message, font);
            Size nameBoxSize = TextRenderer.MeasureText(nameBox.Text, nameBox.Font);
            nameBox.Width = nameBoxSize.Width + 2;

            TextBox messageBox = CreateMessageBox(messageText, font);
            Size size = TextRenderer.MeasureText(messageBox.Text, messageBox.Font);
            messageBox.Height = size.Height + 2;
            //messageBox.Top = nameBox.Height + 1;

            TextBox timeStampBox = CreateTimeStampBox(timeInfoText, timeStamp, font, moveToTheRightSide);

            if (moveToTheRightSide) {
                nameBox.Dock = DockStyle.Right;
                messageBox.Dock = DockStyle.Right;
            }

            // Dimensionen des Panels nach Berechnung der Teilelement-Dimensionen
            panel.Height = messageBox.Height 
                        + timeStampBox.Height
                        + nameBox.Height + 2;

            // Teil-Elemente hinzufügen
            panel.Controls.Add(nameBox, 0, 0);
            panel.Controls.Add(timeStampBox, 0, 1);
            panel.Controls.Add(messageBox, 0, 2);
            panel.Width = ChatPanelScroller.Width - 20;

            // Nach unten scrollen
            ChatPanelScroller.Controls.Add(panel);
            ChatPanelScroller.VerticalScroll.Value = ChatPanelScroller.VerticalScroll.Maximum;

            lastPanel = panel;
            lastChatTextMessageBox = messageBox;
            lastProtocolMessage = message;
        }

        private TextBox CreateTimeStampBox(string timeInfoText, string timeStamp, Font font, bool moveToTheRightSide) {
            Font smallFont = new Font(font.FontFamily, 9, font.Style);
            TextBox timeBox = new TextBox();
            timeBox.Text = timeInfoText;
            timeBox.ReadOnly = true;
            timeBox.Font = smallFont;
            timeBox.BorderStyle = BorderStyle.None;
            timeBox.BackColor = Color.LightYellow;
            Size virtualSize = TextRenderer.MeasureText(timeBox.Text, timeBox.Font);
            timeBox.Width = virtualSize.Width + 2;
            if (moveToTheRightSide) {
                timeBox.Dock = DockStyle.Right;
            }
            // Set up the ToolTip text for the Info-Box
            timeStampHoverText.SetToolTip(timeBox, timeStamp);
            return timeBox;
        }

        private TextBox AddMessageSegmentToRecentPanel(Font font, string messageText, bool moveToTheRightSide, string timeInfoText, string timeStamp) {
            lastPanel.RowCount += 2;
            lastPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
            lastPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            TextBox timeStampBox = CreateTimeStampBox(timeInfoText, timeStamp, font, moveToTheRightSide);
            lastPanel.Height += timeStampBox.Height;
            lastPanel.Controls.Add(timeStampBox, 0, lastPanel.RowCount - 1);

            TextBox messageBox = CreateMessageBox(messageText, font);
            Size size = TextRenderer.MeasureText(messageBox.Text, messageBox.Font);
            logPublisher.Info("size.Height=" + size.Height);
            messageBox.Height = size.Height + 2;
            if (moveToTheRightSide) {
                messageBox.Dock = DockStyle.Right;
            }
            lastPanel.Height += messageBox.Height;
            lastPanel.Controls.Add(messageBox, 0, lastPanel.RowCount);

            ChatPanelScroller.VerticalScroll.Value = ChatPanelScroller.VerticalScroll.Maximum;
            return messageBox;
        }
        private bool NewMessageBelongsToRecentSender(ProtocolMessage message) {
            return lastChatTextMessageBox != null &&
                            message.GetSenderUsername().Equals(lastProtocolMessage.GetSenderUsername());
        }

        private TextBox CreateMessageBox(string messageText, Font font) {
            TextBox messageBox = new TextBox();
            messageBox.ReadOnly = true;
            messageBox.Font = font;
            messageBox.BackColor = Color.LightGoldenrodYellow;
            messageBox.BorderStyle = BorderStyle.None;
            messageBox.Multiline = true;
            messageBox.Text = messageText;
            messageBox.Width = ChatPanelScroller.Width - 80;
            return messageBox;
        }

        private static TextBox CreateNameBox(ProtocolMessage message, Font font) {
            TextBox nameBox = new TextBox();
            nameBox.Text = " " + message.GetSenderUsername();
            nameBox.ReadOnly = true;
            nameBox.Font = font;
            nameBox.BorderStyle = BorderStyle.None;
            nameBox.BackColor = Color.Azure;
            return nameBox;
        }

        private void Button_Send_Message_Click(object sender, EventArgs e) {
            if(Text_Message_Input.Text.Length > Config.maxChatMessageTextLength) {
                MessageBox.Show(
                    "Eine Nachricht darf maximal "+ Config.maxChatMessageTextLength + " Zeichen lang sein.", 
                    "Warnung", 
                    MessageBoxButtons.OK);
                return;
            }
            ProtocolMessage response = chatController.SendMessage(Text_Message_Input.Text, Text_Chat_Partner.Text);
            if (response != null) {
                AddSingleMessageTablePanel(response, true);
            }
            Console.WriteLine("Eingegebene Nachricht = " + Text_Message_Input.Text);
            Text_Message_Input.Text = "";
        }

        private void Text_Message_Input_MouseDown(object sender, MouseEventArgs e) {
            if (Text_Message_Input.Text.Equals("(neue Nachricht verfassen)")){
                Text_Message_Input.Text = "";
            }
        }

        private ToolTip GetToolTip() {
            // Create the ToolTip and associate with the Form container.
            ToolTip tooltip = new ToolTip();
            // Set up the delays for the ToolTip.
            tooltip.AutoPopDelay = 5000;
            tooltip.InitialDelay = 300;
            tooltip.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            tooltip.ShowAlways = true;
            return tooltip;
        }
    }
}
