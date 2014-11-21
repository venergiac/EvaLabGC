using System;
using System.Collections.Generic;
using System.Text;
using EVALab.Util.Input;
using System.Windows.Forms;
using System.Diagnostics;

namespace EVALabGC
{
    public class Context
    {

        public static double WIDTH = 1024;
        public static double HEIGHT = 768;
        public static double STORAGEWIDTH = WIDTH;
        public static double STORAGEHEIGHT=HEIGHT;

        private int sampleRefresh = 10;

        public int SampleRefresh
        {
            get { return sampleRefresh; }
            set { sampleRefresh = value; }
        }

        private string xidport;

        public string XidPort
        {
            get { return xidport; }
        }

        private int xidbaud;

        public int XidBaud
        {
            get { return xidbaud; }
        }

        private TrackerReader reader= null;

        public TrackerReader Reader
        {
            get { return reader; }
            set { reader = value; }
        }

        private int ellipseWidth = 100;

        public int EllipseWidth
        {
            get { return ellipseWidth; }
            set { ellipseWidth = value; }
        }
        private int ellipseHeight = 100;

        public int EllipseHeight
        {
            get { return ellipseHeight; }
            set { ellipseHeight = value; }
        }

        private int screenWidth = 1024;

        public int ScreenWidth
        {
            get { return screenWidth; }
            set { screenWidth = value; }
        }
        private int screenHeight = 768;

        public int ScreenHeight
        {
            get { return screenHeight; }
            set { screenHeight = value; }
        }

        private ControlForm form = null;

        public Joystick Joystick
        {
            get { return form.Joystick; }
        }

        private int idxAdapter = 0;
        public int IdxAdapter
        {
            get { return idxAdapter; }
            set { idxAdapter = value; }
        }


        public void Log(string message)
        {
            form.Log(message);
        }

        public void Log(string message, int completition)
        {
            form.Log(message, completition);
        }

        public Context(ControlForm form, EVALab.Util.IniFile file)
        {
            string model = file.ReadString("EYETRACKER", "model");
            string height = file.ReadString("SCREEN", "height");
            string width = file.ReadString("SCREEN", "width");
            form.Log("Opening Configuration file for model=" + model);

            if (model.Equals("Tobii"))
            {
                string name = file.ReadString("Tobii", "name");
                reader = new Tobii.TobiiReader(name);
                form.Log("Configuration: tobii=" + name + " " + " screen=" + width + "x" + height);
                WIDTH = Double.Parse(file.ReadString("Tobii", "width"));
                HEIGHT = Double.Parse(file.ReadString("Tobii", "height"));
                sampleRefresh = Int32.Parse(file.ReadString("Tobii", "sampleRefresh"));
            }
            else if (model.Equals("Fake"))
            {
                string frequency = file.ReadString("ASL", "frequency");
                reader = new ASL.FakeReader(Int32.Parse(frequency));
                WIDTH = Double.Parse(file.ReadString("ASL", "width"));
                HEIGHT = Double.Parse(file.ReadString("ASL", "height"));
                form.Log("Configuration: FakePort (THIS PORT DOES NOT REGISTER) screen=" + width + "x" + height);
                sampleRefresh = Int32.Parse(file.ReadString("ASL", "sampleRefresh"));
            }
            else
            {
                string port = file.ReadString("ASL", "port");
                string baud = file.ReadString("ASL", "baud");
                string frequency = file.ReadString("ASL", "frequency");
                reader = new ASL.Reader(port,
                      Int32.Parse(baud),
                      Int32.Parse(frequency)
                      );
                WIDTH = Double.Parse(file.ReadString("ASL", "width"));
                HEIGHT = Double.Parse(file.ReadString("ASL", "height"));
                form.Log("Configuration: port=" + port + " " + baud + " screen=" + width + "x" + height);
                sampleRefresh = Int32.Parse(file.ReadString("ASL", "sampleRefresh"));
            }
            reader.Context = this;
            this.form = form;
            screenWidth = Int32.Parse(width);
            screenHeight = Int32.Parse(height);

            STORAGEWIDTH = Double.Parse(file.ReadString("STORAGE", "width"));
            STORAGEHEIGHT = Double.Parse(file.ReadString("STORAGE", "height"));
            if (file.ReadString("XID", "port") != null) { 
                xidport= file.ReadString("XID", "port");
                try
                {
                    xidbaud = Int32.Parse(file.ReadString("XID", "baud"));
                }
                catch (Exception)
                {

                    xidbaud = 9600;
                }
                this.Log("xid device on " + xidport);
#if DEBUG
                MessageBox.Show("XID device is configure on com port " + xidport + " ;-) [DO NOT USE THIS (--DEBUG--) VERSION]");
#endif
            }
            
        }
    }
}
