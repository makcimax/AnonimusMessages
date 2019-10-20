using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Message
    {
        private int srcAddress { get; set; }
        private int dstAddress { get; set; }
        private string text { get; set; }
        public Message() 
        { 
            srcAddress = 0;
            dstAddress = 0;
            text = "";
        }
        public Message(int src, int dst, string newText ) 
        {
            srcAddress = src;
            dstAddress = dst;
            text = newText; 
        }

        public void Edit(string newMessage)
        {
            text = newMessage;
        }

    }
}
