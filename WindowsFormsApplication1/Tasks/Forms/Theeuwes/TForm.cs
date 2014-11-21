using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EVALabGC.ASL;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.IO;
using System.Collections;

namespace EVALabGC.Theeuwes
{
    public partial class TForm : Form1
    {

        public double circleSizeWidth = 36;
        public double circleSizeHeight = 36;

        public double centerCircleSizeWidth = 11;
        public double centerCircleSizeHeight = 11;

        public Color defaultColor = Color.Green;
        public Color onColor = Color.Green;
        public Color abruptColor = Color.Red;
        public Color offColor = Color.Gray;

        public int nValidPosition = 6;
        public int nAbruptPosition = 6;

        public int onPosition = -1;
        public int abruptPosition = -1;

        public int centerR = 265; //9,6*27,6

        private Random rnd = new Random();


        private double lastTime = 0;
        private long onTime = 1200;
        private long offTime = 600;
        private long maxTime = 120000; //5min


        public TForm()
        {
            InitializeComponent();
        }

        public override void Init(Context ctx)
        {
            base.Init(ctx);
            if (task != null)
            {
                onTime = (long)ToDouble(task.Map, "onTime", onTime);

                offTime = (long)ToDouble(task.Map, "offTime", offTime);

                maxTime = (long)ToDouble(task.Map, "maxTime", maxTime);

                centerR = (int)ToDouble(task.Map, "ray", centerR);

                circleSizeWidth = circleSizeHeight = ToDouble(task.Map, "size", circleSizeWidth);

                centerCircleSizeWidth = centerCircleSizeHeight = ToDouble(task.Map, "centerSize", centerCircleSizeWidth);
            }
        }

        private double ToDouble(Hashtable map, string name, double defaultValue)
        {
            try
            {
                return Double.Parse((string)map[name]);
            }
            catch
            {
                return defaultValue;
            }
        }


        protected override void OnPaint(PaintEventArgs pe)
        {
            if (context.Reader.Status != Reader.ReaderStatus.Started) return;
            base.OnPaint(pe);
            pe.Graphics.FillRectangle(Brushes.Black, this.ClientRectangle);
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            DrawAll(pe.Graphics);
        }

        public override string GetStimulusName(long id)
        {
            if (id >= 514)
            {
                return "Saccade + Abrupt";
            }
            else if (id > 0)
            {
                return "Saccade";
            }
            else
            {
                return "Center";
            }
        }

        public override int GetCompletition()
        {
            return (int)(100.0 * (double)lastTime / (double)maxTime);
        }

        public override long GetCurrentStimulusId(Data data)
        {
            if (data.Time >= maxTime)
            {
                context.Reader.Stop();
                return 0;
            }
            Guess(data.Time);
            if (onPosition >= 0)
            {
                Debug.WriteLine("THEUWES: onPosition=" + onPosition + " abruptPosition=" + abruptPosition + " code=" + (1 << (onPosition + 1) | 1 << (abruptPosition + 9)));
                return 1L << (onPosition +1) | 1L << (abruptPosition + 9);
            }
            else
            {
                return 0;
            }
        }

        public void DrawCircle(Graphics g, int position, Color c, bool fill)
        {
            DrawCircle(g, this.Size.Width / 2, this.Size.Height / 2, centerR, position, nValidPosition, c, fill);
        }

        public void DrawCircle(Graphics g, int position, int nPosition, Color c, bool fill)
        {
            DrawCircle(g, this.Size.Width / 2, this.Size.Height / 2, centerR, position, nPosition, c, fill);
        }

        public void DrawCircle(Graphics g, int centerX, int centerY, int centerR, int position, int nPosition, Color c, bool fill)
        {
            float x = (float)Math.Round(centerX + ((double)centerR) * Math.Cos(2.0 * Math.PI * (double)position / (double)nPosition) - circleSizeWidth / 2);
            float y = (float)Math.Round(centerY + ((double)centerR) * Math.Sin(2.0 * Math.PI * (double)position / (double)nPosition) - circleSizeHeight / 2);

            if (fill)
            {
                g.FillEllipse(new SolidBrush(c), x, y, (float)circleSizeWidth, (float)circleSizeHeight);
            }
            else
            {
                g.DrawEllipse(new Pen(c, 3), x, y, (float)circleSizeWidth, (float)circleSizeHeight);
            }
        }

        public void DrawCenterCircle(Graphics g, Color c)
        {
            DrawCenterCircle(g, this.Size.Width / 2, this.Size.Height / 2, c);
        }

        public void DrawCenterCircle(Graphics g, int x, int y, Color c)
        {
            g.FillEllipse(new SolidBrush(c), (float)(x - centerCircleSizeHeight / 2.0), (float)(y - centerCircleSizeHeight / 2.0), (float)centerCircleSizeWidth, (float)centerCircleSizeHeight);
        }


        public void Guess(double time)
        {
            lock (this)
            {
                //Debug.WriteLine(onPosition + " " + (time - lastTime));
                if ((onPosition >= 0) && (time - lastTime >= onTime))
                {
                    //reset
                    lastTime = time;
                    onPosition = -1;
                    abruptPosition = -1;
                    this.Invalidate();
                }
                else if ((onPosition < 0) && (time - lastTime >= offTime))
                {
                    //time to change
                    onPosition = rnd.Next(0, nValidPosition);
                    abruptPosition = rnd.Next(0, nValidPosition + nAbruptPosition);
                    lastTime = time;
                    if (abruptPosition % 2 == 0) abruptPosition = -1;
                    this.Invalidate();
                }
            }
        }

        public void DrawAll(Graphics g)
        {
            if (onPosition >= 0)
            {
                //DrawCenterCircle(g, offColor);
                for (int i = 0; i < nValidPosition; i++)
                {
                    if (i == onPosition)
                    {
                        DrawCircle(g, i, onColor, true);
                    }
                    else
                    {
                        DrawCircle(g, i, offColor, false);
                    }
                }

                if (abruptPosition >= 0)
                {
                    DrawCircle(g, abruptPosition, nAbruptPosition + nValidPosition, abruptColor, false);
                }
            }
            else
            {
                DrawCenterCircle(g, onColor);
                for (int i = 0; i < nValidPosition; i++)
                {
                    DrawCircle(g, i, defaultColor, false);
                }
            }
        }

        private void TForm_Load(object sender, EventArgs e)
        {
        }

        private void TForm_ResizeEnd(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void TForm_Paint(object sender, PaintEventArgs e)
        {
            this.Invalidate();
        }

        public override Image GetPreviewImage()
        {
            System.Reflection.Assembly thisExe;
            thisExe = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream file = thisExe.GetManifestResourceStream("EVALabGC.Tasks.Forms.Theeuwes.tpreview.bmp");
            return Image.FromStream(file);
        }
    }
}
