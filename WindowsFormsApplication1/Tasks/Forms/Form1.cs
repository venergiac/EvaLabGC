using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using EVALab.Util;
using EVALabGC.ASL;
using EVALabGC.Object;
using EVALabGC.Store;
using EVALab.Util.Output;

namespace EVALabGC
{
    public partial class Form1 : Form, StimulusController
    {

        protected TrackerReader reader = null;
        protected int ellipseWidth = 100;
        protected int ellipseHeight = 100;
        protected double memory = 0.0;

        protected Image previewImage = null;

        protected Context context = null;
        protected Task task = null;

        protected int refreshRate = -1;
        protected double refreshMsToWait = -1;

        public Form1()
        {
            this.SuspendLayout();
            InitializeBase();
            InitializeComponent();
            this.ResumeLayout(false);
       }

        protected virtual void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(600, 500);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.ResumeLayout(false);

        }

        public virtual void SetOwner(Task task)
        {
            this.task = task;
        }

        public virtual void Init(Context context)
        {
            this.context = context;
#if DEBUG
            this.MouseMove += new MouseEventHandler(OnMouseMove);
#endif
            this.reader = context.Reader;

            //NEW IMPLEMENTATION BY STIMULUS CONTROLLER
            //reader.ASLDataEvent += new ASL.Reader.ASLDataEventHandler(reader_ASLEvent);
            reader.StimulusController = this;

#if DEBUG
            this.Width=1024;
            this.Height=768;
#endif

            //OBSOLETE
            this.ellipseHeight = context.EllipseWidth;
            this.ellipseWidth = context.EllipseWidth;

            ParsePreviewImage(this.task.Get("preview"));

            //adpaters
            this.refreshRate = Graphic.GetRefreshRate(context.IdxAdapter);
            this.refreshMsToWait =  1000 / this.refreshRate; //trunc to smallest
         }

        private void ParsePreviewImage(string previewImageLocation)
        {
            string path = this.task.Get("path");
            if (previewImageLocation != null)
            {
                previewImage = Image.FromFile(path + "\\" + previewImageLocation);
            }
        }

        protected virtual void OnMouseMove(object sender, MouseEventArgs e)
        {
        }

        /*protected virtual void reader_ASLEvent(object sender, ASL.ASLDataEventArgs e)
        {
        }*/

        public virtual void OnStart()
        {
        }
        public virtual void OnStop()
        {
        }

        public virtual long GetCurrentStimulusId(Data data)
        {
            return 0;
        }

        public virtual int GetCompletition()
        {
            return 0;
        }

        public virtual string GetStimulusName(long stimulusId)
        {
            return "In progress";
        }

        public virtual Image GetPreviewImage()
        {
            
            //Bitmap bmp = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
            //this.DrawToBitmap(bmp, new Rectangle(10, this.ClientRectangle.Top, this.ClientRectangle.Width, this.ClientRectangle.Height));
            return previewImage;
        }

        // this should be in the scope your class

        clientRect restore;

        bool fullscreen = false;

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (e.KeyChar == 'f')
                {
                    FullScreen();
                }

            }
            catch(Exception exc)
            {
                EVALab.Util.Box.ExceptionForm.Show(this, "Error on full screen mode ", exc);
            }

        }

        /// <summary>
        /// Full Screen
        /// </summary>
        /// 
        public virtual void FormScreen()
        {
            base.Show();
        }

        public virtual void FullScreen()
        {
            FullScreen(Screen.FromControl(this));
        }

        public virtual void FullScreen(int screenIdx)
        {
            Screen scrn = Screen.AllScreens[screenIdx];
            FullScreen(scrn);
        }
        
        public virtual void FullScreen(Screen scrn)
        {
            base.Show();
            if (fullscreen == false)
            {
                this.restore.location = this.Location;
                this.restore.width = this.Width;
                this.restore.height = this.Height;
                this.TopMost = true;

                this.Location = scrn.Bounds.Location;
                this.FormBorderStyle = FormBorderStyle.None;
                this.Width = scrn.Bounds.Width;
                this.Height = scrn.Bounds.Height;
                fullscreen = true;
            }
            else
            {
                this.TopMost = false;
                this.Location = this.restore.location;
                this.Width = this.restore.width;
                this.Height = this.restore.height;

                // these are the two variables you may wish to change, depending
                // on the design of your form (WindowState and FormBorderStyle)
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                fullscreen = false;
            }
        }

        // a struct containing important information about the state to restore to

        struct clientRect
        {
            public Point location;
            public int width;
            public int height;
        };

        public virtual Experiment Experiment
        {
            get { return new Experiment(this.task.Name, this.task.Map); }
        }
    }
}
