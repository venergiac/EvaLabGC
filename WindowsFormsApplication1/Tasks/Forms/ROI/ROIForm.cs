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
        public Form():base()
        {
        }

        protected Image image = null; //Image.FromFile(@"picture.bmp");
        protected MyList stimuli = null;

        protected long currentStimulus = 0;

        protected Thread asyncThread = null;
        protected int asyncThreadTime = -1;
        protected bool stopAsyncThread = true;

        protected int invalidateRadius = -1;

        protected bool moveNextEnabled = false;

        public override void Init(Context context)
        {
            base.Init(context);
            if (task != null)
            {
                string path = this.task.Get("path");
                try
                {
                    string moveNextStr = this.task.Get("moveNext");
                    moveNextEnabled = Boolean.Parse(moveNextStr);
                } catch (Exception e) {
                    moveNextEnabled = false;
                }

                try
                {
                    string invalidateRadiusStr = this.task.Get("invalidateRadius");
                    invalidateRadius = Int32.Parse(invalidateRadiusStr);
                }
                catch (Exception e)
                {
                    invalidateRadius = -1;
                }

                InitStimulusList(this.task.Get("timeWindow"), this.task.Get("target"));
                ParseBackgroundAsyncThread(this.task.Get("asyncThreadTime"));
                ParseBackground(this.task.Get("background"));
                ParseCommand(path, new StringReader(this.task.Get("commands")));
            }
        }

        protected virtual void InitStimulusList(string timeWindowStr, string targetStr)
        {
            int timeWindow =1000;
            try
            {
                timeWindow = Int32.Parse(timeWindowStr);
            }
            catch (Exception) {}
            if ((targetStr != null) && (targetStr.ToLower().Equals("exclusive")))
            {
                stimuli = new MyList(timeWindow, true, false);
            } else {
                stimuli = new MyList(timeWindow, false, false);
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

        protected virtual ROI.Region MakeRegion(int id, string type, int x, int y, int width, int height, string action, string[] actionParameters)
        {
            return new ROI.Region(id, type, x, y, width, height, action, actionParameters);
        }

        protected virtual Action MakeAction(int id, string action, string[] actionParameters, bool target) 
        {
            return new Action(id, action, actionParameters, target);
        }

        //FOR COMMAND
        protected void ParseCommand(string directory, StringReader reader)
        {
            int idx = 0;
            double timeEnd = -1;
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

                        ROI.Region region = MakeRegion(idx, regionType, x, y, w, h, data[6], parameters);
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
                        Action action = MakeAction(idx, data[1], parameters, command.Equals("TARGET"));
                        stimuli.Add(action); 
                    }
                    Debug.WriteLine(line);
                    idx++;
                }

                stimuli.DoOptimizationByTime(timeEnd);
            }
            catch (Exception e)
            {
                Debug.Write(e.Message + " " + e.InnerException);
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

        protected double currentTime = 0;
        protected double currentX = 0;
        protected double currentY = 0;
        protected double[] previousX = new double[20];
        protected double[] previousY = new double[20];
        protected long currentIdx = 0;

        protected double lastDrawTime = 0;
     

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

        public override long GetCurrentStimulusId(Data data)
        {
            currentTime = data.Time;
            if (stopAsyncThread)
            {
               this.InvalidateAction(currentTime - lastDrawTime >= this.refreshMsToWait);
            }
            if ((data.X > Context.WIDTH) || (data.X < 0) || (data.Y > Context.HEIGHT) || (data.Y < 0))
            {
                return currentStimulus;
            }

            double X = ((double)(data.X) * (double)Width / Context.WIDTH);
            double Y = ((double)(data.Y) * (double)Height / Context.HEIGHT);
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
            //base.OnPaint(pe);
            //currentStimulus = 0;

            lastDrawTime = currentTime;
            Show(pe.Graphics, (int)(currentX), (int)(currentY), currentTime);
        }

        protected int targetX = -1;
        protected int targetY = -1;
        protected int actionId = -1;
        protected virtual void InvalidateAction(bool drawing)
        {
            if (drawing)
            {
                if ((invalidateRadius>=0) && (targetX >= 0) && (targetY >= 0) && (actionId == stimuli.ActionId))
                {
                    this.Invalidate(new Rectangle(targetX - invalidateRadius, targetY - invalidateRadius, invalidateRadius * 2, invalidateRadius * 2));
                }
                else
                {
                    actionId = stimuli.ActionId;
                    this.Invalidate();
                }
            } else {
                Show(null, (int)(currentX), (int)(currentY), currentTime);
            }
            targetX = stimuli.TargetX;
            targetY = stimuli.TargetY;
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
            return (int)(100.0 * (double)(currentTime + stimuli.Shift) / (double)(stimuli.TimeEnd));
        }

        private int prevJoystickButtonsValue = 0;
        private bool MoveNext()
        {
            if ((!moveNextEnabled) || (context.Joystick==null)) return false;
            if (prevJoystickButtonsValue != context.Joystick.GetJoystickButtonsValue()) 
            {
                prevJoystickButtonsValue = context.Joystick.GetJoystickButtonsValue();
                return (context.Joystick.GetJoystickButtonsValue() > 0);
            }
            return false;
        }

        protected void Show(Graphics myGraphics, int X, int Y, double currentTime)
        {
            if ((currentTime + stimuli.Shift >= stimuli.TimeEnd) && (stimuli.TimeEnd + stimuli.Shift > 0) && (context.Reader.Status == Reader.ReaderStatus.Started))
            {
                context.Log("STOP");
                reader.Stop();
                stopAsyncThread = true;
                return;
            }
            stimuli.ResetTime(currentTime);
            currentStimulus = 0; //now reset
            currentStimulus = stimuli.ShowAction(this, currentStimulus, myGraphics, X, Y, currentTime);

            if ((X >= (0)) && (Y >= (0)) && (X <= this.Width) && (Y <= this.Height))
            {
                currentStimulus = stimuli.ShowROI(this, currentStimulus, myGraphics, X, Y, currentTime);
            }

            if (MoveNext())
            {
                stimuli.DoNext(currentTime);
                context.Log("Shift to next stimulus required by user");
            }
        }
    }

    /**
     * Lista ottimizzata
     **/

    public class MyList
    {

        private List<ROI.Region> regions = new List<ROI.Region>();
        private List<ROI.Action> actions = new List<ROI.Action>();

        private List<ROI.Region>[] listRegion = null;
        private List<ROI.Action>[] listAction = null;

        private double timeEnd = -1;
        private double timeWindow = 1000.0;
        private bool targetExclusive = false;
        private bool targetXY = false;
        private int actionId = -1;

        private double currentTimeStart = 0;
        private double currentTimeEnd = 0;

        private double shift = 0;

        public void DoNext(double currentTime)
        {
            shift = currentTimeEnd - currentTime;
            Debug.WriteLine(shift + "=" + currentTimeEnd + "-" + currentTime);
            if (shift < 0) shift = 0;
        }

        public double Shift
        {
            get { return shift; }
        }

        public double TimeEnd
        {
            get { return timeEnd; }
        }

        public double CurrentTimeStart
        {
            get { return currentTimeStart; }
        }

        public double CurrentTimeEnd
        {
            get { return currentTimeEnd; }
        }

        public void ResetTime(double currentTime)
        {
            currentTimeStart = currentTime;
            currentTimeEnd = currentTime;
        }

        public int ActionId
        {
            get { return actionId; }
        }

        private int targetX = -1;

        public int TargetX
        {
            get { return targetX; }
        }
        private int targetY = -1;

        public int TargetY
        {
            get { return targetY; }
        }

        public MyList(double timeWindow, bool targetExclusive, bool targetXY)
        {
            this.timeWindow = timeWindow;
            this.targetExclusive = targetExclusive;
            this.targetXY = targetXY;
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

        public long ShowAction(Form owner, long currentStimulus, Graphics myGraphics, int X, int Y, double currentTime)
        {
            currentTime += shift; //move next 

            int cIdx = (int)(currentTime / timeWindow);
            if (cIdx >= listRegion.Length) return 0;
            List<ROI.Action> actions = listAction[cIdx];
            if (actions == null)
            {
                return currentStimulus;
            }

            foreach (ROI.Action item in actions)
            {
                if (item.GetStatus(owner, currentTime) == ROI.Action.ActionStatus.Active)
                {
                    currentTimeStart = Math.Min(currentTimeStart, item.TimeStart);
                    currentTimeEnd = Math.Max(currentTimeEnd, item.TimeEnd);
                    actionId = item.ActionId;
                    item.Do(myGraphics, X, Y, currentTime);
                    if (item.IsTarget)
                    {
                        targetX = item.TargetX;
                        targetY = item.TargetY;
                        currentStimulus = item.TargetX * 10000 + item.TargetY;
                    } else if (targetXY) {
                        targetX = -1;
                        targetY = -1;
                        currentStimulus = -1;
                    }
                    else
                    {
                        targetX = -1;
                        targetY = -1;
                        currentStimulus |= 1 << (item.ActionId % 32);
                    }
                    if (targetExclusive) break;
                }
            }

            return currentStimulus;
        }

        public long ShowROI(Form owner, long currentStimulus, Graphics myGraphics, int X, int Y, double currentTime)
        {
            currentTime += shift; //move next 

            int cIdx=(int)(currentTime / timeWindow);
            if (cIdx >= listRegion.Length) return 0;
            List<ROI.Region> regions = listRegion[cIdx];
            if (regions == null) return currentStimulus;

            foreach (ROI.Region item in regions)
            {
                if (item.GetStatus(X, Y) == ROI.Region.RegionStatus.Active)
                {
                    if (item.Action.GetStatus(owner, currentTime) == ROI.Action.ActionStatus.Active)
                    {
                        currentTimeStart = Math.Min(currentTimeStart, item.Action.TimeStart);
                        currentTimeEnd = Math.Max(currentTimeEnd, item.Action.TimeEnd);
                        actionId = item.Action.ActionId;
                        item.Action.Do(myGraphics, X, Y, currentTime);
                        if (item.Action.IsTarget)
                        {
                            targetX = item.Action.TargetX;
                            targetY = item.Action.TargetY;
                            currentStimulus = item.Action.TargetX * 10000 + item.Action.TargetY;
                        }
                        else if (targetXY)
                        {
                            targetX = -1;
                            targetY = -1;
                            currentStimulus = -1;
                        }
                        else
                        {
                            targetX = -1;
                            targetY = -1;
                            currentStimulus |= 1 << (item.Action.ActionId % 32);
                        }
                        if (targetExclusive) break;
                    }
                }
            }

            return currentStimulus;
        }

    }

}
