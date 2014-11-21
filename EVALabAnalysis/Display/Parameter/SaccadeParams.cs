using System;
using System.Collections.Generic;
using System.Text;

namespace EVALabAnalysis.Parameter
{
    public class SaccadeParams : DataParams
    {
        protected static new SaccadeParams singletone = new SaccadeParams();
        public static new SaccadeParams Instance()
        {
            return singletone;
        }

        protected SaccadeParams() { }

        double saccadeVelThresh=60;

        public double SaccadeVelThresh
        {
            get { return saccadeVelThresh; }
            set { saccadeVelThresh = value; }
        }
        double pctPeak = 10;

        public double PctPeak
        {
            get { return pctPeak; }
            set { pctPeak = value; }
        }
        double saccadeWindow=100;

        public double SaccadeWindow
        {
            get { return saccadeWindow; }
            set { saccadeWindow = value; }
        }

        double saccadeMinDuration = 40;

        public double SaccadeMinDuration
        {
            get { return saccadeMinDuration; }
            set { saccadeMinDuration = value; }
        }
    }
}
