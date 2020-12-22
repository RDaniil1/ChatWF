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
using Newtonsoft.Json;
using System.Text.Json;

namespace DotChatWF
{
 
    public partial class AuthentificationForm : Form
  {
        
        public AuthentificationForm()
    {
      InitializeComponent();
    }
        public MainForm mForm;
        [Serializable]
        public class AuthData
        {
            public string token { get; set; } = default;
            public string login { get; set; } = default;
            public string password { get; set; } = default;
            public AuthData()
            {
                token = default;
                login = default;
                password = default;
            }
            public AuthData(string login, string password)
            {
                this.login = login;
                this.password = password;
            }
        }
        private void button1_Click(object sender, EventArgs e)
    {

           
            if (textBox4.Text == "127.0.0.1" || textBox4.Text == "localhost")
            {
                MainForm.ipAddress = textBox4.Text;
            }
            else
            {
                MessageBox.Show("Incorrect or inexistent IP Address");
                return;
            }
            string address = $"http://{MainForm.ipAddress}:5000/api/Auth";
            string login = textBox1.Text;
            string passwd = textBox2.Text;
            AuthData dataPerson = new AuthData(login.ToString(), passwd.ToString());
            string json = System.Text.Json.JsonSerializer.Serialize(dataPerson);

            WebRequest req = WebRequest.Create(address);
            req.Method = "POST";
            req.ContentType = "application/json";     
            
            using (StreamWriter r = new StreamWriter(req.GetRequestStream()))
            {
                r.Write(json);
                r.Close();
            }

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding("utf-8"));
            string content = sr.ReadToEnd();
            sr.Close();

            int int_token = Convert.ToInt32(content, 10);
            switch (int_token)
            {
                case -1:
                    MessageBox.Show("Incorrect password.");
                    break;
                case -2:
                    MessageBox.Show("Inexistent username.");
                    break;
                default:
                    mForm.TextBox_username.Text = login;
                    MainForm.leaveWithoLog = false;
                    MainForm.leaveWithoAuthForm = false;
                    MainForm.counterLog++;
                    mForm.Show();
                    Visible = false;
                    mForm.int_token = int_token;
                    break;
            }

          
        }

        private void AuthForm_Closing(object sender, FormClosingEventArgs e)
        {

        }

        private void AuthForm_Closed(object sender, FormClosedEventArgs e)
        {

                mForm.Show();
                Visible = false;

        }

        private void AuthForm_Load(object sender, EventArgs e)
        {

        }
    }
}
