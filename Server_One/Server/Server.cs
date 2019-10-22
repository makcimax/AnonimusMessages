using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Entity;


namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Reentrant//,
       // UseSynchronizationContext =false 
  )]
    public class Server : IServer
    {
        // private DataBase baseofData = new DataBase();
        private List<Abonent> allAbonents = new List<Abonent>();
        private int idAbonent = 0;
        private void PushMessage(int senderId, int recipientId, string textOfMessage)
        {
            using (var context = new DataBase())
            {
                var message = new MessageDb()
                {
                    SenderId = senderId,
                    RecipientId = recipientId,
                    TextOfMessage = textOfMessage
                };

                context.Messages.Add(message);
                context.SaveChanges();
            }
        }
        private List<MessageDb> PopMessage(int recipientId)
        {
            using (var context = new DataBase())
            {
                List<MessageDb> messagesInDb = (context.Messages.Where(i => i.RecipientId == recipientId)).ToList();

                foreach (MessageDb message in messagesInDb)
                    context.Messages.Remove(message);

                context.SaveChanges();
                return messagesInDb;
            }
        }

        public void SendMessage(int senderId, string[] recipientNames, string message)
        {
            Abonent sender = allAbonents.Find(ab => ab.ID == senderId);
            if (recipientNames == null) //оправить всем
            {
                foreach (Abonent index in allAbonents)
                {
                    if (index.Status)
                    {
                        index.Callback.cbSendMessage(sender.Name, message);
                    }
                    else
                    {
                        PushMessage(sender.ID, index.ID, message);
                        index.IsNewMessage = true;
                    }
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
                        PushMessage(senderId, recipient.ID, message); //сохранить сообщение в базу данных
                        recipient.IsNewMessage = true;
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

        public void ProvideMessage(int id) // мб его все таки включить в RPC и вызывать отдельно клиентом,чтобы не захламлять Connect
        {

            Abonent abonent = allAbonents.Find(ab => ab.ID == id);
            if (abonent.IsNewMessage)
            {

                List<MessageDb> messagesInDb = PopMessage(abonent.ID); //взять сообщение из базы данных
                //    
                foreach (MessageDb message in messagesInDb)
                {
                    Abonent sender = allAbonents.Find(s => s.ID == message.SenderId);
                    abonent.Callback.cbSendMessage(sender.Name, message.TextOfMessage);
                }
                abonent.IsNewMessage = false;
            }
        }

            public int Connect(string name)
        {
            Abonent abonent;
            string str;
            if (allAbonents.Exists(ab => ab.Name == name))
            {
                
                abonent = allAbonents.Find(ab => ab.Name == name);
                if (abonent.Status)
                {
                    Console.WriteLine("Попытка повторного входа!");
                    return -1;
                }
                str = "существующий ";
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
            //ProvideMessage(abonent.ID); //предоставить пользователю непринятые сообщения
            Console.WriteLine("Подключился " + str + abonent.Name);
            return abonent.ID;

        }
        public void Disconnect(int id)
        {
            Abonent abonent = allAbonents.Find(ab => ab.ID == id);
            if(abonent.Status == false)
            {
                Console.WriteLine("Абонент уже отключен");
                return;
            }
            Console.WriteLine("Отключился " + abonent.Name);
            abonent.Status = false;
            abonent.Callback = null;
            foreach (Abonent index in allAbonents)
            {
                if (index.Status && index.ID != abonent.ID)
                {
                    index.Callback.cbShowAbonent(abonent.Name, false);
                }
            }
        }
    }
}
