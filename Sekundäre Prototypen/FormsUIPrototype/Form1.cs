using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// Basis Forms Prototyp
/// Quelle: 
/// https://learn.microsoft.com/en-us/visualstudio/get-started/csharp/tutorial-windows-forms-picture-viewer-layout?view=vs-2022
/// </summary>
namespace FormsPrototyp {
    public partial class Form1 : Form {
        private int textboxCount = 0;
        private TextBox lastTextBox = null;
        private Panel lastPanel = null;
        public Form1() {
            InitializeComponent();
        }

        private void ButtonClickThis_Click(object sender, EventArgs e) {
            string myMessage = "I hob dis geändort.";
            if (lblHelloWorld.Text.Equals(myMessage)) {
                lblHelloWorld.Text = "Nomma geändorrrrTTTT!!!";
            } else {
                lblHelloWorld.Text = myMessage;
            }
            
        }

        private void ShowPictureButton_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void ClearPictureButton_Click(object sender, EventArgs e) {
            pictureBox1.Image = null;
            //pictureBox1 = null;
        }

        private void SetBackgroundColorButton_Click(object sender, EventArgs e) {
            if (colorDialog1.ShowDialog() == DialogResult.OK) {
                pictureBox1.BackColor = colorDialog1.Color;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e) {
            if (checkBox1.Checked) {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            } else {
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            }
        }

        private void ButtonChatSubmit_Click(object sender, EventArgs e) {
            string inputText = textboxChatInput.Text + "\r\n"; //or /n,&#13;
            textboxChatInput.Text = "";
            textboxDialogueDisplay.Text += inputText;
        }

        private void ButtonAddLabel_Click(object sender, EventArgs e) {
            Point locator = new Point(0, 0);
            if (lastTextBox != null) {
                panelChatScroller.VerticalScroll.Value = panelChatScroller.VerticalScroll.Maximum;
                locator = lastTextBox.Location;
                locator.X = 0;
                locator.Offset(0, lastTextBox.Height+2);
            }
            TextBox textbox= new TextBox();
            textbox.Text = "neue Textbox";
            textbox.Location = locator;
            //textbox.Location = new Point(10, textboxCount*20);
            textboxCount++;
            panelChatScroller.Controls.Add(textbox);
            lastTextBox = textbox;
        }

        private void ButtonAddPanel_Click(object sender, EventArgs e) {
            Point locator = new Point(0, 0);
            if (lastPanel != null) {
                locator = lastPanel.Location;
                locator.X = 0;
                locator.Offset(0, lastPanel.Height + 2);
            }

            Panel panel = new Panel();
            TextBox nameBox = new TextBox();
            nameBox.Text = "Henry Stanzel";

            TextBox messageBox = new TextBox();
            messageBox.Text = "Dies ist seine erste Nachricht";
            Size size = TextRenderer.MeasureText(messageBox.Text, messageBox.Font);
            messageBox.Width = size.Width;
            messageBox.Height = size.Height;
            messageBox.Top = nameBox.Height+2;
            panel.Controls.Add(nameBox);
            panel.Controls.Add(messageBox);
            panel.Location = locator;
            panel.Height = messageBox.Height + nameBox.Height + 4;
            panelChatScroller.Controls.Add(panel);
            lastPanel = panel;
            panelChatScroller.VerticalScroll.Value = panelChatScroller.VerticalScroll.Maximum;
        }
    }
}
