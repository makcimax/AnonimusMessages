using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Server
{
    [ServiceContract(CallbackContract = typeof(IMessageCallback))]
    public interface IServer
    {
        [OperationContract(IsOneWay = true)]
        void SendMessage(int senderId, string[] names, string message);

        [OperationContract(IsOneWay = true)]
        void ShowAbonents(int id);

        [OperationContract(IsOneWay = true)]
        void ProvideMessage(int id);

        [OperationContract]
        int Connect(string name);

        [OperationContract(IsOneWay = true)]
        void Disconnect(int id);
    }

}
