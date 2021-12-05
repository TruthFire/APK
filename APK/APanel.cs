using System;
using System.Windows.Forms;


namespace APK
{
    public partial class APanel : Form
    {
        User curr;
        public APanel(User a)
        {
            curr = a;
            InitializeComponent();
        }

        private void APanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main m = new(curr);
            m.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserMgr umgr = new(curr);
            umgr.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SGroupMgr sgm = new(curr);
            sgm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddSubject a_s = new();
            a_s.Show();
        }

        private void APanel_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            Main m = new(curr);
            m.Show();
            this.Hide();

        }

    }
}
