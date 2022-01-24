﻿using System;
using System.IO;
using System.Windows.Forms;

namespace Auth.GG_Winform_Example
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
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
            siticoneCirclePictureBox1.Load(User.ProfilePicture);
            welcome.Text = "Welcome back, " + User.Username + "!";
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

        private void siticoneRoundedButton1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.png; *.jpeg; *.gif; *.bmp)|*.jpg; *.png; *.jpeg; *.gif; *.bmp)";
            if (open.ShowDialog() == DialogResult.OK)
            {
                string profilepic = open.FileName;
                string pic = Convert.ToBase64String(File.ReadAllBytes(profilepic));
                UserSystem.UploadPic(User.Username, pic);
            }
        }
    }
}