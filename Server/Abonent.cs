using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Abonent
    {
        private int _id;
        private string _name;
        private bool _status;
        private bool _isNewMessage;
        private IMessageCallback _callback;
        public int ID { get { return _id; } }
        public string Name { get { return _name; } set {  _name = value; } }
        public ref bool Status { get { return ref _status; } }
        public bool IsNewMessage { get { return _isNewMessage; } set { _isNewMessage = value; } }
        public ref IMessageCallback Callback { get { return ref _callback; }}
        public Abonent(int id, string name, IMessageCallback callback)
        {
            _id = id;
            _name = name;
            _status = true;
            _isNewMessage = false;
            _callback = callback;
        }
    }
}
