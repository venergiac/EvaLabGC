using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Microsoft.Win32;

namespace EVALabGC
{
    class CalibrateForm : Form1
    {
        private Image image = null;

        protected override void InitializeComponent()
        {
            //base.InitializeComponent();
            // 
            // CalibrateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CalibrateForm";
            this.ShowInTaskbar = false;
            this.Text = "Calibration";
            this.Load += new System.EventHandler(this.CalibrateForm_Load);
            this.ResumeLayout(false);

        }

        private void CalibrateForm_Load(object sender, EventArgs e)
        {
            System.Reflection.Assembly thisExe;
            thisExe = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream file = thisExe.GetManifestResourceStream("EVALabGC.Tasks.Forms.Calibration.calibration.bmp");
            image= Image.FromStream(file);
            this.BackgroundImage = image;
        }
    }
}
