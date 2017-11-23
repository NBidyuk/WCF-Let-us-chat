using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Server;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Let_us_chat
{
    public partial class ClientForm : Form
    {
        private readonly SynchronizationContext synchronizationContext = null;
        private User loggedUser;
        private Message message;
        private User penFriend;//user to send message to
        public static NicknameForm nf; 
        public static FindFriendForm newForm;
        public User LoggedUser
        {
            get { return loggedUser; }
            set { loggedUser = value; }
        }

        public User PenFriend
        {
            get { return penFriend; }
            set { penFriend = value; }
        }
        public Message Message
        {
            get { return message; }
            set { message = value; }
        }

        public ClientForm()
        {
            
            InitializeComponent();
            synchronizationContext = SynchronizationContext.Current;
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
            loggedUser = new User();
            nf = new NicknameForm();
            nf.Owner = this;
            newForm = new FindFriendForm();
            newForm.Owner = this;
            
        }
        private SynchronizationContext SynchronizationContext
        {
            get { return synchronizationContext; }
        }
        public Socket ClientSocket { get; set; }
        /* private bool IsIpAddress
         {
             get { return Regex.IsMatch(ServerName, @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$"); }
         }*/

        private string ServerName { get; set; }

        private Boolean Connected { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            ServerName = Dns.GetHostName().ToString();
            
          
            VisiblePanelContents(panel1, false);

        }

        private void button2_Click(object sender, EventArgs e)
        {    
            Connect();
        }

        private async void Connect()
        {
            await Task.Run(() =>
            {
                if (!Connected)
                {
                    IPHostEntry ipHostEntry = Dns.GetHostEntry(ServerName);
                    IPEndPoint ipEndPoint = new IPEndPoint(ipHostEntry.AddressList.First(ipAddress => ipAddress.AddressFamily == AddressFamily.InterNetwork), 11000);
               

                    try
                    {
                        ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        ClientSocket.Connect(ipEndPoint);
                        Connected = true;
                        SynchronizationContext.Send(state => button2.Text = "Disconnect", null);
                        SynchronizationContext.Send(state => button2.BackColor = Color.Green, null);

                        string Err="-1";
                        var buffer = new byte[1024];
                        int bytesReceived;

                        do //the user is logging
                        { if (String.IsNullOrEmpty(loggedUser.NickName)|| Err=="1")
                            {


                                SynchronizationContext.Send(state => nf.ShowDialog(), null); 
                                

                            }

                        
                            byte[] messageBytes = Encoding.Default.GetBytes(loggedUser.NickName);
                            ClientSocket.Send(messageBytes);
                            messageBytes = Encoding.Default.GetBytes(loggedUser.Password);
                            ClientSocket.Send(messageBytes);
                            
                            bytesReceived = ClientSocket.Receive(buffer);
                            //Err = BitConverter.ToInt32(bytesReceived, 0);
                            Err = Encoding.Default.GetString(buffer, 0, bytesReceived);
                            
                            if (Err == "1")
                            {
                                SynchronizationContext.Send(state => MessageBox.Show("The password is wrong"), null);

                            }
                        } while (Err == "1");


                       
                        //bytesReceived = ClientSocket.Receive(buffer);
                        SynchronizationContext.Send(state => MessageBox.Show("The user is connected to server"), null);
                            loggedUser.online = true;

                            bytesReceived = ClientSocket.Receive(buffer);
                            loggedUser.friends = DeSerialization(buffer);
                            SynchronizationContext.Send(state => listBoxContacts.DataSource = loggedUser.friends, null);
                            SynchronizationContext.Send(state => listBoxContacts.DisplayMember = "NickName", null);
                            SynchronizationContext.Send(state => listBoxContacts.ValueMember = "online", null);
                            SynchronizationContext.Send(state => listBoxContacts.Refresh(), null);
                                                        
                            
                            
                            message = new Message();
                            penFriend = new User();
                            /*foreach (User u in loggedUser.friends)
                            {
                                listBoxContacts.Items.Add(u.NickName);
                                if (u.online)
                            }  */

                           while(true)
                            {
                                bytesReceived = ClientSocket.Receive(buffer);
                                message = MessageDeSerialization(buffer);
                                
                                foreach(User u in loggedUser.friends.users)
                                {
                                    if (u.NickName == message.NameSender)
                                    {
                                        u.messages.Add(message);
                                    }
                                }
                            }
                        }
                    
                    catch (SocketException exception)
                    {

                        if (exception.ErrorCode == 10061)
                        {
                            MessageBox.Show("The connection port is closed");
                            // Application.Exit();
                        }
                        else
                        {
                            MessageBox.Show(string.Format("Client: {0}", exception.Message));
                        }
                    }
                }
                else if (Connected)
                {
                    byte[] messageBytes = Encoding.Default.GetBytes("<end>");
                    ClientSocket.Send(messageBytes);
                    ClientSocket.Shutdown(SocketShutdown.Both);
                    ClientSocket.Close();
                    Connected = false;
                    button2.Text = "Disconnect";
                    button2.BackColor = Color.DarkRed;

                }

            });
        }

        

        public static byte[] Serialization(ListUser obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);
                byte[] msg = stream.ToArray();
                return msg;
            }

        }

        public static ListUser DeSerialization(byte[] serializedAsBytes)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(serializedAsBytes, 0, serializedAsBytes.Length);
                stream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(stream) as ListUser;
            }
        }

        public static byte[] MessageSerialization(Message obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);
                byte[] msg = stream.ToArray();
                return msg;
            }

        }

        public static Message MessageDeSerialization(byte[] serializedAsBytes)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(serializedAsBytes, 0, serializedAsBytes.Length);
                stream.Seek(0, SeekOrigin.Begin);
                return (Message)formatter.Deserialize(stream);
            }
        }

        private void listBoxContacts_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;
            User item = (User)this.listBoxContacts.Items[e.Index];
            e.DrawBackground();
            if (item.online == true)
            {
                e.Graphics.DrawString(item.NickName, listBoxContacts.Font, Brushes.Green, e.Bounds, StringFormat.GenericDefault);
            }
            else
            {
                e.Graphics.DrawString(item.NickName, listBoxContacts.Font, Brushes.DarkGray, e.Bounds, StringFormat.GenericDefault);
            }
        }


        private void listBox1_DoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBoxContacts.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                panel1.Enabled = true;
                VisiblePanelContents(panel1, true);


                penFriend = (User)listBoxContacts.SelectedItem;
                labelContactName.Text = penFriend.NickName;
                if (penFriend.online == true)
                    LabelOnline.Text = "online";
                else
                    LabelOnline.Text = "offline";
                foreach (Message mes in penFriend.messages)
                {
                    listBoxMessages.Items.Add(mes.Print());
                    listBoxMessages.Refresh();

                }

            }
        }

        private void textBoxMessage_TextChanged(object sender, EventArgs e)
        {
            buttonSend.Enabled = textBoxMessage.Text.Length > 0;
        }


        private void VisiblePanelContents(Panel panel, bool visible)
        {
            foreach (Control item in panel.Controls)
            {
                item.Visible = visible;
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (buttonSend.Enabled == true)
            {

                Send();

            }
        }



        private async void Send()
        {
            await Task.Run(() =>
            {
                try
                {
                    string mes = textBoxMessage.Text;
                    message.NameReciever = LoggedUser.NickName;
                    message.NameSender = penFriend.NickName;
                    message.MessageBody = mes;
                    byte[] msg = MessageSerialization(message); // конвертируем строку, содержащую сообщение, в массив байтов
                    int bytesSend = ClientSocket.Send(msg); // отправляем серверу сообщение через сокет
                    SynchronizationContext.Send(d => PenFriend.messages.Add(message), null);
                    SynchronizationContext.Send(d => listBoxMessages.Items.Add(message.Print()), null);
                    textBoxMessage.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Client: {0}", ex.Message));
                }
            });
        }


        private void OnApplicationExit(object sender, EventArgs e)
        {
            if (ClientSocket != null)
            {
                Message mes = new Message(loggedUser.NickName, "", "<end>");
                byte[] msg = MessageSerialization(mes);
                ClientSocket.Send(msg);
            }
        }

        private async void buttonFindFriend_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            { 
                
                SynchronizationContext.Send(d => newForm.ShowDialog(), null);  });
            
            this.listBoxContacts.Refresh();
        }
    }
}




   

