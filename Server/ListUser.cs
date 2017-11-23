using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [Serializable]
    public class ListUser
    {
        public List<User> users { get; set; }

        public ListUser(List<User> u)
        {
            users = u;

        }

         public ListUser()
        {
            users =  new List<User>();

        }


    }
}
