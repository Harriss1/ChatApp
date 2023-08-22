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
namespace ChatApp {
    public partial class Form1 : Form {
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

        private void lblHelloWorld_Click(object sender, EventArgs e) {

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
    }
}
