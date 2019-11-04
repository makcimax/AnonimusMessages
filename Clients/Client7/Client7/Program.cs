using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Client7.Server1;

namespace Client7
{

    class MessageCallback: Server1.IServerCallback
    {
        public void cbSendMessage(string senderName, string message)
        {
            Console.WriteLine(senderName+ ":"+ message);
        }
        public void cbShowAbonent(string abonentName, bool abonentStatus)
        {
            Console.WriteLine(abonentName+" "+abonentStatus);
            
        }
    }



class Program
    {
        static void Main(string[] args)
        {
            InstanceContext instanceContext = new InstanceContext(new MessageCallback());
            var client = new ServerClient(instanceContext);
            string name = Console.ReadLine();
            int connectId = client.Connect(name);
            while (true)
            {
                string a = Console.ReadLine();
                if (a == "s") client.ShowAbonents(connectId);
                if (a == "d") client.Disconnect(connectId);
                if (a == "c") client.Connect(name);
                if (a == "send") client.SendMessage(connectId, null, "Привет всем");
                if (a == "p") client.ProvideMessage(connectId);
            }
          




            // string[] abc = { "Igor", "Petr" };
            //client.SendMessage(connectId, abc, "hi");
            //client.ShowAbonents(connectId);
            // client.Disconnect(connectId);
        }
    }
}
