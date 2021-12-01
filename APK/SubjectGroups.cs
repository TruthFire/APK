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
    public partial class SubjectGroups : Form
    {
        User curr;
        int S_id;
        public SubjectGroups(User u, string subject)
        {
            curr = u;
            
            InitializeComponent();
            Db db = new();
            S_id = db.getSubjectId(subject, u.GetId());
            string[] groups = db.GetSubjectGroups(S_id);
            for (int i = 0; i < groups.Length; i++)
            {
                comboBox1.Items.Add(groups[i]);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SubjectStudentList ssl = new(curr, S_id, comboBox1.Text);
            ssl.Show();
        }

        private void SubjectGroups_FormClosed(object sender, FormClosedEventArgs e)
        {
            MySubjects ms = new(curr);
            ms.Show();
        }
    }
}
