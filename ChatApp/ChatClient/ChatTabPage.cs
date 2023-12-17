using ChatApp.Protocol;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp.ChatClient {
    internal class ChatTabPage {
        internal string ChatPartner { get; private set; }
        internal TabPage TabPage { get; private set; }
        internal Panel MessagesScrollerPanel { get; set; }
        internal TextBox TextInput { get; set; }
        internal Button SendButton { get; set; }
        internal Button CloseButton { get; set; }
        internal bool ControlsAreEnabled {get; private set; }
        internal bool showConversationTerminatedWarning = false;

        internal static List<ChatTabPage> TabList = new List<ChatTabPage>();
        private TabControl outerTabControl;
        internal TableLayoutPanel Body { get; private set; }
        Color backColor = Color.FromArgb(218, 232, 252);


        private TableLayoutPanel lastPanel = null;
        private ProtocolMessage lastProtocolMessage = null;
        private TextBox lastChatTextMessageBox = null;
        ToolTip timeStampHoverText;

        private ChatController chatController;

        private ChatTabPage() { }
        public ChatTabPage(string chatpartner, TabControl outside_tab_control, ChatController chatController) {
            this.chatController = chatController;
            timeStampHoverText = GetToolTip();
            ChatPartner = chatpartner;
            outerTabControl = outside_tab_control;
            TabPage = new TabPage();
            TabPage.Text = chatpartner;
            Body = CreateChatContainer();
            TabPage.Controls.Add(Body);
            CloseButton.Click += new EventHandler(CloseTab);
            SendButton.Click += new EventHandler(SendMessage);
            TabList.Add(this);
            DeactivateControls();
        }

        private void SendMessage(object sender, EventArgs e) {
            if (TextInput.Text.Length > Config.maxChatMessageTextLength) {
                MessageBox.Show(
                    "Eine Nachricht darf maximal " + Config.maxChatMessageTextLength + " Zeichen lang sein.",
                    "Warnung",
                    MessageBoxButtons.OK);
                return;
            }
            if (!chatController.LastChatMessageTransmitted) {
                MessageBox.Show(
                    "Bitte warten bis die vorhergehende Nachricht übertragen wurde.",
                    "Information",
                    MessageBoxButtons.OK);
                return;
            }
            ProtocolMessage response = chatController.SendMessage(TextInput.Text, ChatPartner);
            if (response != null) {
                AddSingleMessageTablePanel(response, true);
            }
            Console.WriteLine("Eingegebene Nachricht = " + TextInput.Text);
            TextInput.Text = "";

            TextInput.Enabled = false;
            SendButton.Enabled = false;        
        }

        internal void ActivateControls() {
            ControlsAreEnabled = true;
            SendButton.Enabled = true;
            TextInput.Enabled = true;
        }
        internal void DeactivateControls() {
            ControlsAreEnabled = false;
            SendButton.Enabled = false;
            TextInput.Enabled = false;
        }
        internal static bool TabListContainsChatPartner(string chatpartner) {
            foreach (ChatTabPage tab in TabList) {
                if (tab.ChatPartner.Equals(chatpartner)) {
                    return true;
                }
            }
            return false;
        }

        private void CloseTab(object sender, EventArgs e) {
            chatController.SendChatClosedPermission(ChatPartner);
            Dispose();
        }

        internal void Dispose() {
            outerTabControl.TabPages.Remove(TabPage);
            TabList.Remove(this);
        }

        private TableLayoutPanel CreateChatContainer() {
            TableLayoutPanel chatContainer = new TableLayoutPanel();
            chatContainer.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
            chatContainer.ColumnCount = 1;
            chatContainer.RowCount = 4;
            chatContainer.BackColor = backColor;
            chatContainer.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            chatContainer.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            chatContainer.RowStyles.Add(new RowStyle(SizeType.Absolute, 225));
            chatContainer.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            chatContainer.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));

            CloseButton = new Button();
            CloseButton.Text = "╳";
            CloseButton.Font = new Font("Arial", 6, FontStyle.Regular);
            CloseButton.Width = 18;
            CloseButton.Height = 18;
            CloseButton.Anchor = AnchorStyles.Right;

            MessagesScrollerPanel = new Panel();
            MessagesScrollerPanel.BackColor = backColor;
            MessagesScrollerPanel.AutoScroll = true;
            MessagesScrollerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            TextInput = new TextBox();
            TextInput.ScrollBars = ScrollBars.Vertical;
            TextInput.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            TextInput.WordWrap = true;
            TextInput.Multiline = true;

            SendButton = new Button();
            SendButton.Text = "Nachricht abschicken";
            SendButton.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            SendButton.FlatStyle = FlatStyle.Flat;

            chatContainer.Controls.Add(CloseButton, 0, 0);
            chatContainer.Controls.Add(MessagesScrollerPanel, 0, 1);
            chatContainer.Controls.Add(TextInput, 0, 2);
            chatContainer.Controls.Add(SendButton, 0, 3);

            return chatContainer;
        }

        internal void AddSingleMessageTablePanel(ProtocolMessage message, bool moveToTheRightSide) {
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
            panel.Width = MessagesScrollerPanel.Width - 20;

            // Nach unten scrollen
            MessagesScrollerPanel.Controls.Add(panel);
            MessagesScrollerPanel.VerticalScroll.Value = MessagesScrollerPanel.VerticalScroll.Maximum;

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
            messageBox.Height = size.Height + 2;
            if (moveToTheRightSide) {
                messageBox.Dock = DockStyle.Right;
            }
            lastPanel.Height += messageBox.Height;
            lastPanel.Controls.Add(messageBox, 0, lastPanel.RowCount);

            MessagesScrollerPanel.VerticalScroll.Value = MessagesScrollerPanel.VerticalScroll.Maximum;
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
            messageBox.Width = MessagesScrollerPanel.Width - 80;
            //messageBox.AcceptsReturn = false;
            messageBox.KeyDown += OnKeyEnter_MessageBox;
            return messageBox;
        }

        private void OnKeyEnter_MessageBox(object sender, KeyEventArgs e) {
            if (e.Shift && e.KeyCode == Keys.Enter) {
                //enter key is down
                // https://mycsharp.de/forum/threads/66008/erledigt-shift-enter-in-textbox-abfangen
                SendMessage(sender, e);
            }
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
