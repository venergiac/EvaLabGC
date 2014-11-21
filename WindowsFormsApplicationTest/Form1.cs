using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EVALab.Util.Graph;

namespace WindowsFormsApplicationTest
{
    public partial class Form1 : Form
    {

        Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            while (true)
            {
                this.chartControlForm1.AddValue((float)(rnd.NextDouble()*100.0));
            }
        }

        private void eListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
