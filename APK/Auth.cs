﻿using System;
using System.Windows.Forms;

namespace APK
{
    public partial class Auth : Form
    {
        public Auth()
        {
            InitializeComponent();
            //Db db = new();
            //MessageBox.Show(db.GetAvatar(1));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Db database = new Db();
            if (database.TryAuth(textBox1.Text, textBox2.Text) != 0)
            {
                User u = database.GetUser(textBox1.Text, textBox2.Text);
                Main mForm = new(u);
                mForm.Show();
                this.Hide();

            }
            else
            {
                label3.Text = "Tokio vartotojo nera.";
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register reg = new();
            reg.Show();
            Hide();
        }
    }
}