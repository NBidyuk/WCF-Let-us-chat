namespace Server
{
    partial class ServerForm
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
            this.comboBoxIPList = new System.Windows.Forms.ComboBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.listBoxMessages = new System.Windows.Forms.ListBox();
            this.listBoxUsers = new System.Windows.Forms.ListBox();
            this.listBoxUsersOnline = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxIPList
            // 
            this.comboBoxIPList.FormattingEnabled = true;
            this.comboBoxIPList.Location = new System.Drawing.Point(21, 28);
            this.comboBoxIPList.Name = "comboBoxIPList";
            this.comboBoxIPList.Size = new System.Drawing.Size(242, 21);
            this.comboBoxIPList.TabIndex = 0;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(306, 28);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(177, 20);
            this.textBoxPort.TabIndex = 1;
            // 
            // listBoxMessages
            // 
            this.listBoxMessages.FormattingEnabled = true;
            this.listBoxMessages.Location = new System.Drawing.Point(21, 76);
            this.listBoxMessages.Name = "listBoxMessages";
            this.listBoxMessages.Size = new System.Drawing.Size(153, 342);
            this.listBoxMessages.TabIndex = 2;
            // 
            // listBoxUsers
            // 
            this.listBoxUsers.FormattingEnabled = true;
            this.listBoxUsers.Location = new System.Drawing.Point(191, 76);
            this.listBoxUsers.Name = "listBoxUsers";
            this.listBoxUsers.Size = new System.Drawing.Size(153, 342);
            this.listBoxUsers.TabIndex = 3;
            // 
            // listBoxUsersOnline
            // 
            this.listBoxUsersOnline.FormattingEnabled = true;
            this.listBoxUsersOnline.Location = new System.Drawing.Point(359, 76);
            this.listBoxUsersOnline.Name = "listBoxUsersOnline";
            this.listBoxUsersOnline.Size = new System.Drawing.Size(153, 342);
            this.listBoxUsersOnline.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Messages";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Users";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(404, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Users online";
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 435);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxUsersOnline);
            this.Controls.Add(this.listBoxUsers);
            this.Controls.Add(this.listBoxMessages);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.comboBoxIPList);
            this.Name = "ServerForm";
            this.Text = "Server";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxIPList;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.ListBox listBoxMessages;
        private System.Windows.Forms.ListBox listBoxUsers;
        private System.Windows.Forms.ListBox listBoxUsersOnline;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

