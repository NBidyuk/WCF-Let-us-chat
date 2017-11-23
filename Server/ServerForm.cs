using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Let_us_chat;
using System.Xml.Serialization;

namespace Server
{
    public partial class ServerForm : System.Windows.Forms.Form
    {
        private readonly SynchronizationContext synchronizationContext = null;

        private ListUser users; // all users
        private Hashtable usersOnline;// запоминиет login и socket users online;
        
        public ServerForm()
        {
            InitializeComponent();
            
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
        }
    

        private SynchronizationContext SynchronizationContext
        {
            get { return synchronizationContext; }
        }

        private int Port
        {
            get
            {
                int port = int.Parse(this.textBoxPort.Text);
                return port;
            }

            set
            {
                this.textBoxPort.Text = value.ToString();
            }


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            IPHostEntry IPEntry = Dns.GetHostEntry(Dns.GetHostName());
            usersOnline = new Hashtable();
            foreach (IPAddress IPAdd in IPEntry.AddressList)
            {

                if (IPAdd.AddressFamily == AddressFamily.InterNetwork)
                {
                    this.comboBoxIPList.Items.Add(IPAdd);

                }
            }
            comboBoxIPList.SelectedIndex = 0;

            Port = 11000;

            Accept();

            AddMessage("Server: Waiting for clients to connect.");
            users = new ListUser();
            XMLDeSerialize(users);

            listBoxUsers.DataSource = users.users;
            listBoxUsers.DisplayMember = "NickName";
            listBoxUsers.ValueMember = "online";
            listBoxUsers.Refresh();
        }


        private async void Accept()
        {
            await Task.Run(() =>

            {

                Socket ListeningSocket;
                try
                {
                    IPAddress IP = IPAddress.Any;
                    IPEndPoint IPEndPoint = new IPEndPoint(IP, Port);
                    
                    try
                    {
                        ListeningSocket = new Socket(IP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                        ListeningSocket.Bind(IPEndPoint);
                        ListeningSocket.Listen(20);

                        while (true)
                        {
                            Socket serverSocket = ListeningSocket.Accept();
                            Receive(serverSocket);
                        }
                    }
                    catch (SocketException exception)
                    {
                        if (exception.ErrorCode == 10061)
                            MessageBox.Show("the connection port is closed");
                        Application.Exit();
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(string.Format("Server: {0}", exception.Message));
                }

            });
        }

      

        private void AddMessage(String message)
        {
            listBoxMessages.Items.Add(message);
        }


        private async void Receive(Socket serverSocket)
        {
            await Task.Run(() =>
            {
                try
                {

                    var buffer = new byte[1024];
                    int bytesReceived;
                    int Err;
                    string clientName;
                    string clientPassword;
                    do {
                        bytesReceived = serverSocket.Receive(buffer);
                        clientName = Encoding.Default.GetString(buffer, 0, bytesReceived);
                        bytesReceived = serverSocket.Receive(buffer);
                        clientPassword = Encoding.Default.GetString(buffer, 0, bytesReceived);
                        byte[] messageBytes;
                        Err = CheckUser(clientName, clientPassword);

                        if (Err == 1)
                        {
                            messageBytes = Encoding.Default.GetBytes("1");//the pass is wrong
                            serverSocket.Send(messageBytes);
                        }
                    } while (Err == 1);

                    if (Err == 0)
                    {
                        ListUser temp = new ListUser();
                        foreach (User u in users.users)
                        {
                            if (u.NickName == clientName)
                            {
                                temp = u.friends;
                                u.online = true;
                            }
                        }
                        usersOnline.Add(clientName, serverSocket);
                        listBoxUsersOnline.Items.Add(clientName);

                        byte[] list = Serialization(temp);
                        serverSocket.Send(list);

                    }


                    else if (Err == 2)
                    {
                        users.users.Add(new User(clientName, clientPassword, true));
                        usersOnline.Add(clientName, serverSocket);
                        //messageBytes = Encoding.Default.GetBytes("2");//no user
                        //serverSocket.Send(messageBytes);
                    }

                    AddMessage(string.Format("Server: {0} connected to server.", clientName));
                    Let_us_chat.Message mes = new Let_us_chat.Message();

                    while (true)
                    {
                        bytesReceived = serverSocket.Receive(buffer);

                        mes = MessageDeSerialization(buffer);
                        if (mes.MessageBody == "<users>")// the user asks to send all the contacts
                        {

                            byte[] list = Serialization(users);
                            serverSocket.Send(list);

                        }
                        if (mes.MessageBody == "<end>")// the user sent a message that he is disconnected
                        {
                            foreach (User u in users.users)
                            {
                                if (u.NickName == clientName)
                                {

                                    u.online = false;
                                }
                            }
                            int index = listBoxUsersOnline.FindString(clientName);
                            listBoxUsersOnline.Items.RemoveAt(index);
                            listBoxUsersOnline.Refresh();
                            AddMessage(string.Format("Server: Client {0} disconnected from server.", clientName));

                            serverSocket.Shutdown(SocketShutdown.Both);
                            serverSocket.Close();
                            break;
                        }

                        if (usersOnline.Contains(mes.NameReciever))
                        {

                            Send((Socket)usersOnline[mes.NameReciever], mes);
                            SynchronizationContext.Send(state => AddMessage(string.Format("{0}: {1}", clientName, mes.Print())), null);
                            SynchronizationContext.Send(state => AddMessageToUSer(mes.NameSender, mes), null);
                            SynchronizationContext.Send(state => AddMessageToUSer(mes.NameReciever, mes), null);
                            // messageBytes = Encoding.Default.GetBytes("<OK>");
                            //serverSocket.Send(messageBytes);
                        }
                        /*else
                        {
                            messageBytes = Encoding.Default.GetBytes("<offline>");
                            serverSocket.Send(messageBytes);

                        }*/

                        //String message = Encoding.Default.GetString(buffer, 0, bytesReceived);


                    }


                
                
                }
                catch (Exception exception)
                {
                    MessageBox.Show(string.Format("Server: {0}", exception.Message));
                }
                
            });
        }

        private async void Send(Socket serverSocket, Let_us_chat.Message mes)
        {
            await Task.Run(() =>
            {
            try
            {
                   
                    byte[] messageBytes = MessageSerialization(mes) ;
                    serverSocket.Send(messageBytes);
                    


                }
                catch (Exception exception)
                {
                    MessageBox.Show(string.Format("Server: {0}", exception.Message));
                }

            });
        }

        // checks if the login and password are right. out 0 - right, 1-wrong password but user exists, 2 - no user
        private int CheckUser(string nickName, string Password)
        {
            foreach (User u in users.users)
            {
                if (u.NickName == nickName)
                {
                    if (u.Password == Password)
                    {
                        
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            
            return 2;

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
                return (ListUser)formatter.Deserialize(stream);
            }
        }


        public static byte[] MessageSerialization(Let_us_chat.Message obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);
                byte[] msg = stream.ToArray();
                return msg;
            }

        }

        public static Let_us_chat.Message MessageDeSerialization(byte[] serializedAsBytes)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(serializedAsBytes, 0, serializedAsBytes.Length);
                stream.Seek(0, SeekOrigin.Begin);
                return (Let_us_chat.Message)formatter.Deserialize(stream);
            }
        }

        private void XMLSerialize(ListUser uList)
        {
            string pathFile = Application.StartupPath + "\\users.xml";
            if (System.IO.File.Exists(pathFile))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(ListUser));
                using (FileStream fs = new FileStream("users.xml", FileMode.OpenOrCreate))
                {

                    formatter.Serialize(fs, uList);


                }
            }
            else return;

        }

        private void XMLDeSerialize(ListUser uList)
        {

            XmlSerializer formatter = new XmlSerializer(typeof(ListUser));
            using (FileStream fs = new FileStream("users.xml", FileMode.OpenOrCreate))
            {

                ListUser users = (ListUser)formatter.Deserialize(fs);
                uList.users.Clear();
                uList.users.AddRange(users.users);

            }

        }

        private void AddMessageToUSer(String userName, Let_us_chat.Message mes)
        {
            foreach (User user in users.users)
            {
                if (user.NickName ==userName)
                {
                    user.messages.Add(mes); 
                }
            }
        }



        private void OnApplicationExit(object sender, EventArgs e)
        {
            foreach (User u in users.users)
            {
                
                    u.online = false;
               
            }


            XMLSerialize(users);
        }

    }


}




    
