using System;
using System.Collections.Generic;
using System.Text;

namespace EVALab.Util.Filter
{
    public class FIR:IFilter
    {

        private volatile double[] data = null;
        private volatile double[] coefficients = null;
        private volatile int idx = 0;
        private volatile int n = 0;
        public FIR(double[] coefficients)
        {
            this.coefficients = coefficients;
            this.data = new double[coefficients.Length];
            this.n = coefficients.Length;
        }

        public FIR()
        {
            this.coefficients = new double[] { .1, .1, .1, .1, .1, .1, .1, .1, .2 };
            this.data = new double[coefficients.Length];
            this.n = coefficients.Length;
        }


        #region IFilter Members

        double IFilter.FilterValue(double value)
        {
            double valueReturn = 0;
            data[idx++ %n] = value;
            for (int i = 0; i < n; i++)
            {
                valueReturn += data[ (i+idx) % n]*coefficients[i];
            }
            
            return valueReturn;
        }

        double IFilter.NoiseSignalRatio()
        {
            return 1;
        }

        #endregion
    }
}
