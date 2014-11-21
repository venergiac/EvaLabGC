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
    public partial class PropertyDlg : Form
    {
        private object selectedObject = null;

        public object SelectedObject
        {
            get { return selectedObject; }
            set { selectedObject = value; }
        }


        public PropertyDlg()
        {
            InitializeComponent();
        }

        private void PropertyDlg_Load(object sender, EventArgs e)
        {
            this.propertyGrid1.SelectedObject = selectedObject;
        }
    }
}
