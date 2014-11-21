using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace EVALabGC.ASL
{
    public class Data
    {
        private int status;
        private int x;
        private int y;
        private int pupil;
        private double time;
        private int stimulusId;

        public int StimulusId
        {
            get { return stimulusId; }
            set { stimulusId = value; }
        }

        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public int Pupil
        {
            get
            {
                return pupil;
            }
            set
            {
                pupil = value;
            }
        }

        public double Time
        {
            get
            {
                return time;
            }
        }

        public void ParseData(int stimulusId, byte[] data, double time)
        {
            this.stimulusId = stimulusId;
            x = (int)Math.Round(data[4] * 256.0 + data[5]);
            y = (int)Math.Round(data[6] * 256.0 + data[7]);
            pupil = (int)Math.Round(data[1] * 256.0 + data[2]);
            status = data[0];
            this.time = time;
        }

        public Data(int stimulusId, byte[] data, double time)
        {
            ParseData(stimulusId, data, time);
        }

        public override string ToString()
        {
            return x + "  " + y + "  " + pupil + "  " + time;
        }
    }
}
