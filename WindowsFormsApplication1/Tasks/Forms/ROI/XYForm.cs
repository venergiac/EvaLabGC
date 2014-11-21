using System;
using System.Collections.Generic;
using System.Text;

namespace EVALabGC.ROI
{
    public class XYForm : EVALabGC.ROI.Form
    {

        protected override void InitStimulusList(string timeWindowStr, string targetStr)
        {
            int timeWindow = 1000;
            try
            {
                timeWindow = Int32.Parse(timeWindowStr);
            }
            catch (Exception) { }
            stimuli = new MyList(timeWindow, true, true);
        }
    }
}
