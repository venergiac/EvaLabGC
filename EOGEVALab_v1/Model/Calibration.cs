using System;
using System.Collections.Generic;
using System.Text;

namespace EvaLab.EOG.Model
{
    public class Calibration
    {
        private double maxValueCh1;
        private double minValueCh1;
        private double maxValueCh2;
        private double minValueCh2;
        private double maxReference;
        private double minReference;

        private bool maxSet = false;
        private bool minSet = false;

        public bool Ready()
        {
            return maxSet && minSet;
        }

        public void SetMax(double maxReference, double maxValueCh1, double maxValueCh2)
        {
            this.maxValueCh1 = maxValueCh1;
            this.maxValueCh2 = maxValueCh2;
            this.maxReference = maxReference;
            maxSet = true;
        }

        public void SetMin(double minReference, double minValueCh1, double minValueCh2)
        {
            this.minValueCh1 = minValueCh1;
            this.minValueCh2 = minValueCh2;
            this.minReference = minReference;
            minSet = true;
        }

        public double ScaleCh1(double value)
        {
            if ((!maxSet) || (!minSet)) return value;
            return (double)(maxReference - minReference) * (double)(value - minValueCh1) / (double)(maxValueCh1 - minValueCh1) + minReference;
        }


        public double ScaleCh2(double value)
        {
            if ((!maxSet) || (!minSet)) return value;
            return (double)(maxReference - minReference) * (double)(value - minValueCh2) / (double)(maxValueCh2 - minValueCh2) + minReference;
        }

        public void Reset()
        {
            minSet = false;
            maxSet = false;
        }

        public string ToXML()
        {
            string ret = "<calibration>";
            ret += "<ch1 min=\"" + minValueCh1 + "\" max=\"" + maxValueCh1 + "\"/>";
            ret += "<ch2 min=\"" + minValueCh2 + "\" max=\"" + maxValueCh2 + "\"/>";
            ret += "<reference min=\"" + minReference + "\" max=\"" + maxReference + "\"/>";
            ret += "</calibration>";

            return ret;
        }

    }
}
