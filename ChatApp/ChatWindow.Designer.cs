namespace ChatApp {
    partial class ChatWindow {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.mainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Button_Server_View = new System.Windows.Forms.Button();
            this.Text_Connection_Status = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Text_Server_Ip = new System.Windows.Forms.TextBox();
            this.Button_Login = new System.Windows.Forms.Button();
            this.Text_Username = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.Button_Request_Chat_Partner = new System.Windows.Forms.Button();
            this.Text_Chat_Partner = new System.Windows.Forms.TextBox();
            this.tabTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.chatSegmentTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.Button_Send_Message = new System.Windows.Forms.Button();
            this.ChatPanelScroller = new System.Windows.Forms.Panel();
            this.Text_Message_Input = new System.Windows.Forms.TextBox();
            this.Tab_Control_Chats = new System.Windows.Forms.TabControl();
            this.TabPage_NewChat = new System.Windows.Forms.TabPage();
            this.Button_Tabbed_Chat_Request = new System.Windows.Forms.Button();
            this.Text_Tabbed_Chatpartner = new System.Windows.Forms.TextBox();
            this.mainTableLayout.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabTableLayout.SuspendLayout();
            this.chatSegmentTableLayout.SuspendLayout();
            this.Tab_Control_Chats.SuspendLayout();
            this.TabPage_NewChat.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableLayout
            // 
            this.mainTableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTableLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(232)))), ((int)(((byte)(252)))));
            this.mainTableLayout.ColumnCount = 1;
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayout.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.mainTableLayout.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.mainTableLayout.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.mainTableLayout.Controls.Add(this.tabTableLayout, 0, 3);
            this.mainTableLayout.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.mainTableLayout.Name = "mainTableLayout";
            this.mainTableLayout.RowCount = 4;
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.mainTableLayout.Size = new System.Drawing.Size(800, 844);
            this.mainTableLayout.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.Controls.Add(this.Button_Server_View, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Text_Connection_Status, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 43);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(789, 33);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // Button_Server_View
            // 
            this.Button_Server_View.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Server_View.Location = new System.Drawing.Point(672, 3);
            this.Button_Server_View.Name = "Button_Server_View";
            this.Button_Server_View.Size = new System.Drawing.Size(114, 23);
            this.Button_Server_View.TabIndex = 0;
            this.Button_Server_View.Text = "Server Ansicht";
            this.Button_Server_View.UseVisualStyleBackColor = true;
            this.Button_Server_View.Click += new System.EventHandler(this.Button_Server_View_Click);
            // 
            // Text_Connection_Status
            // 
            this.Text_Connection_Status.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Text_Connection_Status.BackColor = System.Drawing.SystemColors.Menu;
            this.Text_Connection_Status.Font = new System.Drawing.Font("Cascadia Mono Light", 10F);
            this.Text_Connection_Status.Location = new System.Drawing.Point(3, 3);
            this.Text_Connection_Status.Name = "Text_Connection_Status";
            this.Text_Connection_Status.Size = new System.Drawing.Size(663, 23);
            this.Text_Connection_Status.TabIndex = 1;
            this.Text_Connection_Status.Text = "(Verbindungsstatus zum Server)";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.Controls.Add(this.Text_Server_Ip, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.Button_Login, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.Text_Username, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(794, 34);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // Text_Server_Ip
            // 
            this.Text_Server_Ip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Text_Server_Ip.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Text_Server_Ip.Font = new System.Drawing.Font("Cascadia Mono Light", 10F);
            this.Text_Server_Ip.Location = new System.Drawing.Point(360, 3);
            this.Text_Server_Ip.Name = "Text_Server_Ip";
            this.Text_Server_Ip.Size = new System.Drawing.Size(351, 23);
            this.Text_Server_Ip.TabIndex = 2;
            this.Text_Server_Ip.Text = "(Server-IP eingeben)";
            this.Text_Server_Ip.Enter += new System.EventHandler(this.On_Focus_Text_IpAddress);
            this.Text_Server_Ip.Leave += new System.EventHandler(this.On_Leave_Focus_Text_IpAddress);
            // 
            // Button_Login
            // 
            this.Button_Login.Location = new System.Drawing.Point(717, 3);
            this.Button_Login.Name = "Button_Login";
            this.Button_Login.Size = new System.Drawing.Size(74, 23);
            this.Button_Login.TabIndex = 0;
            this.Button_Login.Text = "Anmelden";
            this.Button_Login.UseVisualStyleBackColor = true;
            this.Button_Login.Click += new System.EventHandler(this.Button_Login_Click);
            // 
            // Text_Username
            // 
            this.Text_Username.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Text_Username.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Text_Username.Font = new System.Drawing.Font("Cascadia Mono Light", 10F);
            this.Text_Username.Location = new System.Drawing.Point(3, 3);
            this.Text_Username.Name = "Text_Username";
            this.Text_Username.Size = new System.Drawing.Size(351, 23);
            this.Text_Username.TabIndex = 1;
            this.Text_Username.Text = "(Benutzername eingeben)";
            this.Text_Username.Enter += new System.EventHandler(this.On_Focus_Text_Username);
            this.Text_Username.Leave += new System.EventHandler(this.On_Leave_Focus_Text_Username);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.Button_Request_Chat_Partner, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.Text_Chat_Partner, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 80);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(800, 30);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // Button_Request_Chat_Partner
            // 
            this.Button_Request_Chat_Partner.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Request_Chat_Partner.Location = new System.Drawing.Point(403, 3);
            this.Button_Request_Chat_Partner.Name = "Button_Request_Chat_Partner";
            this.Button_Request_Chat_Partner.Size = new System.Drawing.Size(394, 24);
            this.Button_Request_Chat_Partner.TabIndex = 3;
            this.Button_Request_Chat_Partner.Text = "Chat Anfragen";
            this.Button_Request_Chat_Partner.UseVisualStyleBackColor = true;
            // 
            // Text_Chat_Partner
            // 
            this.Text_Chat_Partner.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Text_Chat_Partner.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Text_Chat_Partner.Font = new System.Drawing.Font("Cascadia Mono Light", 10F);
            this.Text_Chat_Partner.Location = new System.Drawing.Point(3, 3);
            this.Text_Chat_Partner.Name = "Text_Chat_Partner";
            this.Text_Chat_Partner.Size = new System.Drawing.Size(394, 23);
            this.Text_Chat_Partner.TabIndex = 2;
            this.Text_Chat_Partner.Text = "(Benutzername des Chatpartner eingeben)";
            this.Text_Chat_Partner.Enter += new System.EventHandler(this.On_Focus_Text_Chatpartner);
            this.Text_Chat_Partner.Leave += new System.EventHandler(this.On_Leave_Focus_Text_Chatpartner);
            // 
            // tabTableLayout
            // 
            this.tabTableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabTableLayout.ColumnCount = 1;
            this.tabTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabTableLayout.Controls.Add(this.chatSegmentTableLayout, 0, 1);
            this.tabTableLayout.Controls.Add(this.Tab_Control_Chats, 0, 0);
            this.tabTableLayout.Location = new System.Drawing.Point(3, 113);
            this.tabTableLayout.Name = "tabTableLayout";
            this.tabTableLayout.RowCount = 2;
            this.tabTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tabTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabTableLayout.Size = new System.Drawing.Size(794, 728);
            this.tabTableLayout.TabIndex = 6;
            // 
            // chatSegmentTableLayout
            // 
            this.chatSegmentTableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatSegmentTableLayout.ColumnCount = 1;
            this.chatSegmentTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.chatSegmentTableLayout.Controls.Add(this.Button_Send_Message, 0, 2);
            this.chatSegmentTableLayout.Controls.Add(this.ChatPanelScroller, 0, 0);
            this.chatSegmentTableLayout.Controls.Add(this.Text_Message_Input, 0, 1);
            this.chatSegmentTableLayout.Location = new System.Drawing.Point(3, 403);
            this.chatSegmentTableLayout.Name = "chatSegmentTableLayout";
            this.chatSegmentTableLayout.RowCount = 3;
            this.chatSegmentTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.chatSegmentTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.chatSegmentTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.chatSegmentTableLayout.Size = new System.Drawing.Size(788, 322);
            this.chatSegmentTableLayout.TabIndex = 0;
            // 
            // Button_Send_Message
            // 
            this.Button_Send_Message.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Send_Message.Enabled = false;
            this.Button_Send_Message.Location = new System.Drawing.Point(3, 297);
            this.Button_Send_Message.Name = "Button_Send_Message";
            this.Button_Send_Message.Size = new System.Drawing.Size(782, 22);
            this.Button_Send_Message.TabIndex = 4;
            this.Button_Send_Message.Text = "Senden";
            this.Button_Send_Message.UseVisualStyleBackColor = true;
            this.Button_Send_Message.Click += new System.EventHandler(this.Button_Send_Message_Click);
            // 
            // ChatPanelScroller
            // 
            this.ChatPanelScroller.AutoScroll = true;
            this.ChatPanelScroller.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChatPanelScroller.Location = new System.Drawing.Point(3, 3);
            this.ChatPanelScroller.Name = "ChatPanelScroller";
            this.ChatPanelScroller.Size = new System.Drawing.Size(782, 215);
            this.ChatPanelScroller.TabIndex = 0;
            // 
            // Text_Message_Input
            // 
            this.Text_Message_Input.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Text_Message_Input.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Text_Message_Input.Enabled = false;
            this.Text_Message_Input.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text_Message_Input.Location = new System.Drawing.Point(3, 224);
            this.Text_Message_Input.Multiline = true;
            this.Text_Message_Input.Name = "Text_Message_Input";
            this.Text_Message_Input.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Text_Message_Input.Size = new System.Drawing.Size(782, 67);
            this.Text_Message_Input.TabIndex = 4;
            this.Text_Message_Input.Text = "(neue Nachricht verfassen)";
            this.Text_Message_Input.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Text_Message_Input_MouseDown);
            // 
            // Tab_Control_Chats
            // 
            this.Tab_Control_Chats.Controls.Add(this.TabPage_NewChat);
            this.Tab_Control_Chats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tab_Control_Chats.Location = new System.Drawing.Point(3, 3);
            this.Tab_Control_Chats.Name = "Tab_Control_Chats";
            this.Tab_Control_Chats.SelectedIndex = 0;
            this.Tab_Control_Chats.Size = new System.Drawing.Size(788, 394);
            this.Tab_Control_Chats.TabIndex = 1;
            // 
            // TabPage_NewChat
            // 
            this.TabPage_NewChat.Controls.Add(this.Button_Tabbed_Chat_Request);
            this.TabPage_NewChat.Controls.Add(this.Text_Tabbed_Chatpartner);
            this.TabPage_NewChat.Location = new System.Drawing.Point(4, 22);
            this.TabPage_NewChat.Name = "TabPage_NewChat";
            this.TabPage_NewChat.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_NewChat.Size = new System.Drawing.Size(780, 368);
            this.TabPage_NewChat.TabIndex = 0;
            this.TabPage_NewChat.Text = "Neuer Chat";
            this.TabPage_NewChat.UseVisualStyleBackColor = true;
            // 
            // Button_Tabbed_Chat_Request
            // 
            this.Button_Tabbed_Chat_Request.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Tabbed_Chat_Request.Enabled = false;
            this.Button_Tabbed_Chat_Request.Location = new System.Drawing.Point(400, -1);
            this.Button_Tabbed_Chat_Request.Name = "Button_Tabbed_Chat_Request";
            this.Button_Tabbed_Chat_Request.Size = new System.Drawing.Size(199, 324);
            this.Button_Tabbed_Chat_Request.TabIndex = 4;
            this.Button_Tabbed_Chat_Request.Text = "Chat Anfragen";
            this.Button_Tabbed_Chat_Request.UseVisualStyleBackColor = true;
            this.Button_Tabbed_Chat_Request.Click += new System.EventHandler(this.Button_Tabbed_Chat_Request_Click);
            // 
            // Text_Tabbed_Chatpartner
            // 
            this.Text_Tabbed_Chatpartner.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Text_Tabbed_Chatpartner.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Text_Tabbed_Chatpartner.Enabled = false;
            this.Text_Tabbed_Chatpartner.Font = new System.Drawing.Font("Cascadia Mono Light", 10F);
            this.Text_Tabbed_Chatpartner.Location = new System.Drawing.Point(0, 0);
            this.Text_Tabbed_Chatpartner.Name = "Text_Tabbed_Chatpartner";
            this.Text_Tabbed_Chatpartner.Size = new System.Drawing.Size(394, 23);
            this.Text_Tabbed_Chatpartner.TabIndex = 3;
            this.Text_Tabbed_Chatpartner.Text = "(Benutzername des Chatpartner eingeben)";
            this.Text_Tabbed_Chatpartner.Enter += new System.EventHandler(this.Text_Tabbed_Chatpartner_Enter);
            this.Text_Tabbed_Chatpartner.Leave += new System.EventHandler(this.Text_Tabbed_Chatpartner_Leave);
            // 
            // ChatWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 846);
            this.Controls.Add(this.mainTableLayout);
            this.Name = "ChatWindow";
            this.Text = "ChatApp";
            this.mainTableLayout.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tabTableLayout.ResumeLayout(false);
            this.chatSegmentTableLayout.ResumeLayout(false);
            this.chatSegmentTableLayout.PerformLayout();
            this.Tab_Control_Chats.ResumeLayout(false);
            this.TabPage_NewChat.ResumeLayout(false);
            this.TabPage_NewChat.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTableLayout;
        private System.Windows.Forms.Button Button_Server_View;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox Text_Connection_Status;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button Button_Login;
        private System.Windows.Forms.TextBox Text_Username;
        private System.Windows.Forms.TextBox Text_Server_Ip;
        private System.Windows.Forms.TextBox Text_Message_Input;
        private System.Windows.Forms.Button Button_Send_Message;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button Button_Request_Chat_Partner;
        private System.Windows.Forms.TextBox Text_Chat_Partner;
        private System.Windows.Forms.TableLayoutPanel tabTableLayout;
        private System.Windows.Forms.TableLayoutPanel chatSegmentTableLayout;
        private System.Windows.Forms.Panel ChatPanelScroller;
        private System.Windows.Forms.TabControl Tab_Control_Chats;
        private System.Windows.Forms.TabPage TabPage_NewChat;
        private System.Windows.Forms.Button Button_Tabbed_Chat_Request;
        private System.Windows.Forms.TextBox Text_Tabbed_Chatpartner;
    }
}

