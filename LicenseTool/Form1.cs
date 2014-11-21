using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LicenseTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.textBox2.Text =  ""+EVALab.Util.License.FakeApp.Convert(dateTimePicker1.Value);

            long k1 = Int64.Parse(this.textBox2.Text);
            long k2 = EVALab.Util.License.FakeApp.Convert(k1);

            this.dateTimePicker2.Value = new DateTime(k2);
        }
    }
}
