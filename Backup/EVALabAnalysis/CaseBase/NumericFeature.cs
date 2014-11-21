using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVALabAnalysis.CaseBase
{
    public class NumericFeature : IFeature
    {
        private string name = "";
        private double value = 0;
        private double variance = 1;
        public NumericFeature(string name, double value)
        {
            this.name = name;
            this.value = value;
        }

        public NumericFeature(string name, double value, double variance)
        {
            this.name = name;
            this.value = value;
            this.variance = variance;
        }

        public String Name
        {
            get
            {
                return name;
            }
        }

        public double Value
        {
            get
            {
                return value;
            }
        }

        public double Variance
        {
            get
            {
                return variance;
            }
        }

        public double Compare(IFeature feature)
        {
            return Math.Pow(Value - ((NumericFeature)feature).Value, 2) / Variance; 
        }
    }
}
