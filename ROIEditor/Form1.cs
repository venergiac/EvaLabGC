using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ROIEditor
{
    public partial class Form1 : Form
    {
        Bitmap bmp = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                bmp = new Bitmap(openFileDialog1.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.BackgroundImage = new Bitmap(bmp, new Size(pictureBox1.Width, pictureBox1.Height));
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog(this);
        }

        private double x1 = 0.0;
        private double y1 = 0.0;
        private double x2 = 0.0;
        private double y2 = 0.0;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            x1 =((double)(e.X) / (double)pictureBox1.Width);
            y1 = ((double)(e.Y) / (double)pictureBox1.Height);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            x2 = ((double)(e.X) / (double)pictureBox1.Width);
            y2 = ((double)(e.Y) / (double)pictureBox1.Height);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            int x1 = (int)Math.Round(Math.Min(this.x1,this.x2)* bmp.Width);
            int x2 = (int)Math.Round(Math.Max(this.x1, this.x2) * bmp.Width);
            int y1 = (int)Math.Round(Math.Min(this.y1, this.y2) * bmp.Height);
            int y2 = (int)Math.Round(Math.Max(this.y1, this.y2) * bmp.Height);
            textBox1.Text += " ROI Transient "+ x1 + " " + y1 + " " + (x2 - x1) + " " + (y2 - y1) +" PutIn "+ toolStripTextBox1.Text + ".bmp " + x1 + " " + y1 +"\n";
            Bitmap bmpCrop = bmp.Clone(new Rectangle(x1,
                y1,
                x2 - x1,
                y2 - y1),
                bmp.PixelFormat);
            bmpCrop.Save(toolStripTextBox1.Text + ".bmp");
        }
    }
}
