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

        public void Merge(IndicatorList list2)
        {
            List<Indicator> list1 = new List<Indicator>();
            list1.AddRange(list2.List);

            foreach (Indicator id1 in list)
            {
                bool found = false;
                for (int i = 0; i < list2.List.Count; i++)
                {
                    Indicator id2 = list2.List[i];
                    if ((id1.Family.Equals(id2.Family)) && (id1.Name.Equals(id2.Name))) {
                        found = true;
                    } 

                }

                if (!found)
                {
                    list1.Add(id1);
                }
            }

            list = list1;
        }

        public Object Clone()
        {
            IndicatorList obj = new IndicatorList(Name);
            foreach (Indicator it in list)
            {
                obj.List.Add((Indicator)it.Clone());
            }
            return obj;
        }
    }

    [Serializable()]
    public class Indicator : DataObject
    {

        string family = "#";

        public string Family
        {
            get { return family; }
            set { family = value; }
        }

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

        int idxStart = -1;

        public int IdxStart
        {
            get { return idxStart; }
            set { idxStart = value; }
        }

        int idxEnd = -1;

        public int IdxEnd
        {
            get { return idxEnd; }
            set { idxEnd = value; }
        }

        public Indicator(string family, string name, double value, double variance, string unitName, int idxStart, int idxEnd) : this(family, name,  value, variance,  unitName, 0, idxStart,  idxEnd)
        {
        }

        public Indicator(string family, string name, double value, double variance, string unitName, double reference, int idxStart, int idxEnd)
        {
            this.Family = family;
            this.Name = name;
            this.Value = value;
            this.Variance = variance;
            this.UnitName = unitName;
            this.Reference = reference;
            this.idxStart = idxStart;
            this.idxEnd = idxEnd;
        }

        public object Clone()
        {
            return new Indicator(Family, Name, Value, Variance, UnitName, Reference,IdxStart,IdxEnd);
        }
    }
}
