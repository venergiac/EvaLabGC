using System;
using System.Collections.Generic;
using System.Text;

namespace EVALab.Util.Filter
{
    public class Butterworth : IFilter
    {

        const double GAIN = 3.414213562e+00;

        double[] xv = null;
        double[] yv = null;
        double[] zCoeff;
        double[] polesCoeff;
        double gain;

        public Butterworth(double[] zCoeff, double gain)
        {
            xv = new double[zCoeff.Length+1];
            yv = new double[zCoeff.Length+1];
            this.zCoeff = zCoeff;
            this.polesCoeff = new double[zCoeff.Length+1];
            for (int i = 0; i < (polesCoeff.Length)/2+1; i++)
            {
                polesCoeff[i] = polesCoeff[polesCoeff.Length - i - 1] = (i) * polesCoeff.Length + 1;

            }
            this.gain = gain;
        }

        public double FilterValue(double value)
        {
            for (int i = 0; i < yv.Length - 1; i++)
            {
                yv[i] = yv[i + 1];
            }

            xv[xv.Length - 1] = value / gain;
            for (int i = 0; i < xv.Length - 1; i++)
            {
                xv[i] = xv[i + 1];
                yv[yv.Length - 1] += polesCoeff[i] * xv[i]
                         + (zCoeff[i] * yv[i]);
            }

            yv[yv.Length - 1] = yv[xv.Length - 1] * polesCoeff[xv.Length - 1];

            return yv[yv.Length -1] ;
        }


        public double NoiseSignalRatio()
        {
            return 0;
        }
    }
}
