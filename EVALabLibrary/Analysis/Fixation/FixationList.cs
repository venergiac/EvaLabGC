using System;
using System.Collections.Generic;
using System.Text;

namespace EVALab.Analysis.Fixation
{
    [Serializable()]
    public class FixationList : DataObject
    {
        private List<Fixation> list = new List<Fixation>();

        public List<Fixation> List
        {
            get { return list; }
            set { list = value; }
        }

        public FixationList(string name)
        {
            this.Name = name;
        }

        public Object Clone()
        {
            FixationList obj = new FixationList(Name);
            foreach (Fixation it in list)
            {
                obj.List.Add((Fixation)it.Clone());
            }
            return obj;
        }
    }

    [Serializable()]
    public class Fixation
    {

        public Fixation(double x, double y, double dispersion, long duration, int startIdx, int endIdx, double error)
        {
            this.x = x;
            this.y = y;
            this.dispersion = dispersion;
            this.duration = duration;
            this.startIdx = startIdx;
            this.endIdx = endIdx;
            this.error = error;
        }

        public Fixation(double x, double y, double dispersion, long duration, int startIdx, int endIdx, double error, ROI.ROI nearestROI)
        {
            this.x = x;
            this.y = y;
            this.dispersion = dispersion;
            this.duration = duration;
            this.startIdx = startIdx;
            this.endIdx = endIdx;
            this.error = error;
            this.nearestROI = nearestROI;
        }

        private double x;
        public double X
        {
            get { return x; }
            set { x = value; }
        }
        private double y;

        public double Y
        {
            get { return y; }
            set { y = value; }
        }
        private double dispersion;

        public double Dispersion
        {
            get { return dispersion; }
            set { dispersion = value; }
        }

        private double error;

        public double Error
        {
            get { return error; }
            set { error = value; }
        }

        private long duration;

        public long Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        private int startIdx;

        public int IdxStart
        {
            get { return startIdx; }
            set { startIdx = value; }
        }
        private int endIdx;

        public int IdxEnd
        {
            get { return endIdx; }
            set { endIdx = value; }
        }

        protected ROI.ROI nearestROI = null;
        public ROI.ROI NearestROI
        {
            get { return nearestROI; }
            set { nearestROI = value; }
        }

        public Object Clone()
        {
            return new Fixation(x, y, dispersion, duration, startIdx, endIdx, error, nearestROI);
        }
    }
}
