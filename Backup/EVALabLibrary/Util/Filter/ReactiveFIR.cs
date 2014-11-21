using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace EVALab.Util.Filter
{
    public class ReactiveFIR : IFilter
    {

        private volatile double[] data = null;
        private double dataMean = 0;
        private volatile double[] coefficients = null;
        private volatile int idx = 0;
        private volatile int n = 0;
        private double th = 0;

        public ReactiveFIR(double th, int n)
        {
            this.th = th;
            this.coefficients = new double[n];
            for (int i = 0; i < n; i++) this.coefficients[i] = 1.0 / (double)n;
            this.data = new double[n];
            this.n = n;
        }

        public ReactiveFIR(double th, double[] coefficients)
        {
            this.th = th;
            this.coefficients = coefficients;
            this.data = new double[coefficients.Length];
            this.n = coefficients.Length;
        }

        public ReactiveFIR()
        {
            this.th = 25 * 10;
            this.coefficients = new double[] { .1, .1, .1, .1, .1, .1, .1, .1, .2 };
            this.data = new double[coefficients.Length];
            this.n = coefficients.Length;
        }


        #region IFilter Members

        double IFilter.FilterValue(double value)
        {
            Debug.WriteLine(value - dataMean);
            if (Math.Abs(value - dataMean) > th)
            {
                data[idx++ % n] = value;
                return value;
            }
            double valueReturn = 0;
            for (int i = 0; i < n; i++)
            {
                valueReturn += data[(i + idx) % n] * coefficients[i];
            }
            dataMean = valueReturn;

            return valueReturn;
        }

        double IFilter.NoiseSignalRatio()
        {
            return 1;
        }

        #endregion
    }
}
