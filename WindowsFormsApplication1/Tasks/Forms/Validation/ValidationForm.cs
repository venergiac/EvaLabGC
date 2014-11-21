using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using EVALabGC.ASL;
using System.Diagnostics;
using System.Drawing.Imaging;
using EVALabGC.Store;
using System.ComponentModel;
using System.Collections;

namespace EVALabGC.Tasks.Forms.Validation
{
    public class ValidationForm : Form1
    {
        private double time = 1000;
        private volatile int currentPoint = -1;
        private volatile int nPoint = 0;

        private volatile int[] x = null;
        private volatile int[] y = null;
        private volatile double[] xv = null;
        private volatile double[] yv = null;

        private bool paramsReady = false;
        private bool resultReady = false;
        private Image image = null;
        private Experiment experiment = null;

        public override void Init(Context ctx)
        {
            base.Init(ctx);
            Debug.WriteLine("Initialised");
            
        }

        public override void FullScreen(Screen scrn)
        {
            base.FullScreen(scrn);
            InitParameters();
        }

        private void InitParameters()
        {
            image = null;
            resultReady = false;
            experiment = null;
            currentPoint = -1;
            Random rnd = new Random(10);

            int leftMargin = rnd.Next(10, 100);
            int rightMargin = rnd.Next(this.ClientSize.Width - 100, this.ClientSize.Width - 100);
            int topMargin = rnd.Next(10, 100);
            int bottomMargin = rnd.Next(this.ClientSize.Height - 100, this.ClientSize.Height - 100);
            int middleMargin = this.ClientSize.Height/2;

            //Prepare point
            x = new int[]{  leftMargin,
                            rightMargin,
                            leftMargin,
                            leftMargin,
                            rightMargin,
                            rightMargin};
            y = new int[] { topMargin,
                            topMargin,
                            middleMargin,
                            bottomMargin,
                            middleMargin,
                            bottomMargin};

            xv = new double[x.Length];
            yv = new double[y.Length];
            
            nPoint = Math.Min(y.Length, x.Length);
            paramsReady = true;
        }

        public override long GetCurrentStimulusId(Data data)
        {
            if (data.Time >= (currentPoint + 1) * time)
            {

                currentPoint++;
                if ((currentPoint >= nPoint) || (currentPoint < 0))
                {
                    resultReady = true;
                    paramsReady = false;
                    Stop();
                }
                this.Invalidate();

            }
            else if ((data.Time >= (currentPoint + 1) * time -500) && (data.Pupil>0))  //last 200 ms
            {
                xv[currentPoint] += data.X;
                yv[currentPoint] += data.Y;
                xv[currentPoint] /= 2;
                yv[currentPoint] /= 2;
            }
            return currentPoint;
        }

        protected void Stop()
        {
            context.Reader.Stop();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (context.Reader.Status != Reader.ReaderStatus.Started) return; 
            if (resultReady)
            {
                pe.Graphics.DrawImage(MakeImageResult(), 0, 0, this.ClientSize.Width, this.ClientSize.Height);
                return;
            }
            if ((currentPoint >= nPoint) || (currentPoint < 0)) return;
            ShowDot(pe.Graphics, x[currentPoint], y[currentPoint]);
            base.OnPaint(pe);
        }

        private void ShowDot(Graphics g, int x, int y)
        {
            g.FillEllipse(new SolidBrush(Color.Red),x - 5, y - 5, 11, 11);
            g.FillEllipse(new SolidBrush(Color.White), x - 1, y - 1, 3, 3);
        }

        private Image MakeImageResult()
        {
            Bitmap im = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            Graphics g = Graphics.FromImage(im);
            for (int i = 0; i < nPoint; i++)
            {
                g.FillEllipse(new SolidBrush(Color.Red), x[i] - 2, y[i] - 2, 5, 5);
                g.DrawLine(new Pen(Color.Green, 3), x[i], y[i], (float)((double)this.ClientSize.Width * xv[i] / (double)EVALabGC.Context.WIDTH), (float)((double)this.ClientSize.Height * yv[i] / (double)EVALabGC.Context.HEIGHT));
            }

            return im;
        }

        private Experiment MakeExperimentResult()
        {
            ValidationExperiment experiment = new ValidationExperiment(this.task.Name);
            experiment.CornerTopLeft = GetError(0);
            experiment.CornerTopRight = GetError(1);
            experiment.CornerBottomRight = GetError(3);
            experiment.CornerBottomLeft = GetError(5);
            for (int i = 0; i < nPoint; i++) experiment.Precision += GetError(i) / (double)nPoint;
            return experiment;
        }

        private double GetError(int i)
        {
            if ((x == null) || (y == null) || (xv == null) || (yv == null)) return 100;
            return Math.Sqrt(Math.Pow(x[i] - (float)((double)this.ClientSize.Width * xv[i] / (double)EVALabGC.Context.WIDTH), 2) + Math.Pow(y[i] - (float)((double)this.ClientSize.Height * yv[i] / (double)EVALabGC.Context.HEIGHT), 2));
        }

        public override Image GetPreviewImage()
        {
            Debug.WriteLine("GetPreviewImage is " + resultReady);
            if (resultReady)
            {
                if (image == null) image = MakeImageResult();
                
                return image;
            }
            return null;
        }

        protected override void InitializeComponent()
        {
            base.InitializeComponent();
            // 
            // ValidationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(600, 500);
            this.Name = "ValidationForm";
            this.Text = "Validation";
            this.ResumeLayout(false);
        }

        public override Experiment Experiment
        {
            get {
                if (experiment == null) experiment = MakeExperimentResult(); 
                return experiment;
            }
        }

    }

    /// <summary>
    /// Dedicated class for experiment
    /// </summary>
    public class ValidationExperiment : Experiment
    {

        public ValidationExperiment(string name): base(name, new Hashtable())
        {
        }

        private double cornerTopLeft = 0;

        [CategoryAttribute("Validation"),
        DescriptionAttribute("Corner Top Left error in pixel"), ReadOnly(true)]
        public double CornerTopLeft
        {
            get { return cornerTopLeft; }
            set { cornerTopLeft = value; }
        }
        private double cornerTopRight = 0;

        [CategoryAttribute("Validation"),
        DescriptionAttribute("Corner Top Right error in pixel"), ReadOnly(true)]
        public double CornerTopRight
        {
            get { return cornerTopRight; }
            set { cornerTopRight = value; }
        }
        private double cornerBottomLeft = 0;

        [CategoryAttribute("Validation"),
        DescriptionAttribute("Corner Bottom Left error in pixel"), ReadOnly(true)]
        public double CornerBottomLeft
        {
            get { return cornerBottomLeft; }
            set { cornerBottomLeft = value; }
        }
        private double cornerBottomRight = 0;

        [CategoryAttribute("Validation"),
        DescriptionAttribute("Corner Bottom Right error in pixel"), ReadOnly(true)]
        public double CornerBottomRight
        {
            get { return cornerBottomRight; }
            set { cornerBottomRight = value; }
        }

        private double precision = 0;

        [CategoryAttribute("Validation"),
        DescriptionAttribute("Mean error in pixel"), ReadOnly(true)]
        public double Precision
        {
            get { return precision; }
            set { precision = value; }
        }

        public override string ToXml() 
        {
            string xml = "<validation>";
            xml += "<error>";
            xml += "<global>"+precision + "</global>";
            xml += "<point name=\"CornerBottomRight\">"+CornerBottomRight + "</point>";
            xml += "<point name=\"CornerBottomLeft\">"+CornerBottomLeft + "</point>";
            xml += "<point name=\"CornerTopRight\">" + CornerTopRight + "</point>";
            xml += "<point name=\"CornerTopLeft\">" + CornerTopLeft + "</point>";
            xml += "</error>";
            xml += "</validation>";

            return xml;
        }
    }
}
