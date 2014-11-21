using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EVALab.Util.Box
{
    public partial class ExceptionForm : Form
    {
        public ExceptionForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetError(string message, Exception e)
        {
            this.label1.Text = message;
            this.textBox1.Text = e.Message;

            this.textBox1.Text = e.ToString();
        }

        public static bool Show(IWin32Window owner, string message, Exception e)
        {
            ExceptionForm form = new ExceptionForm();
            form.SetError(message, e);
            DialogResult result = form.ShowDialog(owner);
            return result == DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
