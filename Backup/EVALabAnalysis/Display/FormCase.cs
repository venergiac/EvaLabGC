using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EVALabAnalysis.CaseBase;

namespace EVALabAnalysis.Display
{
    public partial class FormCase : Form
    {
        public FormCase()
        {
            InitializeComponent();
        }

        private void FormCase_Load(object sender, EventArgs e)
        {

        }

        public void SetCase(Case thiscase)
        {
            this.textBox1.Text = thiscase.Name;
            this.richTextBox1.Text = thiscase.Description;
            this.richTextBox2.Text = thiscase.Diagnosis;
        }

        public static bool Show(IWin32Window owner, Case thiscase)
        {
            FormCase form = new FormCase();
            form.SetCase(thiscase);
            DialogResult result = form.ShowDialog(owner);
            return result == DialogResult.OK;
        }
    }
}
