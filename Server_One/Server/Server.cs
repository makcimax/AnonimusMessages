using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;


namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class Server : IServer
    {
        private List<Abonent> allAbonents;
        private Dictionary<int, IMessageCallback> links;
        private ILogger logger;
        private int idAbonent;

        public Server()
        {
            allAbonents = new List<Abonent>();
            links = new Dictionary<int, IMessageCallback>();
            logger = new ConsoleLogger();
            idAbonent = 0;
        }

        private void PushMessage(int senderId, int recipientId, string textOfMessage)
        {
            using (var context = new DataBase())
            {
                var message = new Message()
                {
                    SenderId = senderId,
                    RecipientId = recipientId,
                    TextOfMessage = textOfMessage
                };

                context.Messages.Add(message);
                context.SaveChanges();
            }
        }

        private List<Message> PopMessage(int recipientId)
        {
            using (var context = new DataBase())
            {
                List<Message> messagesInDb = (context.Messages.Where(i => i.RecipientId == recipientId)).ToList();

                foreach (Message message in messagesInDb)
                    context.Messages.Remove(message);

                context.SaveChanges();
                return messagesInDb;
            }
        }

        public void SendMessage(int senderId, List<int> recipientNames, string message)
        {
            Abonent sender = allAbonents.Find(ab => ab.id == senderId);
            if (recipientNames == null) //оправить всем
            {
                foreach (var index in links.Keys)
                {
                    if (allAbonents[index].status == Status.Online)
                    {
                        links[index].cbSendMessage(sender.name, message);
                    }
                    else
                    {
                        PushMessage(sender.id, allAbonents[index].id, message);
                    }
                }

                logger.Logging(sender.name + " отправил всем сообщение");
            }
            else
            {
                foreach (var index in recipientNames)
                {
                    if (allAbonents[index].status == Status.Online)
                    {
                        links[index].cbSendMessage(sender.name, message);
                    }
                    else
                    {
                        PushMessage(senderId, index, message); //сохранить сообщение в базу данных
                    }
                }
            }
        }
        public List<Abonent> ShowAbonents(int id)
        {
            Abonent abonent = allAbonents.Find(ab => ab.id == id);
            return allAbonents;
        }

        public List<Message> ProvideMessage(int id)
        {
            Abonent recipient = allAbonents.Find(ab => ab.id == id);
            return PopMessage(recipient.id); 
        }

        public int Connect(string name)
        {
            Abonent abonent;
            string typeConnect;
            if (allAbonents.Exists(ab => ab.name == name))
            {
                abonent = allAbonents.Find(ab => ab.name == name);
                if (abonent.status == Status.Online)
                {
                    logger.Logging("Попытка повторного входа!");
                    return -1;
                }

                typeConnect = "существующий ";
                links[abonent.id] = OperationContext.Current.GetCallbackChannel<IMessageCallback>();
                abonent.status = Status.Online;
            }
            else
            {
                typeConnect = "новый ";
                abonent = new Abonent()
                {
                    id = idAbonent++,
                    name = name,
                    status = Status.Online 
                };
                allAbonents.Add(abonent);
                links[abonent.id] = OperationContext.Current.GetCallbackChannel<IMessageCallback>();
            }

            //Дать знать остальным пользователям о подключении нового
            foreach (var index in  links.Keys)
            {
                if (allAbonents[index].status == Status.Online && allAbonents[index].id != abonent.id)
                {
                    links[index].cbShowAbonent(abonent.name, abonent.status);
                }
            }

            logger.Logging("Подключился " + typeConnect + abonent.name);
            return abonent.id;

        }
        public void Disconnect(int id)
        {
            Abonent abonent = allAbonents.Find(ab => ab.id == id);
            if(abonent.status == Status.Offline)
            {
                logger.Logging("Клиент уже отключен");
                return;
            }

            logger.Logging("Отключился " + abonent.name);
            abonent.status = Status.Offline;
            links[abonent.id] = null;
            foreach (var index in links.Keys)
            {
                if (allAbonents[index].status == Status.Online && allAbonents[index].id != abonent.id)
                {
                    links[index].cbShowAbonent(abonent.name, abonent.status);
                }
            }
        }
    }
}
