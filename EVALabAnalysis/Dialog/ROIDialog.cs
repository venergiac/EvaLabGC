//#define BASE_LICENSE

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EVALabAnalysis.Dialog
{
    public partial class ROIDialog : Form
    {
        List<EVALab.Analysis.ROI.ROIList> list = null;
        public ROIDialog()
        {
            InitializeComponent();
        }

        public EVALab.Analysis.ROI.ROIList SelectedObject
        {
            get { return list[listBox.SelectedIndex]; }
        }

        public void SetList(List<EVALab.Analysis.ROI.ROIList> list)
        {
            this.list = list;
            foreach (EVALab.Analysis.ROI.ROIList o in list)
            {
                this.listBox.Items.Add(o.Name);
            }
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonOK.Enabled = true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
#if BASE_LICENSE
            EVALab.Util.Box.ExceptionForm.Show(null, "You don't have license", new Exception("Base license installed. You don't have right to use this functionality.\n Send email to giacomo.veneri@gmail.com"));
#else
            this.DialogResult = DialogResult.OK;
            this.Close();
#endif
        }
    }
}
