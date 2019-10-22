using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client10.Service;
using System.ServiceModel;

namespace Client10
{
    public partial class Form1 : Form, IServerCallback
    {
        int id;
        ServerClient client;
        string userName;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            InstanceContext i = new InstanceContext(this);
            client = new ServerClient(i);
          
            userName = textBox1.Text;
            id = client.Connect(userName);
            client.ProvideMessage(id);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            client.SendMessage(id, null, richTextBox2.Text);
        }

        public void cbSendMessage(string senderName, string message)
        {
            richTextBox1.Text += senderName + ": " + message;
        }
        public void cbShowAbonent(string abonentName, bool abonentStatus)
        {
            
        }
    }
}
