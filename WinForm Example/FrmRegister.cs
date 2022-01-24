using System;
using System.Windows.Forms;

namespace Auth.GG_Winform_Example
{
    public partial class FrmRegister : Form
    {
        public FrmRegister()
        {
            InitializeComponent();
        }

        private void siticoneRoundedButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmLogin f1 = new FrmLogin();
            f1.Show();
        }

        private void siticoneControlBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void siticoneRoundedButton2_Click(object sender, EventArgs e)
        {
            if (UserSystem.Register(username.Text, password.Text, email.Text, license.Text))
            {
                //Put code here of what you want to do after successful login
                MessageBox.Show("Register has been successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}