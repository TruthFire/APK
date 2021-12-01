using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

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
           // double abc = coefficietns[0] / 100;
            //MessageBox.Show(abc.ToString());
            label1.Text = stud_name;
            numericUpDown1.Value = marks[0];
            numericUpDown2.Value = marks[1];
            numericUpDown3.Value = marks[2];
            numericUpDown4.Value = marks[3];
            numericUpDown5.Value = (decimal)val;
            
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
