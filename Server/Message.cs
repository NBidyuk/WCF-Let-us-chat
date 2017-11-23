using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Let_us_chat
{
    [Serializable]
    public class Message
    {
       
        private string nameReciever;
        private string nameSender;
        private string messageBody;

        public string NameSender
        {
            get { return nameSender; }

            set { nameSender = value; }
        }

        public string NameReciever
        {
            get { return nameReciever; }

            set { nameReciever = value; }
        }


        public string MessageBody
        {
            get { return messageBody; }

            set { messageBody = value; }
        }

        public Message()
        {
            nameReciever = "";
            nameSender = "";
            messageBody = "";

        }


        public Message(string reciever, string sender,
            string message)
        {
            nameReciever = reciever;
            nameSender = sender;
            messageBody = message;

        }


        public string Print()
        {
            return String.Format("{0}: {1}",nameSender,messageBody);

        }


        public string PrintForServer()
        {
            return String.Format("{0} for {1}: {2}", nameSender, nameReciever, messageBody);

        }




    }












}

