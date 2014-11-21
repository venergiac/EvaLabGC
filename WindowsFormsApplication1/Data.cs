using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace EVALabGC
{
    public class Data
    {
        private int status;
        private double x;
        private double y;
        private int pupil;
        private double time;
        private long stimulusId;

        public long StimulusId
        {
            get { return stimulusId; }
            set { stimulusId = value; }
        }

        public double X
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

        public double Y
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

        public Data(int stimulusId, double x, double y, double pupil, double time)
        {
            this.stimulusId = stimulusId;
            this.x = x;
            this.y = y;
            this.time = time;
            this.pupil = (int)pupil;
        }

        public override string ToString()
        {
            return x + "  " + y + "  " + pupil + "  " + time;
        }
    }
}
