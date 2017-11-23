using System.Drawing;
using System.Windows.Forms;

namespace Let_us_chat
{
    partial class ClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxContacts = new System.Windows.Forms.ListBox();
            this.labelContactName = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.listBoxMessages = new System.Windows.Forms.ListBox();
            this.LabelOnline = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.buttonFindFriend = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxContacts
            // 
            this.listBoxContacts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxContacts.FormattingEnabled = true;
            this.listBoxContacts.Location = new System.Drawing.Point(12, 41);
            this.listBoxContacts.Name = "listBoxContacts";
            this.listBoxContacts.Size = new System.Drawing.Size(257, 407);
            this.listBoxContacts.TabIndex = 2;
            this.listBoxContacts.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBoxContacts_DrawItem);
            this.listBoxContacts.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_DoubleClick);
            // 
            // labelContactName
            // 
            this.labelContactName.AutoSize = true;
            this.labelContactName.Location = new System.Drawing.Point(275, 28);
            this.labelContactName.Name = "labelContactName";
            this.labelContactName.Size = new System.Drawing.Size(0, 13);
            this.labelContactName.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Red;
            this.button2.Location = new System.Drawing.Point(12, 461);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 30);
            this.button2.TabIndex = 5;
            this.button2.Text = "Connect";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonSend);
            this.panel1.Controls.Add(this.textBoxMessage);
            this.panel1.Controls.Add(this.listBoxMessages);
            this.panel1.Controls.Add(this.LabelOnline);
            this.panel1.Controls.Add(this.labelName);
            this.panel1.Location = new System.Drawing.Point(281, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 407);
            this.panel1.TabIndex = 7;
            this.panel1.Visible = false;
            // 
            // buttonSend
            // 
            this.buttonSend.Enabled = false;
            this.buttonSend.Location = new System.Drawing.Point(213, 336);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(42, 79);
            this.buttonSend.TabIndex = 4;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Location = new System.Drawing.Point(3, 336);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(204, 79);
            this.textBoxMessage.TabIndex = 3;
            this.textBoxMessage.TextChanged += new System.EventHandler(this.textBoxMessage_TextChanged);
            // 
            // listBoxMessages
            // 
            this.listBoxMessages.FormattingEnabled = true;
            this.listBoxMessages.Location = new System.Drawing.Point(3, 79);
            this.listBoxMessages.Name = "listBoxMessages";
            this.listBoxMessages.Size = new System.Drawing.Size(252, 251);
            this.listBoxMessages.TabIndex = 2;
            // 
            // LabelOnline
            // 
            this.LabelOnline.AutoSize = true;
            this.LabelOnline.Location = new System.Drawing.Point(19, 42);
            this.LabelOnline.Name = "LabelOnline";
            this.LabelOnline.Size = new System.Drawing.Size(0, 13);
            this.LabelOnline.TabIndex = 1;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(19, 16);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(0, 13);
            this.labelName.TabIndex = 0;
            // 
            // buttonFindFriend
            // 
            this.buttonFindFriend.Location = new System.Drawing.Point(12, 12);
            this.buttonFindFriend.Name = "buttonFindFriend";
            this.buttonFindFriend.Size = new System.Drawing.Size(162, 23);
            this.buttonFindFriend.TabIndex = 8;
            this.buttonFindFriend.Text = "Find friends";
            this.buttonFindFriend.UseVisualStyleBackColor = true;
            this.buttonFindFriend.Click += new System.EventHandler(this.buttonFindFriend_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 500);
            this.Controls.Add(this.buttonFindFriend);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.labelContactName);
            this.Controls.Add(this.listBoxContacts);
            this.Name = "ClientForm";
            this.Text = "Let us chat";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox listBoxContacts;
        private System.Windows.Forms.Label labelContactName;
        private System.Windows.Forms.Button button2;
        private Panel panel1;
        private Button buttonSend;
        private TextBox textBoxMessage;
        private ListBox listBoxMessages;
        private Label LabelOnline;
        private Label labelName;
        private Button buttonFindFriend;
    }
}

