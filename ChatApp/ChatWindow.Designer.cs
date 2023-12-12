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
            this.mainTableLayout.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabTableLayout.SuspendLayout();
            this.chatSegmentTableLayout.SuspendLayout();
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
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 246F));
            this.mainTableLayout.Size = new System.Drawing.Size(1067, 674);
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
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
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
            this.Button_Server_View.Margin = new System.Windows.Forms.Padding(4);
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
            this.Text_Connection_Status.Margin = new System.Windows.Forms.Padding(4);
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
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
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
            this.Text_Server_Ip.Margin = new System.Windows.Forms.Padding(4);
            this.Text_Server_Ip.Name = "Text_Server_Ip";
            this.Text_Server_Ip.Size = new System.Drawing.Size(468, 27);
            this.Text_Server_Ip.TabIndex = 2;
            this.Text_Server_Ip.Text = "(Server-IP eingeben)";
            // 
            // Button_Login
            // 
            this.Button_Login.Location = new System.Drawing.Point(956, 4);
            this.Button_Login.Margin = new System.Windows.Forms.Padding(4);
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
            this.Text_Username.Margin = new System.Windows.Forms.Padding(4);
            this.Text_Username.Name = "Text_Username";
            this.Text_Username.Size = new System.Drawing.Size(468, 27);
            this.Text_Username.TabIndex = 1;
            this.Text_Username.Text = "(Benutzername eingeben)";
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
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 98);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1067, 37);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // Button_Request_Chat_Partner
            // 
            this.Button_Request_Chat_Partner.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Request_Chat_Partner.Location = new System.Drawing.Point(537, 4);
            this.Button_Request_Chat_Partner.Margin = new System.Windows.Forms.Padding(4);
            this.Button_Request_Chat_Partner.Name = "Button_Request_Chat_Partner";
            this.Button_Request_Chat_Partner.Size = new System.Drawing.Size(526, 29);
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
            this.Text_Chat_Partner.Location = new System.Drawing.Point(4, 4);
            this.Text_Chat_Partner.Margin = new System.Windows.Forms.Padding(4);
            this.Text_Chat_Partner.Name = "Text_Chat_Partner";
            this.Text_Chat_Partner.Size = new System.Drawing.Size(525, 27);
            this.Text_Chat_Partner.TabIndex = 2;
            this.Text_Chat_Partner.Text = "(Benutzername des Chatpartner eingeben)";
            // 
            // tabTableLayout
            // 
            this.tabTableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabTableLayout.ColumnCount = 1;
            this.tabTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tabTableLayout.Controls.Add(this.chatSegmentTableLayout, 0, 1);
            this.tabTableLayout.Location = new System.Drawing.Point(4, 139);
            this.tabTableLayout.Margin = new System.Windows.Forms.Padding(4);
            this.tabTableLayout.Name = "tabTableLayout";
            this.tabTableLayout.RowCount = 2;
            this.tabTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tabTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabTableLayout.Size = new System.Drawing.Size(1059, 531);
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
            this.chatSegmentTableLayout.Location = new System.Drawing.Point(4, 41);
            this.chatSegmentTableLayout.Margin = new System.Windows.Forms.Padding(4);
            this.chatSegmentTableLayout.Name = "chatSegmentTableLayout";
            this.chatSegmentTableLayout.RowCount = 3;
            this.chatSegmentTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.chatSegmentTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.chatSegmentTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.chatSegmentTableLayout.Size = new System.Drawing.Size(1051, 486);
            this.chatSegmentTableLayout.TabIndex = 0;
            // 
            // Button_Send_Message
            // 
            this.Button_Send_Message.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Send_Message.Location = new System.Drawing.Point(4, 455);
            this.Button_Send_Message.Margin = new System.Windows.Forms.Padding(4);
            this.Button_Send_Message.Name = "Button_Send_Message";
            this.Button_Send_Message.Size = new System.Drawing.Size(1043, 27);
            this.Button_Send_Message.TabIndex = 4;
            this.Button_Send_Message.Text = "Senden";
            this.Button_Send_Message.UseVisualStyleBackColor = true;
            this.Button_Send_Message.Click += new System.EventHandler(this.Button_Send_Message_Click);
            // 
            // ChatPanelScroller
            // 
            this.ChatPanelScroller.AutoScroll = true;
            this.ChatPanelScroller.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChatPanelScroller.Location = new System.Drawing.Point(4, 4);
            this.ChatPanelScroller.Margin = new System.Windows.Forms.Padding(4);
            this.ChatPanelScroller.Name = "ChatPanelScroller";
            this.ChatPanelScroller.Size = new System.Drawing.Size(1043, 353);
            this.ChatPanelScroller.TabIndex = 0;
            // 
            // Text_Message_Input
            // 
            this.Text_Message_Input.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Text_Message_Input.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Text_Message_Input.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text_Message_Input.Location = new System.Drawing.Point(4, 365);
            this.Text_Message_Input.Margin = new System.Windows.Forms.Padding(4);
            this.Text_Message_Input.Multiline = true;
            this.Text_Message_Input.Name = "Text_Message_Input";
            this.Text_Message_Input.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Text_Message_Input.Size = new System.Drawing.Size(1043, 82);
            this.Text_Message_Input.TabIndex = 4;
            this.Text_Message_Input.Text = "(neue Nachricht verfassen)";
            this.Text_Message_Input.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Text_Message_Input_MouseDown);
            // 
            // ChatWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 677);
            this.Controls.Add(this.mainTableLayout);
            this.Margin = new System.Windows.Forms.Padding(4);
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
    }
}

