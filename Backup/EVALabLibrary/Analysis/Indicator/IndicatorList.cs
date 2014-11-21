using System;
using System.Collections.Generic;
using System.Text;

namespace EVALab.Analysis.Indicator
{
    [Serializable()]
    public class IndicatorList: DataObject
    {
        private List<Indicator> list = new List<Indicator>();
        public List<Indicator> List
        {
            get { return list; }
            set { list = value; }
        }

        public IndicatorList(string name)
        {
            this.Name = name;
        }
    }

    [Serializable()]
    public class Indicator : DataObject
    {

        string unitName = "#";

        public string UnitName
        {
            get { return unitName; }
            set { unitName = value; }
        }


        double value = 0;

        public double Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        double variance = 0;

        public double Variance
        {
            get { return variance; }
            set { variance = value; }
        }

        double reference = 0;

        public double Reference
        {
            get { return reference; }
            set { reference = value; }
        }



        public Indicator(string name, double value, double variance, string unitName)
        {
            this.Name = name;
            this.Value = value;
            this.Variance = variance;
            this.UnitName = unitName;
        }

        public Indicator(string name, double value, double variance, string unitName, double reference)
        {
            this.Name = name;
            this.Value = value;
            this.Variance = variance;
            this.UnitName = unitName;
            this.reference = reference;
        }
    }
}
