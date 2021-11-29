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
    public partial class AddSubject : Form
    {
        public AddSubject()
        {
            InitializeComponent();
            Db db = new();
            string[] lect = db.getLectList();
            for(int i = 0; i < lect.Length; i++)
            {
                comboBox1.Items.Add(lect[i]);
            }

        }
    }
}
