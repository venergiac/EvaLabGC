using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EVALabAnalysis.CaseBase;

namespace EVALabAnalysis.Display
{
    public partial class FormFeatureSelection : Form
    {
        public FormFeatureSelection()
        {
            InitializeComponent();
        }

        private void FormFeatureSelection_Load(object sender, EventArgs e)
        {

        }

        public void SetNames(List<MetaFeature> names)
        {
            foreach (MetaFeature name in names)
            {
                this.checkedListBox1.Items.Add(name.Name, name.Enabled);
            }
        }

        public static bool Show(IWin32Window owner, List<MetaFeature> names)
        {
            FormFeatureSelection form = new FormFeatureSelection();
            form.SetNames(names);
            DialogResult result = form.ShowDialog(owner);
            return result == DialogResult.OK;
        }
    }
}
