using System;
using System.Collections.Generic;
using System.Text;

namespace EVALab.Util.Filter
{
    public interface IFilter
    {

        double FilterValue(double value);
        double NoiseSignalRatio();
    }
}
