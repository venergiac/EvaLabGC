using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using EVALabGC.ASL;
using System.Media;

namespace EVALabGC
{
    class GCForm : Form1
    {

        private Image image = null;
        private Image imageBack = null; //Image.FromFile(@"picture.bmp");
        private SoundPlayer soundPlayer = null;
        private bool soundLoop = false;
        private int nFilter = 0;
        private double thFilter = 0;
        
        public override void Init(Context ctx)
        {
            base.Init(ctx);
            InitParameters();
        }

        private void InitParameters()
        {
            if (task != null)
            {
                string path = task.Get("path");
                string fg = task.Get("foreground");
                if (fg != null)
                {
                    SetTextureBrush(@"" + path + "/" + fg);
                }
                string bg = task.Get("background");
                if (bg != null)
                {
                    SetBackground(@"" + path + "/" + bg);
                }
                string sound = task.Get("sound");
                string soundLoop = task.Get("soundloop");
                if (sound != null)
                {
                    SetSound(@"" + path + "/" + sound, ((soundLoop!=null) && (soundLoop.Equals("true"))));
                }
                string hole = task.Get("hole");
                if (hole != null)
                {
                    this.ellipseWidth = Int32.Parse(hole);
                    this.ellipseHeight = Int32.Parse(hole);
                }
                string nStr = task.Get("n");
                if (nStr != null)
                {
                    nFilter = Int32.Parse(nStr);
                }
                string thStr = task.Get("th");
                if (thStr != null)
                {
                    thFilter = Int32.Parse(thStr);
                }

                if (nFilter>0) {
                    filterX = new EVALab.Util.Filter.ReactiveFIR(thFilter,nFilter);
                    filterY = new EVALab.Util.Filter.ReactiveFIR(thFilter,nFilter);
                } else {
                    filterX = null;
                    filterY = null;
                }
            }
        }

        public void SetTextureBrush(string file)
        {
            image = Image.FromFile(@file);
            myTextureBrush = new TextureBrush(image);
        }

        public void SetBackground(string file)
        {
            imageBack = Image.FromFile(@file);
            this.BackgroundImage = imageBack;
        }

        public void SetSound(string file, bool looping)
        {
            this.soundLoop = looping;
            soundPlayer = new SoundPlayer(@file);
            soundPlayer.Load();
            if (soundPlayer.IsLoadCompleted) this.context.Log("Sound loaded successfully");
        }

        //events
        private double currentX = 0;
        private double currentY = 0;
        private double previousX = 0;
        private double previousY = 0;

        private EVALab.Util.Filter.IFilter filterX = null;
        private EVALab.Util.Filter.IFilter filterY = null;
        /*protected override void reader_ASLEvent(object sender, ASL.ASLDataEventArgs e)
        {
            GetCurrentStimulusId(e.Data);
        }*/

        public override void OnStart()
        {
            if (soundPlayer != null)
            {
                if (soundLoop) soundPlayer.PlayLooping();
                else soundPlayer.Play();
                this.context.Log("Sound playing");
            }
            base.OnStart();
        }

        public override void OnStop()
        {
            if (soundPlayer != null)
            {
                soundPlayer.Stop();
                this.context.Log("Sound stopped");
            }
            base.OnStop();
        }

        public override long GetCurrentStimulusId(Data data)
        {
            if ((data.X > Context.WIDTH) || (data.X < 0) || (data.Y > Context.HEIGHT) || (data.Y < 0) || (data.Pupil<=0))
            {
                return 0;
            }

            double x = (filterX==null) ? data.X : filterX.FilterValue(data.X);
            double y = (filterY==null) ? data.Y : filterY.FilterValue(data.Y);
            double X = ((double)(x) * (double)Width / Context.WIDTH) - ellipseWidth / 2;
            double Y = ((double)(y) * (double)Height / Context.HEIGHT) - ellipseHeight / 2;
            DoCoordinates(X, Y);

            return 0;
        }

        protected void DoCoordinates(double X, double Y)
        {
            //System.Diagnostics.Debug.WriteLine(Math.Abs(X - previousX) + " " + Math.Abs(Y - previousY) + " " + memory);
            if ((Math.Abs(X - previousX) >= ((double)ellipseWidth) * memory / 20.0) || (Math.Abs(Y - previousY) >= ((double)ellipseWidth) * memory / 20.0))
            {
                currentX = X;
                currentY = Y;
                //this.Invalidate(new Rectangle((int)previousX, (int)previousY, 2 * (int)ellipseWidth, 2 * (int)ellipseHeight));
                this.Invalidate();
                previousX = X;
                previousY = Y;
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (context.Reader.Status != Reader.ReaderStatus.Started) return;

            Ellipse_Move(pe.Graphics, (int)( currentX), (int)(currentY), ellipseWidth, ellipseHeight);
            base.OnPaint(pe);
        }
#if DEBUG 
        protected override void OnMouseMove(object sender, MouseEventArgs e)
        {
            

            //double X = (double)(e.X) - ellipseWidth / 2;
            //double Y = (double)(e.Y) - ellipseHeight / 2;
           
            //DoCoordinates(X, Y);

        }
#endif

        private Brush myTextureBrush = new SolidBrush(Color.Gray);
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

                if (imageBack != null) {
                    //myGraphics.DrawImage(image,0,0);
                } else {
                    myGraphics.Clear(this.BackColor);
                }
                //myGraphics.PixelOffSetMode = PixelOffSetMode.HighQuality;
                //myGraphics.FillEllipse(new SolidBrush(this.BackColor), (int)previousX, (int)previousY, sizeX, sizeY);
                //myGraphics.FillEllipse(new SolidBrush(Color.Red), X, Y, sizeX, sizeY);
                myGraphics.FillEllipse(myTextureBrush, X, Y, sizeX, sizeY);

#if DEBUG
                //myGraphics.DrawEllipse(new Pen(Color.White), X, Y, sizeX, sizeY);
#endif
            }
        }

        protected override void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GCForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(496, 348);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "GCForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.GCForm_Load);
            this.ResumeLayout(false);

        }

        private void GCForm_Load(object sender, EventArgs e)
        {
             this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer, true);
        }

        public override Image GetPreviewImage()
        {
            return this.image;
        }

    }
}
