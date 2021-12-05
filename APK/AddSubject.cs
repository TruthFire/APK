using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
            for (int i = 0; i < lect.Length; i++)
            {
                comboBox1.Items.Add(lect[i]);
            }
            if(lect.Length > 0)
            comboBox1.SelectedIndex = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value + numericUpDown2.Value + numericUpDown3.Value + numericUpDown4.Value == 100)
            {
                MarkCoefficients mc = new();
                mc.Coefficients = new int[] { Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numericUpDown3.Value), Convert.ToInt32(numericUpDown4.Value) };
                string json = JsonConvert.SerializeObject(mc);
                string[] groupList = textBox2.Text.Split(';');
                Db db = new();
                string[] allSubjects = db.getAllGroups();
                IEnumerable<string> except = groupList.Except(allSubjects);
                if (!except.Any())
                {
                    db.addSubject(comboBox1.Text, json, textBox1.Text, groupList);
                }
                else
                {
                    string msg = "Grupe(s):";
                    foreach (string g in except)
                    {
                        msg += g + "\n";
                    }
                    msg += "neegzistuoja";
                    MessageBox.Show(msg);
                }
            }
            else
            {
                MessageBox.Show("Koefficientu suma negali būti mažesnė arba didesnė nei 100");
            }
        }
    }

    public class MarkCoefficients
    {
        public int[] Coefficients = new int[4];
    }
}
