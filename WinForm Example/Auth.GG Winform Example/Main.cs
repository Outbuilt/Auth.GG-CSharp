using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auth.GG_Winform_Example
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void siticoneRoundedButton1_Click(object sender, EventArgs e)
        {
        }

        private void siticoneControlBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void siticoneRoundedButton2_Click(object sender, EventArgs e)
        {
            string var = App.GrabVariable(variable.Text);
            MessageBox.Show(var);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            userid.Text = "ID: " + User.ID;
            username.Text = "Username: " + User.Username;
            email.Text = "Email: " + User.Email;
            hwid.Text = "HWID: " + User.HWID;
            uservariable.Text = "User Variable: " + User.UserVariable;
            userrank.Text = "Rank: " + User.Rank;
            ip.Text = "IP: " + User.IP;
            expiry.Text = "Expiry: " + User.Expiry;
            lastlogin.Text = "Last Login: " + User.LastLogin;
            registerdate.Text = "Register Date: " + User.RegisterDate;
        }
    }
}
