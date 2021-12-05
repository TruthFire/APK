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
    public partial class UserEditor : Form
    {
        User curr, student;
        int student_id;
        public UserEditor(User u, int Stud_id)
        {
            curr = u;
            Db db = new();
            student = db.GetUser(Stud_id);
            string[] groups = db.getAllGroups();
            InitializeComponent();
            for(int i = 0; i < groups.Length; i++)
            {
                comboBox1.Items.Add(groups[i]);
                if(groups[i] ==  student.GetInfo())
                {
                    comboBox1.SelectedIndex = i;
                }
            }
            textBox1.Text = student.GetName();
            textBox2.Text = student.GetSurename();
            textBox3.Text = student.GetNick();
            textBox4.Text = student.GetPwd();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Db db = new Db();
            Person newP = new(this.textBox1.Text, this.textBox2.Text);
            User newUser = new(newP, textBox3.Text, textBox4.Text, 1, comboBox1.Text, student_id);
            db.updateUser(newUser);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Db db = new();
           
            db.DeleteUser(student_id);
            MessageBox.Show("Sekmingai.");
            this.Close();
        }
    }
}
