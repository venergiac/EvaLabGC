using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace EVALab.Util.Graph
{
    public partial class EvaListBoxCtrl : UserControl
    {
        public EvaListBoxCtrl()
        {
            InitializeComponent();
        }

        public void AddItem(string txt, int imageIdx)
        {
            this.eListBox1.Items.Add(new EListBoxItem(txt,imageIdx));
        }

        public void Clear()
        {
            this.eListBox1.Items.Clear();
        }
    }
}
