using Newtonsoft.Json;
using System;
using System.Windows.Forms;

namespace APK
{
    public partial class UserMarks : Form
    {
        string name;
        int studId, subj_id;

        public UserMarks(string stud_name, string stud_group, int subject_id, User curr)
        {

            Db db = new();
            string[] tmp = stud_name.Split(' ');
            studId = db.GetUserId(tmp[0], tmp[1], stud_group);
            subj_id = subject_id;
            int[] marks = db.GetStudentMarks(studId, subject_id);
            int[] coefficietns = db.GetCoefficients(subject_id);
            double val = 0.0;
            for (int i = 0; i < 4; i++)
            {
                val += (double)marks[i] * ((double)coefficietns[i] / (double)100);
            }
            InitializeComponent();
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;

            label1.Text = stud_name;
            numericUpDown1.Value = marks[0];
            numericUpDown2.Value = marks[1];
            numericUpDown3.Value = marks[2];
            numericUpDown4.Value = marks[3];
            numericUpDown5.Value = (decimal)val;

        }

        public UserMarks(User u, string subject)
        {
            Db db = new();
            int groupId = db.GetGroupId(u.GetInfo());
            int subject_id = db.getSubjectIdByGroup(groupId, subject);
            int[] marks = db.GetStudentMarks(u.GetId(), subject_id);
            int[] coefficietns = db.GetCoefficients(subject_id);
            double val = 0.0;
            for (int i = 0; i < 4; i++)
            {
                val += (double)marks[i] * ((double)coefficietns[i] / (double)100);
            }


            InitializeComponent();
            button1.Visible = false;
            numericUpDown1.Visible = false;
            numericUpDown2.Visible = false;
            numericUpDown3.Visible = false;
            numericUpDown4.Visible = false;
            numericUpDown5.Visible = false;
            textBox1.Text = marks[0].ToString();
            textBox2.Text = marks[1].ToString();
            textBox3.Text = marks[2].ToString();
            textBox4.Text = marks[3].ToString();
            textBox5.Text = val.ToString();
            label1.Text = subject;
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Db db = new();
            Marks m = new();
            m.stud_marks = new int[4] { Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numericUpDown3.Value), Convert.ToInt32(numericUpDown4.Value) };
            string json = JsonConvert.SerializeObject(m);
            db.UpdateMarks(studId, subj_id, json);
            MessageBox.Show("Sekmingai!");
        }
    }

}
