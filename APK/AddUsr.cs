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
    public partial class AddUsr : Form
    {
        public AddUsr()
        {
            InitializeComponent();
            Db db = new();
            string[] groups;
            groups = db.getStudGroupList();
            for(int i = 0; i < groups.Length;i++)
            {
                comboBox1.Items.Add(groups[i]);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Db db = new Db();
            Person newP = new(this.textBox1.Text, this.textBox2.Text);
            User newUser = new(newP, textBox3.Text, textBox4.Text, 1, comboBox1.Text);
            db.CreateUser(newUser);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Db db = new Db();
            Person newP = new(this.textBox8.Text, this.textBox7.Text);
            User newUser = new(newP, textBox6.Text, textBox5.Text, 2, comboBox2.Text);
            db.CreateUser(newUser);
        }
    }
}
