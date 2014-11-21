using System;
using System.Collections.Generic;
using System.Text;

namespace EVALab.Analysis.ROI
{
    [Serializable()]
    public class ROIList : DataObject
    {
        private List<ROI> list = new List<ROI>();

        public List<ROI> List
        {
            get { return list; }
            set { list = value; }
        }

        public ROIList(string name)
        {
            this.Name = name;
        }

        public ROI FindNearestROI(double xc, double yc)
        {
            ROI bestRoi = null;
            double d = Double.MaxValue;
            foreach (ROI r in list)
            {
                double dd = r.Distance(xc, yc);
                if (dd <= d)
                {
                    bestRoi = r;
                    d = dd;
                    if (d == 0) return r;
                }
            }
            return bestRoi;
        }
    }
}
