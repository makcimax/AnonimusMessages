using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Client10.Service;
using System.ServiceModel;

//TODO колл-бэк показа абонентов
//     Мгновенная смена статуса пользователя у других(Спроси у Игоря лучше, чтобы потом не переделывать)

namespace Client10
{
    public partial class Form1 : Form, IServerCallback
    {
        int id;
        ServerClient client = null;
        Abonent[] AllAbonents;
        string userName;
        Status status = Status.Offline;
        public Form1()
        {
            InitializeComponent();
            this.ActiveControl = InputName;
        }
        private void ConnectMethod()
        {
            if (InputName.Text.Trim() == "")
            {
                MessageBox.Show("Incorrect data!!! Try again");
                return;
            }
            else
            {

                InstanceContext i = new InstanceContext(this);
                client = new ServerClient(i);

                userName = InputName.Text.Trim();
                id = client.Connect(userName);
                status = Status.Online;
                AllAbonents = client.ShowAbonents(id);


                OutputMessage.Enabled  = true;
                SendButton.Enabled     = true;
                InputMessage.Enabled   = true;
                AbonentList.Enabled    = true;
                ConnDisconnButton.Text = "Disconnect";
                InputName.ReadOnly     = true;
                ShowButton.Enabled     = true;
                ForAllCheck.Enabled    = true;
                this.Text              = userName;

                //Метод работы со списком
                DrawAbonentList(userName, status,AllAbonents);

                var h = client.ProvideMessage(id);
                foreach (var index in h)
                {
                    OutputMessage.Text += index.RecipientId + ": " + index.TextOfMessage + "\r";//Почему здесь стоит ID?
                   //клиент7: Console.WriteLine(allAbonents[index.SenderId].name + " : " + index.TextOfMessage);
                }

                
                this.ActiveControl = InputMessage;
                
            }
        }

        private void DisconnectMethod()
        {
            client.Disconnect(id);
            client = null;
            status = Status.Offline;

            //Метод работы со списком
            DrawAbonentList(userName, status,AllAbonents);

            OutputMessage.Enabled  = false;
            OutputMessage.Clear();
            SendButton.Enabled     = false;
            InputMessage.Enabled   = false;
            AbonentList.Enabled    = false;
            ConnDisconnButton.Text = "Connect";
            InputName.ReadOnly     = false;
            ShowButton.Enabled     = false;
            ForAllCheck.Enabled    = false;
            this.Text              = "Login";

        }

        private void SendMethod()
        {
            if (InputMessage.Text.Trim() == "")
            {
                return;
            }
            else
            {
                client.SendMessage(id, null, InputMessage.Text);
                InputMessage.Clear();
                this.ActiveControl = InputMessage;
            }   
        }
        private void ExitMethod()
        {
            if (status == Status.Online)
            {
                DisconnectMethod();
                Application.Exit();
            }
            else
            {
                Application.Exit();
            }
        }

        private int in_List(string userName)
        {
            int index = -1;
            for (int i = 0; i<AbonentList.Items.Count;++i)
            {
                string tmp = AbonentList.Items[i].ToString();
                tmp = tmp.Substring(0, tmp.IndexOf(':'));
                if (tmp  == userName)
                    index = i;
            }

            return index;
        }
        private void DrawAbonentList(string userName = "<default>", Status userStatus = Status.Offline, Abonent[] allUsers = null)
        {
            if (allUsers == null)
            {
                AbonentList.Items.Add(userName + ": " + userStatus);
            }
            else
            {
                int index;
               
                foreach(Abonent abonent in allUsers)
                {
                    if ((index = in_List(abonent.name)) != -1)
                    {
                        AbonentList.Items[index] = abonent.name + ": " + abonent.status;
                    }
                    else
                    {
                        AbonentList.Items.Add(abonent.name + ": " + abonent.status);
                    }
                }

                if ((index = in_List(userName)) != -1)
                {
                    AbonentList.Items[index] = userName + ": " + userStatus;
                }
                else
                {
                    AbonentList.Items.Add(userName + ": " + userStatus);
                }


            }
        }

        public void cbSendMessage(string senderName, string message)
        {
            OutputMessage.Text += senderName + ": " + message+"\r";
        }
        public void cbShowAbonent(Abonent abonent)
        {
            
        }

        private void ConnDisconnButton_Click(object sender, EventArgs e)
        {
            if (status == Status.Online)
            {
                DisconnectMethod();            
            }
            else
            {
                ConnectMethod();
            }
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            SendMethod();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            ExitMethod();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ExitMethod();
        }

        private void InputName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectMethod();
            }
        }

        private void InputMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendMethod();
                InputMessage.Clear();
            }
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            client.ShowAbonents(id);
        }
    }
}
