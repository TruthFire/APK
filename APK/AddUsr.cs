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
            Db db = new Db();
            string[] groups;
            groups = db.getStudGroupList();
            for(int i = 0; i < groups.Length;i++)
            {
                comboBox1.Items.Add(groups[i]);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
