using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APK
{
    public partial class MySubjects : Form
    {
        User curr;
        public MySubjects(User u)
        {
            curr = u;
            Db db = new();
            string[] s_titles;
            InitializeComponent();
            if (u.GetGroup() == 2)
            {
                s_titles = db.getMySubjectTitles(u.GetId());
                for (int i = 0; i < s_titles.Length; i++)
                {
                    comboBox1.Items.Add(s_titles[i]);
                }
            }
            else if (u.GetGroup() == 1)
            {
                s_titles = db.getStudentSubjects(u);
                for (int i = 0; i < s_titles.Length; i++)
                {
                    comboBox1.Items.Add(s_titles[i]);
                }

            }
            
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (curr.GetGroup() == 2)
            {
                SubjectGroups sg = new(curr, comboBox1.Text);
                sg.Show();
                this.Hide();
            }
            else if(curr.GetGroup() == 1)
            {
                UserMarks um = new(curr, comboBox1.Text);
                um.Show();
            }
        }

        private void MySubjects_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main m = new(curr);
            m.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (curr.GetGroup() == 2)
            {
                SubjectGroups sg = new(curr, comboBox1.Text);
                sg.Show();
                this.Hide();
            }
            else if (curr.GetGroup() == 1)
            {
                UserMarks um = new(curr, comboBox1.Text);
            }
        }
    }
}
