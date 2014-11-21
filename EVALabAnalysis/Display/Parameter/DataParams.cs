using System;
using System.Collections.Generic;
using System.Text;

namespace EVALabAnalysis.Parameter
{
    public class DataParams
    {

        protected DataParams()
        {
            pixelsPerDegreeX = pixelsPerDegreeX * (double)Properties.Settings.Default.PixelX;
            pixelsPerDegreeY = pixelsPerDegreeY * (double)Properties.Settings.Default.PixelY;
        }

        protected static DataParams singletone = new DataParams();
        public static DataParams Instance()
        {
            return singletone;
        }

        private double pixelsPerDegreeX = 27.35 / 640.0;

        public double PixelsPerDegreeX
        {
            get { return pixelsPerDegreeX; }
            set { pixelsPerDegreeX = value; }
        }
        private double pixelsPerDegreeY = 27.35 / 480.0;

        public double PixelsPerDegreeY
        {
            get { return pixelsPerDegreeY; }
            set { pixelsPerDegreeY = value; }
        }

        private int ticksPerDegree = 10;

        public int TicksPerDegree
        {
            get { return ticksPerDegree; }
            set { ticksPerDegree = value; }
        }
    }
}
