namespace ChatApp {
    partial class ServerWindow {
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
            this.Panel_Controls = new System.Windows.Forms.TableLayoutPanel();
            this.Button_Shutdown_Server = new System.Windows.Forms.Button();
            this.Button_Start_Server = new System.Windows.Forms.Button();
            this.Button_Abort_Server = new System.Windows.Forms.Button();
            this.Text_Console_Output = new System.Windows.Forms.TextBox();
            this.tableLayout.SuspendLayout();
            this.Panel_Controls.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayout
            // 
            this.tableLayout.ColumnCount = 1;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Controls.Add(this.Panel_Controls, 0, 1);
            this.tableLayout.Controls.Add(this.Text_Console_Output, 0, 0);
            this.tableLayout.Location = new System.Drawing.Point(4, 5);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 2;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayout.Size = new System.Drawing.Size(406, 444);
            this.tableLayout.TabIndex = 0;
            // 
            // Panel_Controls
            // 
            this.Panel_Controls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Controls.ColumnCount = 4;
            this.Panel_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Panel_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.Panel_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.Panel_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.Panel_Controls.Controls.Add(this.Button_Shutdown_Server, 2, 0);
            this.Panel_Controls.Controls.Add(this.Button_Start_Server, 1, 0);
            this.Panel_Controls.Controls.Add(this.Button_Abort_Server, 3, 0);
            this.Panel_Controls.Location = new System.Drawing.Point(0, 414);
            this.Panel_Controls.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Controls.Name = "Panel_Controls";
            this.Panel_Controls.RowCount = 1;
            this.Panel_Controls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Panel_Controls.Size = new System.Drawing.Size(406, 30);
            this.Panel_Controls.TabIndex = 0;
            // 
            // Button_Shutdown_Server
            // 
            this.Button_Shutdown_Server.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Shutdown_Server.Location = new System.Drawing.Point(209, 3);
            this.Button_Shutdown_Server.Name = "Button_Shutdown_Server";
            this.Button_Shutdown_Server.Size = new System.Drawing.Size(94, 24);
            this.Button_Shutdown_Server.TabIndex = 1;
            this.Button_Shutdown_Server.Text = "Herunterfahren";
            this.Button_Shutdown_Server.UseVisualStyleBackColor = true;
            this.Button_Shutdown_Server.Click += new System.EventHandler(this.Button_Shutdown_Server_Click);
            // 
            // Button_Start_Server
            // 
            this.Button_Start_Server.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Start_Server.Location = new System.Drawing.Point(109, 3);
            this.Button_Start_Server.Name = "Button_Start_Server";
            this.Button_Start_Server.Size = new System.Drawing.Size(94, 24);
            this.Button_Start_Server.TabIndex = 0;
            this.Button_Start_Server.Text = "Starten";
            this.Button_Start_Server.UseVisualStyleBackColor = true;
            this.Button_Start_Server.Click += new System.EventHandler(this.Button_Start_Server_Click);
            // 
            // Button_Abort_Server
            // 
            this.Button_Abort_Server.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Abort_Server.Location = new System.Drawing.Point(309, 3);
            this.Button_Abort_Server.Name = "Button_Abort_Server";
            this.Button_Abort_Server.Size = new System.Drawing.Size(94, 24);
            this.Button_Abort_Server.TabIndex = 2;
            this.Button_Abort_Server.Text = "Sofort Stoppen";
            this.Button_Abort_Server.UseVisualStyleBackColor = true;
            this.Button_Abort_Server.Click += new System.EventHandler(this.Button_Abort_Server_Click);
            // 
            // Text_Console_Output
            // 
            this.Text_Console_Output.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Text_Console_Output.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Text_Console_Output.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text_Console_Output.ForeColor = System.Drawing.SystemColors.GrayText;
            this.Text_Console_Output.Location = new System.Drawing.Point(0, 0);
            this.Text_Console_Output.Margin = new System.Windows.Forms.Padding(0);
            this.Text_Console_Output.Multiline = true;
            this.Text_Console_Output.Name = "Text_Console_Output";
            this.Text_Console_Output.ReadOnly = true;
            this.Text_Console_Output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Text_Console_Output.Size = new System.Drawing.Size(406, 414);
            this.Text_Console_Output.TabIndex = 1;
            // 
            // ServerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 450);
            this.Controls.Add(this.tableLayout);
            this.Name = "ServerWindow";
            this.Text = "Server";
            this.tableLayout.ResumeLayout(false);
            this.tableLayout.PerformLayout();
            this.Panel_Controls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.TableLayoutPanel Panel_Controls;
        private System.Windows.Forms.Button Button_Start_Server;
        private System.Windows.Forms.Button Button_Shutdown_Server;
        private System.Windows.Forms.TextBox Text_Console_Output;
        private System.Windows.Forms.Button Button_Abort_Server;
    }
}