using System;
using System.Collections.Generic;
using System.Text;
using EVALab.Util.Statistics;

namespace EVALab.Util.Filter
{
    public class Median : IFilter
    {
        private double[] data = null;
        public Median(int order)
        {
            data = new double[order];
        }

        public double FilterValue(double value)
        {
            //shift array
            for (int i = 0; i < data.Length - 1; i++)
            {
                data[i] = data[i + 1];
            }
            data[data.Length - 1]=value;

            DescriptiveResult result= Descriptive.Compute(data);
            return result.Median;
        }

        public double NoiseSignalRatio()
        {
            return 0;
        }

    }
}
