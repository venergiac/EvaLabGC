using System;
using System.Collections.Generic;
using System.Text;
using EVALab.Util.Input;

namespace EVALabGC
{
    public class Context
    {
        private ASL.Reader reader = null;

        public ASL.Reader Reader
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

        private Controller form = null;

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

        public Context(Controller form, EVALab.Util.IniFile file)
        {
            string port = file.ReadString("ASL", "port");
            string baud = file.ReadString("ASL", "baud");
            string frequency = file.ReadString("ASL", "frequency");
            string height = file.ReadString("SCREEN", "height");
            string width = file.ReadString("SCREEN", "width");
            form.Log("Opening Configuration file");
            form.Log("Configuration: port=" + port + " " + baud + " screen=" + width + "x" + height);


            this.form = form;
            reader = new ASL.Reader(port,
                                  Int32.Parse(baud),
                                  Int32.Parse(frequency)
                                  );

            

            screenWidth = Int32.Parse(width);
            screenHeight = Int32.Parse(height);
        }
    }
}
