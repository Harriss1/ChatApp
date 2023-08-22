namespace FormsPrototyp {
    partial class Form1 {
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
            this.buttonClickThis = new System.Windows.Forms.Button();
            this.lblHelloWorld = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.showPictureButton = new System.Windows.Forms.Button();
            this.clearPictureButton = new System.Windows.Forms.Button();
            this.setBackgroundColorButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.textboxDialogueDisplay = new System.Windows.Forms.TextBox();
            this.textboxChatInput = new System.Windows.Forms.TextBox();
            this.buttonChatSubmit = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panelChatScroller = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonAddLabel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panelChatScroller.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClickThis
            // 
            this.buttonClickThis.Location = new System.Drawing.Point(855, 485);
            this.buttonClickThis.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClickThis.Name = "buttonClickThis";
            this.buttonClickThis.Size = new System.Drawing.Size(129, 19);
            this.buttonClickThis.TabIndex = 0;
            this.buttonClickThis.Text = "Mein erster Button <3";
            this.buttonClickThis.UseVisualStyleBackColor = true;
            this.buttonClickThis.Click += new System.EventHandler(this.ButtonClickThis_Click);
            // 
            // lblHelloWorld
            // 
            this.lblHelloWorld.AutoSize = true;
            this.lblHelloWorld.Location = new System.Drawing.Point(730, 469);
            this.lblHelloWorld.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHelloWorld.Name = "lblHelloWorld";
            this.lblHelloWorld.Size = new System.Drawing.Size(35, 13);
            this.lblHelloWorld.TabIndex = 1;
            this.lblHelloWorld.Text = "label1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(486, 515);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableLayoutPanel1.SetColumnSpan(this.pictureBox1, 2);
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(480, 457);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(3, 466);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(57, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Strech";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.showPictureButton);
            this.flowLayoutPanel1.Controls.Add(this.clearPictureButton);
            this.flowLayoutPanel1.Controls.Add(this.setBackgroundColorButton);
            this.flowLayoutPanel1.Controls.Add(this.closeButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(75, 466);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(408, 46);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // showPictureButton
            // 
            this.showPictureButton.AutoSize = true;
            this.showPictureButton.Location = new System.Drawing.Point(317, 3);
            this.showPictureButton.Name = "showPictureButton";
            this.showPictureButton.Size = new System.Drawing.Size(88, 23);
            this.showPictureButton.TabIndex = 0;
            this.showPictureButton.Text = "Show a picture";
            this.showPictureButton.UseVisualStyleBackColor = true;
            this.showPictureButton.Click += new System.EventHandler(this.ShowPictureButton_Click);
            // 
            // clearPictureButton
            // 
            this.clearPictureButton.AutoSize = true;
            this.clearPictureButton.Location = new System.Drawing.Point(217, 3);
            this.clearPictureButton.Name = "clearPictureButton";
            this.clearPictureButton.Size = new System.Drawing.Size(94, 23);
            this.clearPictureButton.TabIndex = 1;
            this.clearPictureButton.Text = "Clear the picture";
            this.clearPictureButton.UseVisualStyleBackColor = true;
            this.clearPictureButton.Click += new System.EventHandler(this.ClearPictureButton_Click);
            // 
            // setBackgroundColorButton
            // 
            this.setBackgroundColorButton.AutoSize = true;
            this.setBackgroundColorButton.Location = new System.Drawing.Point(92, 3);
            this.setBackgroundColorButton.Name = "setBackgroundColorButton";
            this.setBackgroundColorButton.Size = new System.Drawing.Size(119, 23);
            this.setBackgroundColorButton.TabIndex = 2;
            this.setBackgroundColorButton.Text = "Set background color";
            this.setBackgroundColorButton.UseVisualStyleBackColor = true;
            this.setBackgroundColorButton.Click += new System.EventHandler(this.SetBackgroundColorButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.AutoSize = true;
            this.closeButton.Location = new System.Drawing.Point(11, 3);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All file" +
    "s (*.*)|*.*";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel2.Controls.Add(this.textboxDialogueDisplay, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.textboxChatInput, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.buttonChatSubmit, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(720, 13);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(264, 411);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // textboxDialogueDisplay
            // 
            this.textboxDialogueDisplay.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel2.SetColumnSpan(this.textboxDialogueDisplay, 2);
            this.textboxDialogueDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textboxDialogueDisplay.Location = new System.Drawing.Point(3, 3);
            this.textboxDialogueDisplay.Multiline = true;
            this.textboxDialogueDisplay.Name = "textboxDialogueDisplay";
            this.textboxDialogueDisplay.ReadOnly = true;
            this.textboxDialogueDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textboxDialogueDisplay.Size = new System.Drawing.Size(258, 363);
            this.textboxDialogueDisplay.TabIndex = 0;
            // 
            // textboxChatInput
            // 
            this.textboxChatInput.AcceptsTab = true;
            this.textboxChatInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textboxChatInput.Location = new System.Drawing.Point(29, 372);
            this.textboxChatInput.Multiline = true;
            this.textboxChatInput.Name = "textboxChatInput";
            this.textboxChatInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textboxChatInput.Size = new System.Drawing.Size(232, 36);
            this.textboxChatInput.TabIndex = 1;
            // 
            // buttonChatSubmit
            // 
            this.buttonChatSubmit.BackColor = System.Drawing.SystemColors.Highlight;
            this.buttonChatSubmit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonChatSubmit.Location = new System.Drawing.Point(3, 372);
            this.buttonChatSubmit.Name = "buttonChatSubmit";
            this.buttonChatSubmit.Size = new System.Drawing.Size(20, 36);
            this.buttonChatSubmit.TabIndex = 2;
            this.buttonChatSubmit.Text = ">";
            this.buttonChatSubmit.UseVisualStyleBackColor = false;
            this.buttonChatSubmit.Click += new System.EventHandler(this.ButtonChatSubmit_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Controls.Add(this.panelChatScroller, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.buttonAddLabel, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(489, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(225, 463);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // panelChatScroller
            // 
            this.panelChatScroller.AutoScroll = true;
            this.tableLayoutPanel3.SetColumnSpan(this.panelChatScroller, 2);
            this.panelChatScroller.Controls.Add(this.textBox2);
            this.panelChatScroller.Controls.Add(this.textBox1);
            this.panelChatScroller.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChatScroller.Location = new System.Drawing.Point(3, 3);
            this.panelChatScroller.Name = "panelChatScroller";
            this.panelChatScroller.Size = new System.Drawing.Size(219, 364);
            this.panelChatScroller.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.HideSelection = false;
            this.textBox2.Location = new System.Drawing.Point(96, 205);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "ert";
            // 
            // textBox1
            // 
            this.textBox1.HideSelection = false;
            this.textBox1.Location = new System.Drawing.Point(96, 179);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "wqb";
            // 
            // buttonAddLabel
            // 
            this.buttonAddLabel.Location = new System.Drawing.Point(3, 373);
            this.buttonAddLabel.Name = "buttonAddLabel";
            this.buttonAddLabel.Size = new System.Drawing.Size(75, 23);
            this.buttonAddLabel.TabIndex = 1;
            this.buttonAddLabel.Text = "Add Label";
            this.buttonAddLabel.UseVisualStyleBackColor = true;
            this.buttonAddLabel.Click += new System.EventHandler(this.ButtonAddLabel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 515);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lblHelloWorld);
            this.Controls.Add(this.buttonClickThis);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panelChatScroller.ResumeLayout(false);
            this.panelChatScroller.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClickThis;
        private System.Windows.Forms.Label lblHelloWorld;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button showPictureButton;
        private System.Windows.Forms.Button clearPictureButton;
        private System.Windows.Forms.Button setBackgroundColorButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox textboxDialogueDisplay;
        private System.Windows.Forms.TextBox textboxChatInput;
        private System.Windows.Forms.Button buttonChatSubmit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panelChatScroller;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonAddLabel;
    }
}

