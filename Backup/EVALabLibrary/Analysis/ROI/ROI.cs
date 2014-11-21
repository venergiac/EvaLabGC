using System;
using System.Collections.Generic;
using System.Text;

namespace EVALab.Analysis.ROI
{
    
    public interface ROI
    {
        string Id
        {
            get;
        }
        bool IsInRoi(double x, double y);
        double Distance(double x, double y);
    }
}
