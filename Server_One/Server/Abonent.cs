using System.Runtime.Serialization;

namespace Server
{
    public enum Status
    {
        Online,
        Offline
    }

    [DataContract]
    public class Abonent
    {
        [DataMember]
        public int id;

        [DataMember]
        public string name;

        [DataMember]
        public Status status;
    }
}