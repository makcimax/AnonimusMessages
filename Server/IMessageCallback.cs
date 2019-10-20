using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
namespace Server
{
    public interface IMessageCallback
    {
        [OperationContract(IsOneWay = true)]
        void cbSendMessage(string senderName, string message);
        
        [OperationContract(IsOneWay = true)]
        void cbShowAbonent(string abonentName, bool abonentStatus);
    }
}
