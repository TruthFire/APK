﻿using System;
using System.Windows.Forms;

namespace APK
{
    public partial class Register : Form
    {

        public Register()
        {
            InitializeComponent();
        }


        private string IsNullOrSpace(string s)
        {
            if (s == null || s.Trim() == string.Empty)
            {
                throw new ArgumentException("Ivedimas ne gali buti tuscias arba lygus null");
            }
            return s;

        }

        private DateTime IsDateValid(DateTime d)
        {
            DateTime curr = DateTime.Today;
            if (d.Day >= curr.Day && d.Month >= curr.Month && d.Year > curr.Year)
            {
                throw new ArgumentException("Gimimo data negali buti didesni, nei esama");
            }
            return d;
        }

        private bool DoPasswordsMatch(string p1, string p2)
        {
            return p1 == p2;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                ErrLabel.Text = "";
                if (DoPasswordsMatch(Pw1_Input.Text, Pw2_Input.Text))
                {
                    Person p;
                    string name = IsNullOrSpace(Name_Input.Text);
                    string surename = IsNullOrSpace(Surename_Input.Text);
                    DateTime dob = IsDateValid(DateTime.Parse(Dob_Input.Text));
                    p = new Person(name, surename);

                    User NewUser = new(p, Nickname_Input.Text, Pw1_Input.Text, 1);
                    Db Database = new();
                    if (!Database.CheckNick(NewUser.GetNick()))
                    {
                        Database.CreateUser(NewUser);
                        Main mForm = new(NewUser);
                        mForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        ErrLabel.Text = "Šis slapyvardis jau užimtas";


                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Register_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}