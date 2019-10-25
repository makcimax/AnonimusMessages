using System;
using System.Linq;
using System.Windows.Forms;
using Client10.Service;
using System.ServiceModel;

namespace Client10
{
    public partial class Form1 : Form, IServerCallback
    {
        int id;
        ServerClient client = null;
        string userName;
        bool Online = false;
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

                userName = InputName.Text;
                id = client.Connect(userName);
                Online = true;


                OutputMessage.Enabled  = true;
                SendButton.Enabled     = true;
                InputMessage.Enabled   = true;
                AbonentList.Enabled    = true;
                ConnDisconnButton.Text = "Disconnect";
                InputName.ReadOnly     = true;
                ShowButton.Enabled     = true;
                this.Text              = userName;

                client.ProvideMessage(id);
                this.ActiveControl = InputMessage;
                
            }
        }

        private void DisconnectMethod()
        {
            client.Disconnect(id);
            client = null;
            Online = false;


            OutputMessage.Enabled  = false;
            OutputMessage.Clear();
            SendButton.Enabled     = false;
            InputMessage.Enabled   = false;
            AbonentList.Enabled    = false;
            ConnDisconnButton.Text = "Connect";
            InputName.ReadOnly     = false;
            ShowButton.Enabled     = false;
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
            if (Online)
            {
                DisconnectMethod();
                Application.Exit();
            }
            else
            {
                Application.Exit();
            }
        }


        public void cbSendMessage(string senderName, string message)
        {
            OutputMessage.Text += senderName + ": " + message+"\r";
        }
        public void cbShowAbonent(string abonentName, bool abonentStatus)
        {
           
        }

        private void ConnDisconnButton_Click(object sender, EventArgs e)
        {
            if (Online)
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
            }
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            client.ShowAbonents(id);
        }
    }
}
