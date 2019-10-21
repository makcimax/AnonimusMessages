﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using Client10.Service;

namespace Client10
{
    class MessageCallback : IServerCallback
    {
        public void cbSendMessage(string senderName, string message)
        {
           // Console.WriteLine(senderName + ":" + message);
        }
        public void cbShowAbonent(string abonentName, bool abonentStatus)
        {
            //Console.WriteLine(abonentName + " " + abonentStatus);
        }
    }


    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InstanceContext instanceContext = new InstanceContext(new MessageCallback());
            var client = new ServerClient(instanceContext);
            string name = "KIRIK";
            int connectId = client.Connect(name);
            client.SendMessage(connectId, null, "привет");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}