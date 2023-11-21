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
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.Button_Send_Message = new System.Windows.Forms.Button();
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
            this.Text_Message_Input = new System.Windows.Forms.TextBox();
            this.Text_Chatmessages_Placeholder = new System.Windows.Forms.TextBox();
            this.tableLayout.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayout
            // 
            this.tableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(232)))), ((int)(((byte)(252)))));
            this.tableLayout.ColumnCount = 1;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Controls.Add(this.Button_Send_Message, 0, 5);
            this.tableLayout.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayout.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayout.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayout.Controls.Add(this.Text_Message_Input, 0, 4);
            this.tableLayout.Controls.Add(this.Text_Chatmessages_Placeholder, 0, 3);
            this.tableLayout.Location = new System.Drawing.Point(0, 0);
            this.tableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 6;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayout.Size = new System.Drawing.Size(800, 450);
            this.tableLayout.TabIndex = 0;
            // 
            // Button_Send_Message
            // 
            this.Button_Send_Message.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Send_Message.Location = new System.Drawing.Point(3, 423);
            this.Button_Send_Message.Name = "Button_Send_Message";
            this.Button_Send_Message.Size = new System.Drawing.Size(794, 24);
            this.Button_Send_Message.TabIndex = 4;
            this.Button_Send_Message.Text = "Senden";
            this.Button_Send_Message.UseVisualStyleBackColor = true;
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(789, 34);
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
            // 
            // Button_Login
            // 
            this.Button_Login.Location = new System.Drawing.Point(717, 3);
            this.Button_Login.Name = "Button_Login";
            this.Button_Login.Size = new System.Drawing.Size(74, 23);
            this.Button_Login.TabIndex = 0;
            this.Button_Login.Text = "Anmelden";
            this.Button_Login.UseVisualStyleBackColor = true;
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
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
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
            // 
            // Text_Message_Input
            // 
            this.Text_Message_Input.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Text_Message_Input.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Text_Message_Input.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text_Message_Input.Location = new System.Drawing.Point(3, 323);
            this.Text_Message_Input.Multiline = true;
            this.Text_Message_Input.Name = "Text_Message_Input";
            this.Text_Message_Input.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Text_Message_Input.Size = new System.Drawing.Size(794, 94);
            this.Text_Message_Input.TabIndex = 4;
            this.Text_Message_Input.Text = "(neue Nachricht verfassen)";
            // 
            // Text_Chatmessages_Placeholder
            // 
            this.Text_Chatmessages_Placeholder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Text_Chatmessages_Placeholder.Font = new System.Drawing.Font("Calibri", 12F);
            this.Text_Chatmessages_Placeholder.Location = new System.Drawing.Point(3, 113);
            this.Text_Chatmessages_Placeholder.Multiline = true;
            this.Text_Chatmessages_Placeholder.Name = "Text_Chatmessages_Placeholder";
            this.Text_Chatmessages_Placeholder.ReadOnly = true;
            this.Text_Chatmessages_Placeholder.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Text_Chatmessages_Placeholder.Size = new System.Drawing.Size(794, 204);
            this.Text_Chatmessages_Placeholder.TabIndex = 5;
            this.Text_Chatmessages_Placeholder.Text = "(messages)";
            // 
            // ChatWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayout);
            this.Name = "ChatWindow";
            this.Text = "ChatApp";
            this.tableLayout.ResumeLayout(false);
            this.tableLayout.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayout;
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
        private System.Windows.Forms.TextBox Text_Chatmessages_Placeholder;
    }
}

