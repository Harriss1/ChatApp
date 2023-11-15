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
            this.Button_Stop_Server = new System.Windows.Forms.Button();
            this.Button_Start_Server = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tableLayout.SuspendLayout();
            this.Panel_Controls.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayout
            // 
            this.tableLayout.ColumnCount = 1;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Controls.Add(this.Panel_Controls, 0, 1);
            this.tableLayout.Controls.Add(this.textBox1, 0, 0);
            this.tableLayout.Location = new System.Drawing.Point(4, 5);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 2;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayout.Size = new System.Drawing.Size(795, 444);
            this.tableLayout.TabIndex = 0;
            // 
            // Panel_Controls
            // 
            this.Panel_Controls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Controls.ColumnCount = 3;
            this.Panel_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Panel_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.Panel_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.Panel_Controls.Controls.Add(this.Button_Stop_Server, 2, 0);
            this.Panel_Controls.Controls.Add(this.Button_Start_Server, 1, 0);
            this.Panel_Controls.Location = new System.Drawing.Point(0, 414);
            this.Panel_Controls.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Controls.Name = "Panel_Controls";
            this.Panel_Controls.RowCount = 1;
            this.Panel_Controls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Panel_Controls.Size = new System.Drawing.Size(795, 30);
            this.Panel_Controls.TabIndex = 0;
            // 
            // Button_Stop_Server
            // 
            this.Button_Stop_Server.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Stop_Server.Location = new System.Drawing.Point(698, 3);
            this.Button_Stop_Server.Name = "Button_Stop_Server";
            this.Button_Stop_Server.Size = new System.Drawing.Size(94, 24);
            this.Button_Stop_Server.TabIndex = 1;
            this.Button_Stop_Server.Text = "Stoppen";
            this.Button_Stop_Server.UseVisualStyleBackColor = true;
            // 
            // Button_Start_Server
            // 
            this.Button_Start_Server.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Start_Server.Location = new System.Drawing.Point(598, 3);
            this.Button_Start_Server.Name = "Button_Start_Server";
            this.Button_Start_Server.Size = new System.Drawing.Size(94, 24);
            this.Button_Start_Server.TabIndex = 0;
            this.Button_Start_Server.Text = "Starten";
            this.Button_Start_Server.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.textBox1.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Margin = new System.Windows.Forms.Padding(0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(795, 414);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Dies ist ein Text";
            // 
            // ServerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
        private System.Windows.Forms.Button Button_Stop_Server;
        private System.Windows.Forms.TextBox textBox1;
    }
}