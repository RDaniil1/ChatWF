using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotChatWF
{

    public partial class MainForm : Form
    {
        

        int lastMsgID = 0;
        EmojiForm EmojForm;
        AuthentificationForm AuthForm;
        RegistartionForm RegForm;
        public TextBox TextBox_username;
        public int int_token;
        public static string ipAddress;
        public static bool leaveWithoLog = true;
        public static bool leaveWithoAuthForm = true;
        public static bool leaveWithoRegForm = true;
        public static int counterLog = 0;
        public static int counterReg = 0;
        public static string prevUser;
        public static bool isDeleted = false;
        public static int messageCount = 0;
        public MainForm()
        {
            InitializeComponent();
        }
        public void ChangeMessage(ref string message)
        {
            fieldMessage.Text = message;
            
        }
        private void updateLoop_Tick(object sender, EventArgs e)
        {
            
            if(!leaveWithoAuthForm && counterLog != 2)
            {
                SendMessage(new Message(fieldUsername.Text + " is now online", ""));
                leaveWithoAuthForm = true;
            }
            else if (!leaveWithoRegForm && counterReg != 2)
            {
                SendMessage(new Message(fieldUsername.Text + " succefully registered", ""));
                leaveWithoRegForm = true;
            }
            if( (counterLog == 2 || counterReg == 2))
            {
                SendMessage(new Message(prevUser + " disconnected", ""));
                if(counterLog == 2)
                counterLog--;
                if (counterReg == 2)
                    counterReg--;
            }
            Message msg = GetMessage(lastMsgID);
            //If message deleted, then refresh the listBox 
            if (msg != null) {
                listMessages.Items.Add($"[{msg.username}] {msg.text} {msg.timestamp}");
                lastMsgID++;
            }

            msg = GetMessage(messageCount);
            if (msg != null)
            {
                messageCount++;
                msg = GetMessage(messageCount);
            }
            else if (msg == null && messageCount < lastMsgID)
            {
                listMessages.Items.Clear();
                lastMsgID = 0;
            }
            else if (lastMsgID == messageCount)
                messageCount = 0;

        }

        private void btnSend_Click(object sender, EventArgs e) {

            if (int_token == 0)
      {
        MessageBox.Show("Please log in or register");
      }
      else 
      {

                    SendMessage(new Message()
                    {
                        username = fieldUsername.Text,
                        text = fieldMessage.Text,
                    });


      }
    }

        public void SendMessage(Message msg)
        {
            WebRequest req = WebRequest.Create($"http://{ipAddress}:5000/api/chat");
            req.Method = "POST";
            string postData = JsonConvert.SerializeObject(msg);
            req.ContentType = "application/json";
            StreamWriter reqStream = new StreamWriter(req.GetRequestStream());
            reqStream.Write(postData);
            reqStream.Close();
            req.GetResponse();
        }
        Message GetMessage(int id)
        {
            try
            {
                WebRequest req = WebRequest.Create($"http://{ipAddress}:5000/api/chat/{id}");
                WebResponse resp = req.GetResponse();
                string smsg = new StreamReader(resp.GetResponseStream()).ReadToEnd();

                if (smsg == "Not found") return null;
                return JsonConvert.DeserializeObject<Message>(smsg);
            } catch {
                return null;
            }
        }

    private void btnAuth_Click(object sender, EventArgs e)
    {
            prevUser = fieldUsername.Text;
            AuthForm.mForm = this;
            AuthForm.Show();
            Visible = false;
    }
        
    private void MainForm_Load(object sender, EventArgs e)
    {
      int_token = 0;
      AuthForm = new AuthentificationForm();
      RegForm = new RegistartionForm();
      EmojForm = new EmojiForm();
      TextBox_username = fieldUsername;
    }

    private void btnReg_Click(object sender, EventArgs e)
    {
            prevUser = fieldUsername.Text;
      RegForm.mForm = this;
      RegForm.Show();
      Visible = false;
    }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (leaveWithoLog)
            {

            }
            else
            {
                SendMessage(new Message(fieldUsername.Text + " disconnected", ""));
            }
        }

        private void listMessages_MouseClick(object sender, MouseEventArgs e)
        {
            if(fieldUsername.Text == "admin")
            {
                int index = listMessages.SelectedIndex;
                listMessages.Items.RemoveAt(index);
                string convIndex = Convert.ToString(index + 1);
                string deleteMsg = string.Concat("delete:", convIndex);
                SendMessage(new Message("admin", deleteMsg));
            }
        }

        private void emoji_Click(object sender, EventArgs e)
        {
            if (leaveWithoLog == false)
            {
                EmojForm.mForm = this;
                EmojForm.Show();
                Visible = false;
            }
            else MessageBox.Show("Please log in or register");
        }

        private void emojiButton_LocationChanged(object sender, EventArgs e)
        {

        }
    }
    [Serializable]
    public class Message
    {
        public string username = "";
        public string text = "";
        public DateTime timestamp;
        public Message()
        {
            username = "";
            text = "";
            timestamp = DateTime.Now;
        }
        public Message(string name, string text)
        {
            this.username = name;
            this.text = text;
            timestamp = DateTime.Now;
        }
    }

}
