using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotChatWF
{
    public partial class EmojiForm : Form
    {
       public MainForm mForm;
        public string messageText = "";
        public EmojiForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void musicButton_Click(object sender, EventArgs e)
        {
            messageText += "\x266B";
        }

        private void faceButton_Click(object sender, EventArgs e)
        {
            messageText += "\x263A";
        }

        private void heartButton_Click(object sender, EventArgs e)
        {
            messageText += "\x2665";
        }

        private void sunButton_Click(object sender, EventArgs e)
        {
            messageText += "\x263C";
        }

        private void starButton_Click(object sender, EventArgs e)
        {
            messageText += "\x2736";
        }

        private void spadesButton_Click(object sender, EventArgs e)
        {
            messageText += "\x2660";
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            mForm.ChangeMessage(ref messageText);
            mForm.Show();
            Visible = false;
        }

        private void EmojiForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mForm.Show();
            Visible = false;
        }
    }
}
