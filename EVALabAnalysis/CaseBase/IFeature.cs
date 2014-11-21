using System;
using System.Collections.Generic;
using System.Text;

namespace EVALabAnalysis.CaseBase
{
    public interface IFeature
    {
        string Name
        {
            get;
        }
        double Compare(IFeature feature);
    }
}
