using System;
using System.Windows.Forms;
using System.ServiceModel;
using Client_v1.Service;

namespace Client_v1
{

    //279,179
    public partial class Form1 : Form, IServerCallback
    {
        ServerClient client;
        int id;
        string nameInList;

        private void SendMethod()
        {
            if (InputMessage.Text == "")
            {
                return;
            }
            else
            {
                client.SendMessage(id, null, InputMessage.Text);
                InputMessage.Text = string.Empty;
            } 
        }

        private void ConnectMethod()
        {
            if (InputName.Text == "")
            {
                MessageBox.Show("Incorrect data!");
                ConnectButton.Enabled = true;
                return;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.ControlBox = true;
                this.Text = InputName.Text;
                this.AllABonents.Enabled = true;
                this.InputMessage.Enabled = true;
                this.SendButton.Enabled = true;
                this.OutputMessage.Enabled = true;
                this.ConnectButton.Text = "Disconnect";
                this.InputName.ReadOnly = true;


                InstanceContext instanceContext = new InstanceContext(this);
                client = new ServerClient(instanceContext);
                id = client.Connect(InputName.Text);
                nameInList = id.ToString() + " " + InputName.Text;
                AllABonents.Items.Add(nameInList);

            }
            
        }

        public Form1()
        {
            InitializeComponent();
            this.ActiveControl = InputName;
            
        }

        public void cbSendMessage(string senderName, string message)
        {
            if (message == "") return;
            else
            {
                OutputMessage.Text += senderName + ": " + message + "\r";
            }
        }

        public void cbShowAbonent(string abonentName, bool abonentStatus)
        {
            throw new NotImplementedException();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if(ConnectButton.Text == "Connect")
            {
                ConnectMethod();
            }
            else
            {
                MessageBox.Show("Disconnect is done");
            }
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            SendMethod();
        }

        private void InputMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendMethod();
        }

        private void InputName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ConnectMethod();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            //ДИсконнект
            Application.Exit();
        }
    }
}
