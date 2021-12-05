using System;
using System.Windows.Forms;

namespace APK
{
    public partial class AddGroup : Form
    {
        public AddGroup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Db db = new();
            if (!db.checkGroup(textBox1.Text))
            {
                db.CreateGroup(textBox1.Text);
                MessageBox.Show("Sekmingai");
            }
            else
            {
                MessageBox.Show("Toks grupes pavadinimas yra uzimtas");
            }
        }
    }
}
