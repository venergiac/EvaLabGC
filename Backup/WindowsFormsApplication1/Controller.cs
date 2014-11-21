using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using EVALabGC.ROI;
using EVALab.Util.Input;
using EVALab.Util;
using System.Windows.Forms;
using EVALabGC.ASL;

namespace EVALabGC
{
    public class Controller : System.Windows.Forms.Form
    {
        protected int counter = 0;

        protected Context context = null;
        protected Joystick joystick = null;

        delegate void StopCallback();
        /// <summary>
        /// Initialize
        /// </summary>
        protected void Init()
        {
            IniFile file = new IniFile(Application.StartupPath + "\\Config.ini");
            context = new Context(this, file);
            context.Reader.ASLReaderEvent += new EVALabGC.ASL.Reader.ASLReaderEventHandler(Reader_ASLReaderEvent);
            context.Reader.ASLDataEvent += new EVALabGC.ASL.Reader.ASLDataEventHandler(Reader_ASLDataEvent);

            //look for joystick
            Joystick joystick = new Joystick(this.Handle);
            string[] joys = joystick.FindJoysticks();
            for (int i = 0; (joys != null) && (i < joys.Length); i++)
            {
                Log("Found joystick " + joys[i]);
            }
            if ((joys != null) && (joys.Length > 0))
            {
                this.joystick = joystick;
                this.joystick.AcquireJoystick(joys[0]);
                Log("Acquired joystick " + joys[0]);
            }
        }

        protected virtual void Reader_ASLDataEvent(object sender, EVALabGC.ASL.ASLDataEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void Reader_ASLReaderEvent(object sender, EVALabGC.ASL.ASLReaderEventArgs e)
        {
            if (e.Status == Reader.ReaderStatus.Stopped)
            {
                context.Log("STOP Request by task");
            }
            else if (e.Status == Reader.ReaderStatus.Started)
            {
                context.Log("Reader START");
            }
        }

        public Joystick Joystick
        {
            get { return joystick; }
            set { joystick = value; }
        }

        /***
        * Joystick functions
        */

        protected void JoystickUpdateStatus()
        {
            if (joystick == null) return;
            joystick.UpdateStatus();
        }

        protected int GetJoystickButtonsValue()
        {
#if DEBUG
            if (joystick == null) return 10- counter % 1100;
#endif
            if (joystick == null) return 0;
            JoystickUpdateStatus();
            int value = 0;
            for (int i = 0; i < joystick.Buttons.Length; i++)
            {
                if (joystick.Buttons[i])
                {
                    value |= 1 << i;
                }
            }
            return value;
        }

        protected int GetJoystickXValue()
        {
#if DEBUG
            if (joystick == null) return 10 - counter % 1100;
#endif
            if (joystick == null) return 0;
            JoystickUpdateStatus();
            Debug.WriteLine("X:" + (32767 - joystick.AxisC));
            return 32767 - joystick.AxisC;
        }

        protected int GetJoystickYValue()
        {
#if DEBUG
            if (joystick == null) return 10 - counter % 1000;
#endif
            if (joystick == null) return 0;
            JoystickUpdateStatus();
            Debug.WriteLine("Y:" + (32767 - joystick.AxisD));
            return 32767 - joystick.AxisD;
        }


        //LOGGING
        public virtual void Log(string message) {
            Debug.WriteLine(message);
        }

        public virtual void Log(string message, int completition)
        {
            Debug.WriteLine(message + " " + completition+"%");
        }

        #region buffer data save

        public void Reset()
        {
            Store.StorageManager.Instance().Reset();
        }

        #endregion

        #region Start & Stop
        protected virtual void Start()
        {
            try
            {
                context.Reader.Start();
            }
            catch (Exception exc)
            {
                EVALab.Util.Box.ExceptionForm.Show(this, "Error on opening port", exc);
                return;
            }
            Reset();
        }

        protected virtual void SafeStop()
        {
            this.Invoke(new StopCallback(Stop));
        }

        protected virtual void Stop()
        {
            if (context.Reader.Status == Reader.ReaderStatus.Started)
            {
                context.Reader.Stop();
            }
        }
        #endregion
    }
}
