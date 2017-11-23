using Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Let_us_chat
{
    public partial class NicknameForm : Form
    {

        
        public NicknameForm()
        {
            InitializeComponent();
            
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            ClientForm FParent = (ClientForm)this.Owner;
            if (String.IsNullOrEmpty(textBoxLogin.Text))
                MessageBox.Show("Enter your nickname, please");
            else


            {
               // FParent.LoggedUser = new User(textBoxLogin.Text, CalculateMD5Hash(textBoxPassword.Text),false);
                FParent.LoggedUser.NickName = textBoxLogin.Text;
                FParent.LoggedUser.Password = CalculateMD5Hash(textBoxPassword.Text);
                this.Close();
            }
        }

        public string CalculateMD5Hash(string input)

        {

            // step 1, calculate MD5 hash from input

            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)

            {

                sb.Append(hash[i].ToString("X2"));

            }

            return sb.ToString();

        }

    }
}
