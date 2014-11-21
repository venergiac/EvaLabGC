using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using EVALabGC.ASL;
using EVALab.Util.Input;
using Microsoft.Xna.Framework;

namespace EVALabGC.Tasks.Forms.Test
{
    public partial class TestForm : Form1
    {
        ForceFeedbackManager ffManager = null;
        public TestForm()
        {
            InitializeComponent();
        }

        public override void Init(Context ctx)
        {
            base.Init(ctx);

            InitParameters();
        }

        private void InitParameters()
        {
   
        }

        //events
        private double currentX = 0;
        private double currentY = 0;

        public override long GetCurrentStimulusId(Data data)
        {
            if ((data.X > Context.WIDTH) || (data.X < 0) || (data.Y > Context.HEIGHT) || (data.Y < 0))
            {
                return 0;
            }

            /*if ((context.Joystick != null) && (ffManager==null))
            {
                ffManager = new ForceFeedbackManager(PlayerIndex.One);
                ffManager.AddVibration(0.7f, 0.7f, 100000);
            }

            if (ffManager != null)
            {
                ffManager.Update((float)data.Time);
            }*/

            currentX = data.X;
            currentY = data.Y;
            this.Invalidate();
            return 0;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (context.Reader.Status != Reader.ReaderStatus.Started) return;

            Ellipse_Move(pe.Graphics, (int)(currentX), (int)(currentY), ellipseWidth, ellipseHeight);
            base.OnPaint(pe);
        }


        /// <summary>
        /// Ellipse HOLE
        /// </summary>
        private void Ellipse_Move(Graphics myGraphics, int X, int Y, int sizeX, int sizeY)
        {
            if ((X >= (-sizeX)) && (Y >= (-sizeY)) && (X <= this.Width) && (Y <= this.Height))
            {
                if (myGraphics == null) myGraphics = this.CreateGraphics();
                myGraphics.SmoothingMode = SmoothingMode.HighSpeed;
                myGraphics.CompositingQuality = CompositingQuality.HighSpeed;

                myGraphics.DrawEllipse(new Pen(System.Drawing.Color.Red), X - 10, Y - 10, 10, 10);

#if DEBUG
                //myGraphics.DrawEllipse(new Pen(Color.White), X, Y, sizeX, sizeY);
#endif
            }
        }

        private void GCForm_Load(object sender, EventArgs e)
        {
           this.SetStyle(
           ControlStyles.UserPaint |
           ControlStyles.AllPaintingInWmPaint |
           ControlStyles.OptimizedDoubleBuffer, true);
           InitParameters();
        }

        public override Image GetPreviewImage()
        {
            return null ;
        }

    }
}
