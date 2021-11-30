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
           // UpdateDg();
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

        /*public void UpdateDg()
        {
            Db db = new();
            BindingSource bs = new();
            bs.DataSource = db.FillGridView();
            dataGridView1.DataSource = bs;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox1.Text) != curr.GetId())
            {
                try
                {
                    if (curr.IsAdmin())
                    {
                        Db db = new();
                        db.DeleteUser(Convert.ToInt32(this.textBox1.Text));
                        UpdateDg();
                        MessageBox.Show("Sekmingai");
                    }
                    else
                    {
                        throw new ArgumentException("Jūs neturite prieigos vykdyti šią komandą.");
                    }

                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Jus negalite pasalinti savo profili.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }*/
    }
}
