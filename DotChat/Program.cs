using System;
using Terminal.Gui;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using System.Net.Cache;
using System.Timers;

namespace DotChat
{
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
    class Program
    {
        private static MenuBar menu;
        private static Window winMain;
        private static Window winMessages;
        private static Label labelUsername;
        private static Label labelMessage;
        private static TextField fieldUsername;
        private static TextField fieldMessage;
        private static Button btnSend;
        private static Dialog reg;
        private static Dialog log;
        private static Dialog emoj;
        //For Register window
        private static Label labelName;
        private static Label labelPassword;
        private static Label labelRepeatPasswd;
        private static Label labelIPaddress;

        private static TextField textFieldName;
        private static TextField textFieldPassword;
        private static TextField textFieldRepeatPasswd;
        private static TextField textFieldIPaddress;
        private static string ipAddress;

        private static Button btnCanc;
        private static Button btnReg;

        private static int int_token2 = 0;

        private static bool leaveWithoLog = true;
        public static int counterLog = 0;
        public static int counterReg = 0;

        //For Login window
        private static Label labelNameLog;
        private static Label labelPasswordLog;
        private static Label labelIPaddressLog;

        private static TextField textFieldNameLog;
        private static TextField textFieldPasswordLog;
        private static TextField textFieldIPaddressLog;

        private static Button btnCancLog;
        private static Button btnLog;

        //For emoji window
        private static Button btnEmoji;

        private static Button star;
        private static Button sun;
        private static Button music;
        private static Button face;
        private static Button heart;
        private static Button spades;
        private static Button btnCancEmoj;
 
        private static List<Message> messages = new List<Message>();
        private static bool isLog = false;
        private static bool secondTimeVis = false;
        private static int lastMsgID;
        private static int messageCount = 0;
        public static string prevUser;
        public static bool leaveWithoAuthForm = true;
        public static bool leaveWithoRegForm = true;

        public static string user;
        static void Main(string[] args)
        {
            Application.Init();
           
            menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem("Menu", new MenuItem[] {
                    new MenuItem("Register", "Create your account", () => Register()),
                    new MenuItem("Login", "Log into your account", () => Login()),
                    new MenuItem("Quit", "Close the app", () => ExitClick()),
                }),
            }) {
                X = 0, Y = 0,
                Width = Dim.Fill(),
                Height = 1,
            };
            Application.Top.Add(menu);

            
            winMain = new Window() {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                Title = "DotChat",
            };
        
            Application.Top.Add(winMain);

          
            winMessages = new Window() {
                X = 0,
                Y = 0,
                Width = winMain.Width,
                Height = winMain.Height - 2,
            };
            winMain.Add(winMessages);

          
            labelUsername = new Label() { 
                X = 0,
                Y = Pos.Bottom(winMain) - 5,
                Width = 15,
                Height = 1,
                Text = "Username:",
                TextAlignment = TextAlignment.Right,
            };
            winMain.Add(labelUsername);

         
            labelMessage = new Label()
            {
                X = 0,
                Y = Pos.Bottom(winMain) - 4,
                Width = 15,
                Height = 1,
                Text = "Message:",
                TextAlignment = TextAlignment.Right,
            };
            winMain.Add(labelMessage);
            
          
            fieldUsername = new TextField()
            {
                X = 15,
                Y = Pos.Bottom(winMain) - 5,
                Width = winMain.Width - 15,
                Height = 1,
            };
            winMain.Add(fieldUsername);
            fieldUsername.Text = "Create your account or log in existing one";
            fieldUsername.ReadOnly = true;

        
            fieldMessage = new TextField()
            {
                X = 15,
                Y = Pos.Bottom(winMain) - 4,
                Width = winMain.Width - 15,
                Height = 1,
            };
            winMain.Add(fieldMessage);

         
            btnSend = new Button()
            {
                X = Pos.Right(winMain) - 15,
                Y = Pos.Bottom(winMain) - 4,
                Width = 15,
                Height = 1,
                Text = "Send",
            };
            btnSend.Clicked += OnBtnSendClick;
            winMain.Add(btnSend);

            // Create emoji button
            btnEmoji = new Button()
            {
                X = Pos.Right(winMain) - 15,
                Y = Pos.Bottom(winMain) - 5,
                Width = 15,
                Height = 1,
                Text = "Emoji",
            };
            btnEmoji.Clicked += Emoji;
            winMain.Add(btnEmoji);

            messageCount = 0;
            lastMsgID = 0;
            Timer updateLoop = new Timer();
            updateLoop.Interval = 1000;
            updateLoop.Elapsed += (object sender, ElapsedEventArgs e) => {
                
                Message msg = GetMessage(lastMsgID);
                if (msg != null) {
                    messages.Add(msg);
                    MessagesUpdate();
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
                    messages.Clear();
                    lastMsgID = 0;
                }
                else if (lastMsgID == messageCount)
                    messageCount = 0;
                
            };
            updateLoop.Start();
            try
            {
                Application.Run();
            }
            catch { }

        }

     
        static void OnBtnSendClick() {
            if (fieldUsername.Text.Length != 0 && fieldMessage.Text.Length != 0)
            {
                    Message msg = new Message()
                    {
                        username = fieldUsername.Text.ToString(),
                        text = fieldMessage.Text.ToString(),
                    };
                SendMessage(msg);
                fieldMessage.Text = "";
            }
            MessagesUpdate();

        }

        
        static void MessagesUpdate() {
            winMessages.RemoveAll();
            int offset = 0;
            for (var i = messages.Count - 1; i >= 0; i--) {
                View msg = new View() { 
                    X = 0, Y = offset,
                    Width = winMessages.Width,
                    Height = 1,
                    Text = $"[{messages[i].username}] {messages[i].text} {messages[i].timestamp}",
                };
                winMessages.Add(msg);
                
                offset++;
            }
            Application.Refresh();
        }

      
        static void SendMessage(Message msg) {
            if (isLog) {
                WebRequest req = WebRequest.Create($"http://{ipAddress}:5000/api/chat");
                req.Method = "POST";
                string postData = JsonConvert.SerializeObject(msg);
                byte[] bytes = Encoding.UTF8.GetBytes(postData);
                req.ContentType = "application/json";
                req.ContentLength = bytes.Length;
                Stream reqStream = req.GetRequestStream();
                reqStream.Write(bytes);
                reqStream.Close();

                req.GetResponse();
            }
        }
        
       
        static Message GetMessage(int id) {
            try
            {
                WebRequest req = WebRequest.Create($"http://{ipAddress}:5000/api/chat/{id}");
                WebResponse resp = req.GetResponse();
                string smsg = new StreamReader(resp.GetResponseStream()).ReadToEnd();

                if (smsg == "Not found") return null;
                return JsonConvert.DeserializeObject<Message>(smsg);
            }
            catch { return null; }
        }
        static void RegisterClick()
        {
            user = Convert.ToString(textFieldName.Text);

            string pass1 = Convert.ToString(textFieldPassword.Text);
            string pass2 = Convert.ToString(textFieldRepeatPasswd.Text);
            if (textFieldIPaddress.Text == "26.13.90.183")
            {
                ipAddress = Convert.ToString(textFieldIPaddress.Text);
            }
            else
            {
                MessageBox.ErrorQuery(30, 10, "Error", "Incorrect or inexistent IP Address", "OK");
                return;
            }
            if (pass1 == pass2)
            {

                WebRequest req = WebRequest.Create($"http://{ipAddress}:5000/api/reg");
                req.Method = "POST";
                AuthData auth_data = new AuthData();
                auth_data.login = Convert.ToString(textFieldName.Text);
                auth_data.password = pass1;
                string postData = JsonConvert.SerializeObject(auth_data);

                req.ContentType = "application/json";

                StreamWriter reqStream = new StreamWriter(req.GetRequestStream());
                reqStream.Write(postData);
                reqStream.Close();

                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                string content = sr.ReadToEnd();
                sr.Close();

                int int_token = Convert.ToInt32(content, 10);

                if (int_token != -1)
                {
                    if (secondTimeVis == true)
                    {
                        SendMessage(new Message($"User {user} disconnected", ""));
                    }
                    fieldUsername.Text = auth_data.login;
                    int_token2 = int_token;
                    secondTimeVis = true;
                    isLog = true;
                    leaveWithoLog = false;
                    counterReg++;
                    counterLog++;
                    SendMessage(new Message($"User {user} is registered", ""));
                    Application.RequestStop();
                }
                else
                {
                    MessageBox.ErrorQuery(30, 10, "Error", "This user is already exists", "OK");
                }
            }
            else
            {
                MessageBox.ErrorQuery(30, 10, "Error", "Passwords don't match", "OK");
            }
        }

        static void Register()
        {
            prevUser = Convert.ToString(fieldUsername.Text);
            reg = new Dialog("Register");

            //Add button which create an account and connect to chat using his IP Address
            btnReg = new Button()
            {
                X = Pos.Right(reg) - 80,
                Y = Pos.Bottom(reg) - 5,
                Width = 15,
                Height = 1,
                Text = "Register",
            };
            btnReg.Clicked += RegisterClick;
            reg.Add(btnReg);

            btnCanc = new Button()
            {
                X = Pos.Right(reg) - 60,
                Y = Pos.Bottom(reg) - 5,
                Width = 15,
                Height = 1,
                Text = "Cancel",
            };
            btnCanc.Clicked += Application.RequestStop;
            reg.Add(btnCanc);

            //Label login, password, again password and IP Address 
            labelName = new Label()
            {
                X = Pos.Bottom(reg),
                Y = Pos.Bottom(reg) - 25,
                Width = 15,
                Height = 1,
                Text = "Username:",
                TextAlignment = TextAlignment.Right,
            };
            reg.Add(labelName);

            labelPassword = new Label()
            {
                X = Pos.Bottom(reg),
                Y = Pos.Bottom(reg) - 20,
                Width = 15,
                Height = 1,
                Text = "Password:",
                TextAlignment = TextAlignment.Right,
            };
            reg.Add(labelPassword);

            labelRepeatPasswd = new Label()
            {
                X = Pos.Bottom(reg) - 1,
                Y = Pos.Bottom(reg) - 15,
                Width = 16,
                Height = 1,
                Text = "Repeat password:",
                TextAlignment = TextAlignment.Right,
            };
            reg.Add(labelRepeatPasswd);

            labelIPaddress = new Label()
            {
                X = Pos.Bottom(reg),
                Y = Pos.Bottom(reg) - 10,
                Width = 15,
                Height = 1,
                Text = "IP Address:",
                TextAlignment = TextAlignment.Right,
            };
            reg.Add(labelIPaddress);

            //Textfield for username, password, repeat password and IP Address

            textFieldName = new TextField()
            {
                X = Pos.Bottom(reg) + 15,
                Y = Pos.Bottom(reg) - 25,
                Width = 15,
                Height = 1,
            };
            reg.Add(textFieldName);

            textFieldPassword = new TextField()
            {
                X = Pos.Bottom(reg) + 15,
                Y = Pos.Bottom(reg) - 20,
                Width = 15,
                Height = 1,
            };
            reg.Add(textFieldPassword);
            

            textFieldRepeatPasswd = new TextField()
            {
                X = Pos.Bottom(reg) + 15,
                Y = Pos.Bottom(reg) - 15,
                Width = 15,
                Height = 1,
            };
            reg.Add(textFieldRepeatPasswd);

            textFieldIPaddress = new TextField()
            {
                X = Pos.Bottom(reg) + 15,
                Y = Pos.Bottom(reg) - 10,
                Width = 15,
                Height = 1,
            };
            reg.Add(textFieldIPaddress);

            Application.Run(reg);

        }
        static void LoginClick()
        {
            if (textFieldIPaddressLog.Text == "26.13.90.183")
            {
                ipAddress = Convert.ToString(textFieldIPaddressLog.Text);
            }
            else
            {
                MessageBox.ErrorQuery(30, 10, "Error", "Incorrect or inexistent IP Address", "OK");
                return;
            }
            string address = $"http://{ipAddress}:5000/api/Auth";
            string login = Convert.ToString(textFieldNameLog.Text);
            string passwd = Convert.ToString(textFieldPasswordLog.Text);
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
                    MessageBox.ErrorQuery(30, 10, "Error", "Incorrect password", "OK");
                    break;
                case -2:
                    MessageBox.ErrorQuery(30, 10, "Error", "Inexistent username", "OK");
                    break;
                default:
                    if (secondTimeVis == true)
                    {
                        SendMessage(new Message($"User {fieldUsername.Text} disconnected", ""));
                    }
                    fieldUsername.Text = login;
                    int_token2 = int_token;
                    isLog = true;
                    leaveWithoLog = false;
                    secondTimeVis = true;
                    counterLog++;
                    counterReg++;
                    SendMessage(new Message($"User {fieldUsername.Text} is now online", ""));
                    Application.RequestStop();
                    break;
            }
        }

        static void Login()
        {
            prevUser = Convert.ToString(fieldUsername.Text);
            log = new Dialog("Login");

            //Add button which create an account and connect to chat using his IP Address
            btnLog = new Button()
            {
                X = Pos.Right(log) - 80,
                Y = Pos.Bottom(log) - 5,
                Width = 15,
                Height = 1,
                Text = "Login",
            };
            btnLog.Clicked += LoginClick;
            log.Add(btnLog);

            btnCancLog = new Button()
            {
                X = Pos.Right(log) - 60,
                Y = Pos.Bottom(log) - 5,
                Width = 15,
                Height = 1,
                Text = "Cancel",
            };
            btnCancLog.Clicked += Application.RequestStop;
            log.Add(btnCancLog);

            //Label login, password, again password and IP Address 
            labelNameLog = new Label()
            {
                X = Pos.Bottom(log),
                Y = Pos.Bottom(log) - 25,
                Width = 15,
                Height = 1,
                Text = "Username:",
                TextAlignment = TextAlignment.Right,
            };
            log.Add(labelNameLog);

            labelPasswordLog = new Label()
            {
                X = Pos.Bottom(log),
                Y = Pos.Bottom(log) - 20,
                Width = 15,
                Height = 1,
                Text = "Password:",
                TextAlignment = TextAlignment.Right,
            };
            log.Add(labelPasswordLog);

            labelIPaddressLog = new Label()
            {
                X = Pos.Bottom(log),
                Y = Pos.Bottom(log) - 15,
                Width = 15,
                Height = 1,
                Text = "IP Address:",
                TextAlignment = TextAlignment.Right,
            };
            log.Add(labelIPaddressLog);

            //Textfield for username, password, repeat password and IP Address

            textFieldNameLog = new TextField()
            {
                X = Pos.Bottom(log) + 15,
                Y = Pos.Bottom(log) - 25,
                Width = 15,
                Height = 1,
            };
            log.Add(textFieldNameLog);

            textFieldPasswordLog = new TextField()
            {
                X = Pos.Bottom(log) + 15,
                Y = Pos.Bottom(log) - 20,
                Width = 15,
                Height = 1,
            };
            log.Add(textFieldPasswordLog);

            textFieldIPaddressLog = new TextField()
            {
                X = Pos.Bottom(log) + 15,
                Y = Pos.Bottom(log) - 15,
                Width = 15,
                Height = 1,
            };
            log.Add(textFieldIPaddressLog);

            Application.Run(log);
        }
        static void ExitClick()
        {
            if (leaveWithoLog)
            {

            }
            else
            {
                SendMessage(new Message(Convert.ToString(fieldUsername.Text) + " disconnected", ""));
            }
            Application.RequestStop();
        }
        static void Emoji()
        {
            emoj = new Dialog("Choose your emoji");
            //Cancel button
            btnCancEmoj = new Button()
            {
                X = Pos.Right(emoj) - 70,
                Y = Pos.Bottom(emoj) - 5,
                Width = 15,
                Height = 1,
                Text = "Cancel",
            };
            btnCancEmoj.Clicked += Application.RequestStop;
            emoj.Add(btnCancEmoj);

            //Music button
            music = new Button()
            {
                X = Pos.Right(emoj) - 90,
                Y = Pos.Bottom(emoj) - 15,
                Width = 15,
                Height = 1,
                Text = "Music",
            };
            music.Clicked += MusicClick;
            emoj.Add(music);

            //Face button
            face = new Button()
            {
                X = Pos.Right(emoj) - 90,
                Y = Pos.Bottom(emoj) - 10,
                Width = 15,
                Height = 1,
                Text = "Face",
            };
            face.Clicked += FaceClick;
            emoj.Add(face);

            //Heart button
            heart = new Button()
            {
                X = Pos.Right(emoj) - 70,
                Y = Pos.Bottom(emoj) - 15,
                Width = 15,
                Height = 1,
                Text = "Heart",
            };
            heart.Clicked += HeartClick;
            emoj.Add(heart);


            //Sun button
            sun = new Button()
            {
                X = Pos.Right(emoj) - 70,
                Y = Pos.Bottom(emoj) - 10,
                Width = 15,
                Height = 1,
                Text = "Sun",
            };
            sun.Clicked += SunClick;
            emoj.Add(sun);

            //Star button
            star = new Button()
            {
                X = Pos.Right(emoj) - 50,
                Y = Pos.Bottom(emoj) - 15,
                Width = 15,
                Height = 1,
                Text = "Star",
            };
            star.Clicked += StarClick;
            emoj.Add(star);

            //Spades button
            spades = new Button()
            {
                X = Pos.Right(emoj) - 50,
                Y = Pos.Bottom(emoj) - 10,
                Width = 15,
                Height = 1,
                Text = "Spades",
            };
            spades.Clicked += SpadesClick;
            emoj.Add(spades);

            Application.Run(emoj);
        }
        static void MusicClick()
        {
            fieldMessage.Text += "\x266B";
        }
        static void FaceClick()
        {
            fieldMessage.Text += "\x263A";
        }
        static void HeartClick()
        {
            fieldMessage.Text += "\x2665";
        }
        static void SunClick()
        {
            fieldMessage.Text += "\x263C";
        }
        static void StarClick()
        {
            fieldMessage.Text += "\x2736";
        }
        static void SpadesClick()
        {
            fieldMessage.Text += "\x2660";
        }
    }
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

}
