using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using EVALabGC.ASL;
using System.Collections;
using System.Threading;

namespace EVALabGC.ROI
{
    public partial class Form : Form1
    {
        public Form()
        {
            InitializeComponent();
        }

        private Image image = null; //Image.FromFile(@"picture.bmp");
        private MyList stimuli = new MyList(1000);
        private double timeEnd = -1;
        private int currentStimulus = 0;

        private Thread asyncThread = null;
        private int asyncThreadTime = -1;
        private bool stopAsyncThread = true;

        public override void Init(Context context)
        {
            base.Init(context);
            if (task != null)
            {
                string path = this.task.Get("path");
                ParseBackgroundAsyncThread(this.task.Get("asyncThreadTime"));
                ParseBackground(this.task.Get("background"));
                ParseCommand(path, new StringReader(this.task.Get("commands")));
            }
        }

        protected void ParseBackground(string background)
        {
            if (background == null) return;
            if (background.ToLower().Equals("black"))
            {
                this.BackColor = Color.Black;
            }
            else if (background.ToLower().Equals("red"))
            {
                this.BackColor = Color.Red;
            }
            else if (background.ToLower().Equals("gray"))
            {
                this.BackColor = Color.Gray;
            }
            else if (background.ToLower().Equals("blue"))
            {
                this.BackColor = Color.Blue;
            }
            else if (background.ToLower().Equals("green"))
            {
                this.BackColor = Color.Green;
            }
            else if (background.ToLower().Equals("white"))
            {
                this.BackColor = Color.White;
            }
            else
            {
                background = background.Trim();
                string[] c = background.Split(',');
                this.BackColor = Color.FromArgb(Int32.Parse(c[0].Trim()), Int32.Parse(c[1].Trim()), Int32.Parse(c[2].Trim()));
            }
        }

        //FOR COMMAND
        protected void ParseCommand(string directory, StringReader reader)
        {
            int idx = 0;
            try
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null) return;
                    if (line.Length <= 0) continue;
                    context.Log(line);
                    string[] data = line.Split(new char[]{' ','\t'});
                    string command = data[0];

                    if (command.Equals("ROI"))
                    {
                        string regionType = data[1];
                        int x = Int32.Parse(data[2]);
                        int y = Int32.Parse(data[3]);
                        int w = Int32.Parse(data[4]);
                        int h = Int32.Parse(data[5]);
                        string[] parameters = new string[data.Length - 7];
                        Array.Copy(data, 7, parameters, 0, parameters.Length);
                        parameters[0] = directory + "\\" + parameters[0];

                        ROI.Region region = new ROI.Region(idx, regionType, x, y, w, h, data[6], parameters);
                        stimuli.Add(region);

                    }
                    else if (command.Equals("REM"))
                    {

                    }
                    else if (command.Equals("STOP"))
                    {
                        timeEnd = Int32.Parse(data[1]);
                        break;
                    }
                    else
                    {
                        string[] parameters = new string[data.Length - 2];
                        Array.Copy(data, 2, parameters, 0, parameters.Length);
                        parameters[0] = directory + "\\" + parameters[0];
                        Action action = new Action(idx, data[1], parameters);
                        stimuli.Add(action); 
                    }
                    Debug.WriteLine(line);
                    idx++;
                }

                stimuli.DoOptimizationByTime(timeEnd);
            }
            catch (Exception e)
            {
                throw new Exception("Error on file at line " + idx, e);
            }
        }

        protected void ParseBackgroundAsyncThread(string asyncThreadTimeStr)
        {
            asyncThreadTime = -1;
            if (asyncThreadTimeStr != null)
            {
                asyncThreadTime = Int32.Parse(asyncThreadTimeStr);
                asyncThread = new Thread(new ThreadStart(Run));
            }
            
        }

        protected override void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(594, 574);
            this.Name = "Form";
            this.Text = "ROI Gaze Contingent";
            this.Load += new System.EventHandler(this.ROIForm_Load);
            this.ResumeLayout(false);

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        private void ROIForm_Load(object sender, EventArgs e)
        {
            if (image != null)
            {
                this.BackgroundImage = image;
            }
        }

        private const double ASLWIDTH = 261.0;
        private const double ASLHEIGHT = 240.0;

        private double currentTime = 0;
        private double currentX = 0;
        private double currentY = 0;
        private double[] previousX = new double[20];
        private double[] previousY = new double[20];
        private long currentIdx = 0;

        private double lastDrawTime = 0;
        

        /*protected override void reader_ASLEvent(object sender, ASL.ASLDataEventArgs e)
        {
            GetCurrentStimulusId(e.Data);
        }*/

        public override void OnStart()
        {
 	         base.OnStart();
             if (asyncThread != null)
             {
                 stopAsyncThread = false;
                 asyncThread.Start();
             }
        }

        public override void OnStop()
        {
 	         base.OnStop();
             stopAsyncThread = true;
        }

        public override int GetCurrentStimulusId(Data data)
        {
            currentTime = data.Time;
            if (stopAsyncThread)
            {
                if (currentTime - lastDrawTime >= this.refreshMsToWait)
                {
                    this.Invalidate();
                }
                else
                {
                    this.InvalidateAction();
                }

            }
            if ((data.X > ASLWIDTH) || (data.X < 0) || (data.Y > ASLHEIGHT) || (data.Y < 0))
            {
                return currentStimulus;
            }
            
            double X = ((double)(data.X) * (double)Width / ASLWIDTH);
            double Y = ((double)(data.Y) * (double)Height / ASLHEIGHT);
            currentX = X;
            currentY = Y;
           
            return currentStimulus;
        }

        protected override void OnMouseMove(object sender, MouseEventArgs e)
        {
            currentX = e.X;
            currentY = e.Y;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if ((context==null) || (context.Reader.Status != Reader.ReaderStatus.Started)) return;
            base.OnPaint(pe);
            //currentStimulus = 0;
            
            lastDrawTime = currentTime;
            ShowAction(pe.Graphics, (int)(currentX), (int)(currentY), currentTime);
            ShowROI(pe.Graphics, (int)(currentX), (int)(currentY), currentTime);
        }

        private void InvalidateAction()
        {
            ShowAction(null, (int)(currentX), (int)(currentY), currentTime);
            ShowROI(null, (int)(currentX), (int)(currentY), currentTime);

        }

        /// <summary>
        /// Just for async thread
        /// </summary>
        protected void Run()
        {
            while ((stopAsyncThread == false) && (context.Reader.Status == Reader.ReaderStatus.Started))
            {
                this.Invalidate();
                Debug.WriteLine("Run");
                Thread.Sleep(asyncThreadTime);  // simulate a lot of processing

            }
        }

        public override int GetCompletition()
        {
            return (int)(100.0 * (double)currentTime / (double)timeEnd);
        }

        private void ShowROI(Graphics myGraphics, int X, int Y, double currentTime)
        {
            if ((currentTime >= timeEnd) && (timeEnd > 0) && (context.Reader.Status == Reader.ReaderStatus.Started))
            {
                context.Log("STOP");
                reader.Stop();
                stopAsyncThread = true;
                return;
            }
            if ((X >= (0)) && (Y >= (0)) && (X <= this.Width) && (Y <= this.Height))
            {
                if (myGraphics == null) myGraphics = this.CreateGraphics();

                currentStimulus = stimuli.ShowROI(this, currentStimulus, myGraphics, X, Y, currentTime);
            }
        }

        private void ShowAction(Graphics myGraphics, int X, int Y, double currentTime)
        {
            if ((currentTime >= timeEnd) && (timeEnd > 0) && (context.Reader.Status == Reader.ReaderStatus.Started))
            {
                context.Log("STOP");
                reader.Stop();
                stopAsyncThread = true;
                return;
            }
            currentStimulus = stimuli.ShowAction(this, currentStimulus, myGraphics, X, Y, currentTime);
        }
    }

    /**
     * Lista ottimizzata
     **/

    class MyList
    {

        private List<ROI.Region> regions = new List<ROI.Region>();
        private List<ROI.Action> actions = new List<ROI.Action>();

        private List<ROI.Region>[] listRegion = null;
        private List<ROI.Action>[] listAction = null;

        private double timeEnd = -1;
        private double timeWindow = 1000.0;

        public MyList(double timeWindow)
        {
            this.timeWindow = timeWindow;
        }

        public void Add(ROI.Action action)
        {
            timeEnd = Math.Max(action.TimeStart, timeEnd);
            timeEnd = Math.Max(action.TimeEnd, timeEnd);

            actions.Add(action);
        }

        public void Add(ROI.Region region)
        {
            timeEnd = Math.Max(region.Action.TimeStart, timeEnd);
            timeEnd = Math.Max(region.Action.TimeEnd, timeEnd);
            regions.Add(region);
        }

        public void DoOptimizationByTime(double timeEnd)
        {
            if (timeEnd>0) this.timeEnd = timeEnd;

            //allocazione
            listRegion = new List<ROI.Region>[(int)Math.Ceiling(timeEnd/timeWindow) + 1];
            listAction = new List<ROI.Action>[(int)Math.Ceiling(timeEnd / timeWindow) + 1];

            //ottimizza le azioni
            foreach (ROI.Action action in actions)
            {
                int idxStart = (int)((double)action.TimeStart / timeWindow);
                idxStart = Math.Max(0, idxStart);
                int idxEnd = (int)Math.Ceiling((double)action.TimeEnd / timeWindow);
                idxEnd = Math.Max(listAction.Length - 1, idxEnd);

                for (int i = idxStart; i <= idxEnd && idxEnd < listAction.Length; i++)
                {
                    if (listAction[i] == null)
                    {
                        listAction[i] = new List<Action>(2);
                    }
                    listAction[i].Add(action);
                }
            }


            //ottimizza le regioni
            foreach (ROI.Region region in regions)
            {
                int idxStart = (int)((double)region.Action.TimeStart / timeWindow);
                idxStart = Math.Max(0, idxStart);
                int idxEnd = (int)Math.Ceiling((double)region.Action.TimeEnd / timeWindow);
                idxEnd = Math.Max(listRegion.Length - 1, idxEnd);

                for (int i = idxStart; i <= idxEnd && idxEnd < listRegion.Length; i++)
                {
                    if (listRegion[i] == null)
                    {
                        listRegion[i] = new List<Region>(2);
                    }
                    listRegion[i].Add(region);
                }
            }
        }

        public int ShowAction(Form owner, int currentStimulus, Graphics myGraphics, int X, int Y, double currentTime)
        {
            List<ROI.Action> actions = listAction[(int)(currentTime / timeWindow)];
            if (actions == null) return currentStimulus;
            
            foreach (ROI.Action item in actions)
            {
                if (item.GetStatus(owner, currentTime) == ROI.Action.ActionStatus.Active) item.Do(myGraphics, X, Y, currentTime);
                currentStimulus |= 1 << item.ActionId;
            }

            return currentStimulus;
        }

        public int ShowROI(Form owner, int currentStimulus, Graphics myGraphics, int X, int Y, double currentTime)
        {
            List<ROI.Region> regions = listRegion[(int)(currentTime / timeWindow)];
            if (regions == null) return currentStimulus;

            foreach (ROI.Region item in regions)
            {
                if (item.GetStatus(X, Y) == ROI.Region.RegionStatus.Active)
                {
                    if (item.Action.GetStatus(owner, currentTime) == ROI.Action.ActionStatus.Active)
                    {
                        item.Action.Do(myGraphics, X, Y, currentTime);
                        currentStimulus |= 1 << item.Action.ActionId;
                    }
                }
            }

            return currentStimulus;
        }

    }

}
