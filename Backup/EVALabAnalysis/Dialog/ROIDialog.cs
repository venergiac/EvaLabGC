using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            get { return list.ElementAt(listBox.SelectedIndex); }
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
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
