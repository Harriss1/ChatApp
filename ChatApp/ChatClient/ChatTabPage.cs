﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp.ChatClient {
    internal class ChatTabPage {
        internal TabPage TabPage { get; private set; }
        internal Panel Messages { get; set; }
        internal TextBox TextInput { get; set; }
        internal Button SendButton { get; set; }
        internal Button CloseButton { get; set; }
        internal TableLayoutPanel Body { get; private set; }
        Color backColor = Color.FromArgb(218, 232, 252);
        private ChatTabPage() { }
        public ChatTabPage(string chatpartner) {
            TabPage = new TabPage();
            TabPage.Text = chatpartner;
            Body = CreateChatContainer();
            TabPage.Controls.Add(Body);
        }
        private TableLayoutPanel CreateChatContainer() {
            TableLayoutPanel chatContainer = new TableLayoutPanel();
            chatContainer.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
            chatContainer.ColumnCount = 1;
            chatContainer.RowCount = 4;
            chatContainer.BackColor = backColor;
            chatContainer.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            chatContainer.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            chatContainer.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
            //chatContainer.RowStyles.Add(new RowStyle(SizeType.Absolute, 73));
            chatContainer.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            chatContainer.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));

            CloseButton = new Button();
            CloseButton.Text = "╳";
            CloseButton.Font = new Font("Arial", 6, FontStyle.Regular);
            CloseButton.Width = 18;
            CloseButton.Height = 18;
            CloseButton.Anchor = AnchorStyles.Right;

            Messages = new Panel();
            Messages.BackColor = Color.Firebrick;
            Messages.AutoScroll = true;
            Messages.Dock = DockStyle.Fill;

            TextInput = new TextBox();
            TextInput.ScrollBars = ScrollBars.Vertical;
            TextInput.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            TextInput.WordWrap = true;
            TextInput.Multiline = true;

            SendButton = new Button();
            SendButton.Text = "Nachricht abschicken";
            SendButton.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;

            chatContainer.Controls.Add(CloseButton, 0, 0);
            chatContainer.Controls.Add(Messages, 0, 1);
            chatContainer.Controls.Add(TextInput, 0, 2);
            chatContainer.Controls.Add(SendButton, 0, 3);

            return chatContainer;
        }
    }
}
