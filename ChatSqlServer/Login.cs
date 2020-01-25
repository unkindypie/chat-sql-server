using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatSqlServer
{
    public partial class Login : Form
    {
        public User user;

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 3)
            {
                if (Model.Self.Connect())
                {
                    user = Model.Self.login(textBox1.Text);

                    var sysUser = Model.Self.login("System");
                    Model.Self.sendMessage(new Message(textBox1.Text + " joined to our chat!", sysUser, null));

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("No connection to server!");
                   
                    Close();
                }
            } else
            {
                MessageBox.Show("Username is too short!");
            }
       
        }
    }
}
