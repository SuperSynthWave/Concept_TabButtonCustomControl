using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testingApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabButton1.tabIcon = Image.FromFile(Application.StartupPath + @"\testIcon.png");
        }

        private void tabButton1_tabClicked(TabButtonControl.TabButton sender)
        {
            MessageBox.Show(((TabButtonControl.TabButton)sender).caption);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabButton1.Active = !tabButton1.Active;
        }
    }
}
