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
            
            InitializeComponent();
            Db db = new();
            string[] s_titles = db.getMySubjectTitles(u.GetId());
            for (int i = 0; i < s_titles.Length; i++)
            {
                comboBox1.Items.Add(s_titles[i]);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SubjectGroups sg = new(curr, comboBox1.Text);
            sg.Show();
            this.Hide();
        }

        private void MySubjects_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main m = new(curr);
            m.Show();
        }
    }
}
