using System;
using System.Windows.Forms;

namespace APK
{
    public partial class SubjectStudentList : Form
    {
        User curr;
        string student_group;
        int sub_id;
        public SubjectStudentList(User u, int subject_id, string group)
        {
            InitializeComponent();
            Db db = new();
            string[] names = db.GetGroupStudents(group);
            for (int i = 0; i < names.Length; i++)
            {
                comboBox1.Items.Add(names[i]);
            }
            comboBox1.SelectedIndex = 0;
            curr = u;
            student_group = group;
            sub_id = subject_id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Db db = new();
            UserMarks um = new(comboBox1.Text, student_group, sub_id, curr);
            um.Show();
        }
    }
}
