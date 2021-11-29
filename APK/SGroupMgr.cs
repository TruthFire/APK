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
    public partial class SGroupMgr : Form
    {
        User curr;
        public SGroupMgr(User u)
        {
            InitializeComponent();
            UpdateDg();
            curr = u;
        }

        public void UpdateDg()
        {
            Db db = new();
            BindingSource bs = new();
            bs.DataSource = db.FillSGroupGridView();
            dataGridView1.DataSource = bs;
            dataGridView1.Columns[1].HeaderText = "Pavadinimas";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddGroup ag = new();
            ag.Show();
        }
    }
}
