using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace EVALab.Util.License
{
    public partial class LicenseForm : Form
    {
        public LicenseForm()
        {
            InitializeComponent();
        }

        public static bool Show(IWin32Window owner, string message, Exception e)
        {
            LicenseForm form = new LicenseForm();
            form.SetError(message, e);
            DialogResult result = form.ShowDialog(owner);
            return result == DialogResult.OK;
        }

        public void SetError(string message, Exception e)
        {
            this.richTextBox1.Text = message + "\n" + e.StackTrace;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            RegistryKey licenseKey = Registry.CurrentUser.CreateSubKey("Software\\EvaLab\\Key");
            licenseKey.SetValue("0017106c-e5f2-4401-8e7a-df3a2715fbee", this.textBox1.Text);

            this.richTextBox1.Text = "Updated please restart";
            this.buttonExit.Text = "Restart";
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
