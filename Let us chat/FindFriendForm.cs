using Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Let_us_chat
{
    public partial class FindFriendForm : Form
    {
        
       // private ClientForm cf;
        private ListUser users;
      

        public FindFriendForm()
        {
            InitializeComponent();
           
            //cf = (ClientForm)this.Owner;
           

         
           
        }

        private void Form_Load(object sender, EventArgs e)
        {
            ClientForm cf = (ClientForm)this.Owner;
            Message message = new Message(cf.LoggedUser.NickName, "", "");
            message.MessageBody = "<users>";
            byte[] msg = ClientForm.MessageSerialization(message);
            cf.ClientSocket.Send(msg);

            var buffer = new byte[1024];
            int bytesReceived = cf.ClientSocket.Receive(buffer);
            users = new ListUser();
            users = ClientForm.DeSerialization(buffer);

            listBoxUsers.DataSource = users;
            listBoxUsers.DisplayMember = "NickName";
            listBoxUsers.ValueMember = "online";
            listBoxUsers.Refresh();
        }

        private void listBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClientForm cf = (ClientForm)this.Owner;
            if (listBoxUsers.SelectedItems.Count > 0)
            {
                User penFriend = (User)listBoxUsers.SelectedItem;
                cf.LoggedUser.friends.users.Add(penFriend);
                this.Close();
            }

            else
                MessageBox.Show("Choose a user first.");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void listBox_DoubleClick(object sender, MouseEventArgs e)
        {
            ClientForm cf = (ClientForm)this.Owner;
            int index = listBoxUsers.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {

                User penFriend = (User)listBoxUsers.SelectedItem;
                cf.LoggedUser.friends.users.Add(penFriend);
                this.Close();
            }
            
        }
    }
}
