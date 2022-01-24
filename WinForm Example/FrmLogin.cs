using System;
using System.Windows.Forms;

namespace Auth.GG_Winform_Example
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void siticoneRoundedButton2_Click(object sender, EventArgs e)
        {
            Hide();
            FrmRegister f2 = new FrmRegister();
            f2.Show();
        }

        private void siticoneControlBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void siticoneRoundedButton1_Click(object sender, EventArgs e)
        {
            if (UserSystem.Login(username.Text, password.Text))
            {
                //Put code here of what you want to do after successful login
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                App.Log(User.Username, "Signed in");
                FrmMain main = new FrmMain();
                main.Show();
                Hide();
            }
        }
    }
}