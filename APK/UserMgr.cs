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
    public partial class UserMgr : Form
    {
        User Curr;
        public UserMgr(User u)
        {
            InitializeComponent();
            UpdateDg();
            Curr = u;
        }

        public void UpdateDg()
        {
            Db db = new();
            BindingSource bs = new();
            bs.DataSource = db.FillUserGridView();
            dataGridView1.DataSource = bs;
            dataGridView1.Columns[1].HeaderText = "Slapyvardis";
            dataGridView1.Columns[2].HeaderText = "Vardas";
            dataGridView1.Columns[3].HeaderText = "Pavarde";
        }

        private void UserMgr_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main m = new(Curr);
            m.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddUsr au = new();
            au.Show(); //
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Db db = new();
            if (db.CheckId(Convert.ToInt32(textBox1.Text)))
            {
                UserEditor ue = new(Curr, Convert.ToInt32(textBox1.Text));
                ue.Show();
            }
            else
            {
                MessageBox.Show("Tokio ID Nera");
            }
        }
    }
}
