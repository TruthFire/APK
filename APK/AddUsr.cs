using System;
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
            for (int i = 0; i < groups.Length; i++)
            {
                comboBox1.Items.Add(groups[i]);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Db db = new Db();
            Person newP = new(this.textBox1.Text, this.textBox2.Text);
            if (!String.IsNullOrEmpty(textBox3.Text) && !String.IsNullOrEmpty(textBox4.Text))
            {
                User newUser = new(newP, textBox3.Text, textBox4.Text, 1, comboBox1.Text);
                db.CreateUser(newUser);
            }
            else
            {
                User newUser = new(newP, textBox1.Text, textBox2.Text, 1, comboBox1.Text);
                db.CreateUser(newUser);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Db db = new Db();
            Person newP = new(this.textBox8.Text, this.textBox7.Text);
            User newUser = new(newP, textBox6.Text, textBox5.Text, 2, comboBox2.Text);
            db.CreateUser(newUser);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Db db = new Db();
            Person newP = new(this.textBox12.Text, this.textBox11.Text);
            User newUser = new(newP, textBox10.Text, textBox9.Text, 3);
            db.CreateUser(newUser);
        }
    }
}
