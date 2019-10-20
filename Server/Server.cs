using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Server : IServer
    {
        private Base baseofData = new Base();
        private List<Abonent> allAbonents =  new List<Abonent>();
        private int idAbonent = 0;
        public void SendMessage(int senderId, string[] recipientNames, string message)
        {
            Abonent sender = allAbonents.Find(ab => ab.ID == senderId);
            Console.WriteLine(sender.Name + " отправляет всем сообщение");
            if (recipientNames == null) //оправить всем
            {
                foreach (Abonent index in allAbonents)
                {
                    index.Callback.cbSendMessage(sender.Name, message);
                }
                Console.WriteLine(sender.Name + " отправил всем сообщение");
            }
            else
            {
                foreach (string index in recipientNames)
                {
                    Abonent recipient = allAbonents.Find(ab => ab.Name == index);
                    if (recipient.Status)
                    {
                        recipient.Callback.cbSendMessage(sender.Name, message);
                    }
                    else
                    {
                        //PushMessage() сохранить сообщение в базу данных
                    }
                }
            }
           
           
        }
        public void ShowAbonents(int id)
        {
            Abonent abonent = allAbonents.Find(ab => ab.ID == id); //мб быстрее будет вызвать напрямую  OperationContext.Current.GetCallbackChannel<IMessageCallback>() ?
            foreach (Abonent index in allAbonents) // и стоит ли вообще вызывать столько callbackов, мб стоит добавить еще одну функцию cbShowAbonents(string[]names ,string[] status)
            {
               if (index.ID != id)
               {
                   abonent.Callback.cbShowAbonent(index.Name, index.Status);
               }
            }
        }

        private void ProvideMessage(int id) // мб его все таки включить в RPC и вызывать отдельно клиентом,чтобы не захламлять Connect
        {
            //PopMessage() взять сообщение из базы данных
            Abonent abonent = allAbonents.Find(ab => ab.ID == id);
            string Message = "сообщение из базы данных";
            string senderName = "Имя отправителя";
            abonent.Callback.cbSendMessage(senderName, Message);
        }
        public int Connect(string name)
        {
            Abonent abonent;
            string str;
            if (allAbonents.Exists(ab => ab.Name == name))
            {
                str = "существующий ";
                abonent = allAbonents.Find(ab => ab.Name == name);
                abonent.Callback = OperationContext.Current.GetCallbackChannel<IMessageCallback>();
                abonent.Status = true;
            }
            else
            {
                str = "новый ";
                abonent = new Abonent(idAbonent++, name, OperationContext.Current.GetCallbackChannel<IMessageCallback>());
                allAbonents.Add(abonent);
            }

            //Дать знать остальным пользователям о подключении нового
            foreach (Abonent index in allAbonents)
            {
                if (index.Status && index.ID != abonent.ID)
                {
                    index.Callback.cbShowAbonent(abonent.Name, true);
                }
            }

            //ProvideMessage(int id) //предоставить пользователю непринятые сообщения
            Console.WriteLine("Подключился " + str + abonent.Name);
            return abonent.ID;

        }
        public void Disconnect(int id) 
        {
            Abonent abonent = allAbonents.Find(ab => ab.ID == id);
            abonent.Status = false;
            abonent.Callback = null;
        }
    }
}
