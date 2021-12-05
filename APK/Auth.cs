using System;
using System.Windows.Forms;

namespace APK
{
    public partial class Auth : Form
    {
        public Auth()
        {
            InitializeComponent();
            //Db db = new();
            //MessageBox.Show(db.GetStudentMarks(1, 7)[0]);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Db database = new();
            if (database.TryAuth(textBox1.Text, textBox2.Text) != 0)
            {
                User u = database.GetUser(database.TryAuth(textBox1.Text, textBox2.Text));
                Main mForm = new(u);
                mForm.Show();
                this.Hide();

            }
            else
            {
                label3.Text = "Tokio vartotojo nera.";
            }

        }
    }
}
