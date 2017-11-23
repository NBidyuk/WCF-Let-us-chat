using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Let_us_chat;

namespace Server
{
    [Serializable]
    public class User
    {

        
        public string NickName { get; set;}
        public string Password { get; set; }
        
        public List<Message> messages;
        public Boolean online { get; set; }
        public ListUser friends;



        public User()
        {

            online = false;
            NickName = "";
            Password = "";
            friends = new ListUser();
            messages = new List<Message>();
        }

        public User(string name, string pass, bool online)
        {
            NickName= name;
            Password = pass;

            friends = new ListUser();
            messages = new List<Message>();
            this.online = online;
        }

    }
}
