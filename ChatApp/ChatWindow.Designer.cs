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
            this.tabTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.Tab_Control_Chats = new System.Windows.Forms.TabControl();
            this.TabPage_NewChat = new System.Windows.Forms.TabPage();
            this.Button_Tabbed_Chat_Request = new System.Windows.Forms.Button();
            this.Text_Tabbed_Chatpartner = new System.Windows.Forms.TextBox();
            this.mainTableLayout.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabTableLayout.SuspendLayout();
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
            this.mainTableLayout.Controls.Add(this.tabTableLayout, 0, 2);
            this.mainTableLayout.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.mainTableLayout.Name = "mainTableLayout";
            this.mainTableLayout.RowCount = 3;
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 246F));
            this.mainTableLayout.Size = new System.Drawing.Size(1067, 619);
            this.mainTableLayout.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel1.Controls.Add(this.Button_Server_View, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Text_Connection_Status, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 53);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1052, 41);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // Button_Server_View
            // 
            this.Button_Server_View.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Server_View.Location = new System.Drawing.Point(896, 4);
            this.Button_Server_View.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Button_Server_View.Name = "Button_Server_View";
            this.Button_Server_View.Size = new System.Drawing.Size(152, 28);
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
            this.Text_Connection_Status.Location = new System.Drawing.Point(4, 4);
            this.Text_Connection_Status.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Text_Connection_Status.Name = "Text_Connection_Status";
            this.Text_Connection_Status.Size = new System.Drawing.Size(884, 27);
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
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tableLayoutPanel2.Controls.Add(this.Text_Server_Ip, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.Button_Login, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.Text_Username, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1059, 41);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // Text_Server_Ip
            // 
            this.Text_Server_Ip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Text_Server_Ip.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Text_Server_Ip.Font = new System.Drawing.Font("Cascadia Mono Light", 10F);
            this.Text_Server_Ip.Location = new System.Drawing.Point(480, 4);
            this.Text_Server_Ip.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Text_Server_Ip.Name = "Text_Server_Ip";
            this.Text_Server_Ip.Size = new System.Drawing.Size(468, 27);
            this.Text_Server_Ip.TabIndex = 2;
            this.Text_Server_Ip.Text = "Server: 127.0.0.1 (oder IP hier eingeben)";
            this.Text_Server_Ip.Enter += new System.EventHandler(this.On_Focus_Text_IpAddress);
            this.Text_Server_Ip.Leave += new System.EventHandler(this.On_Leave_Focus_Text_IpAddress);
            // 
            // Button_Login
            // 
            this.Button_Login.Location = new System.Drawing.Point(956, 4);
            this.Button_Login.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Button_Login.Name = "Button_Login";
            this.Button_Login.Size = new System.Drawing.Size(99, 28);
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
            this.Text_Username.Location = new System.Drawing.Point(4, 4);
            this.Text_Username.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Text_Username.Name = "Text_Username";
            this.Text_Username.Size = new System.Drawing.Size(468, 27);
            this.Text_Username.TabIndex = 1;
            this.Text_Username.Text = "(Benutzername eingeben)";
            this.Text_Username.Enter += new System.EventHandler(this.On_Focus_Text_Username);
            this.Text_Username.Leave += new System.EventHandler(this.On_Leave_Focus_Text_Username);
            // 
            // tabTableLayout
            // 
            this.tabTableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabTableLayout.ColumnCount = 1;
            this.tabTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabTableLayout.Controls.Add(this.Tab_Control_Chats, 0, 0);
            this.tabTableLayout.Location = new System.Drawing.Point(4, 102);
            this.tabTableLayout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabTableLayout.Name = "tabTableLayout";
            this.tabTableLayout.RowCount = 1;
            this.tabTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 492F));
            this.tabTableLayout.Size = new System.Drawing.Size(1059, 513);
            this.tabTableLayout.TabIndex = 6;
            // 
            // Tab_Control_Chats
            // 
            this.Tab_Control_Chats.Controls.Add(this.TabPage_NewChat);
            this.Tab_Control_Chats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tab_Control_Chats.Location = new System.Drawing.Point(4, 4);
            this.Tab_Control_Chats.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tab_Control_Chats.Name = "Tab_Control_Chats";
            this.Tab_Control_Chats.SelectedIndex = 0;
            this.Tab_Control_Chats.Size = new System.Drawing.Size(1051, 505);
            this.Tab_Control_Chats.TabIndex = 1;
            // 
            // TabPage_NewChat
            // 
            this.TabPage_NewChat.Controls.Add(this.Button_Tabbed_Chat_Request);
            this.TabPage_NewChat.Controls.Add(this.Text_Tabbed_Chatpartner);
            this.TabPage_NewChat.Location = new System.Drawing.Point(4, 25);
            this.TabPage_NewChat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TabPage_NewChat.Name = "TabPage_NewChat";
            this.TabPage_NewChat.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TabPage_NewChat.Size = new System.Drawing.Size(1043, 476);
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
            this.Button_Tabbed_Chat_Request.Location = new System.Drawing.Point(8, 43);
            this.Button_Tabbed_Chat_Request.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Button_Tabbed_Chat_Request.Name = "Button_Tabbed_Chat_Request";
            this.Button_Tabbed_Chat_Request.Size = new System.Drawing.Size(226, 49);
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
            this.Text_Tabbed_Chatpartner.Location = new System.Drawing.Point(8, 8);
            this.Text_Tabbed_Chatpartner.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Text_Tabbed_Chatpartner.Name = "Text_Tabbed_Chatpartner";
            this.Text_Tabbed_Chatpartner.Size = new System.Drawing.Size(524, 27);
            this.Text_Tabbed_Chatpartner.TabIndex = 3;
            this.Text_Tabbed_Chatpartner.Text = "(Benutzername des Chatpartner eingeben)";
            this.Text_Tabbed_Chatpartner.Enter += new System.EventHandler(this.Text_Tabbed_Chatpartner_Enter);
            this.Text_Tabbed_Chatpartner.Leave += new System.EventHandler(this.Text_Tabbed_Chatpartner_Leave);
            // 
            // ChatWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 621);
            this.Controls.Add(this.mainTableLayout);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ChatWindow";
            this.Text = "ChatApp";
            this.mainTableLayout.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tabTableLayout.ResumeLayout(false);
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
        private System.Windows.Forms.TableLayoutPanel tabTableLayout;
        private System.Windows.Forms.TabControl Tab_Control_Chats;
        private System.Windows.Forms.TabPage TabPage_NewChat;
        private System.Windows.Forms.Button Button_Tabbed_Chat_Request;
        private System.Windows.Forms.TextBox Text_Tabbed_Chatpartner;
    }
}

