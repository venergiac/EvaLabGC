using System;
using System.Collections.Generic;
using System.Text;
using EVALab.Analysis.Saccade;
using EVALab.Analysis.Fixation;
using EVALab.Analysis.WaveForm;
using System.Runtime.Serialization;
using EVALab.Analysis.Indicator;

namespace EVALab.Analysis.Data
{

    [Serializable()]
    public class DataList : DataObject 
    {
        private List<Item> list = new List<Item>();
        private SaccadeList saccades = null;

        public SaccadeList Saccades
        {
            get { return saccades; }
            set { saccades = value; }
        }
        private FixationList fixations = null;

        public FixationList Fixations
        {
            get { return fixations; }
            set { fixations = value; }
        }

        private WaveFormList waveForms = null;

        public WaveFormList WaveForms
        {
            get { return waveForms; }
            set { waveForms = value; }
        }

        private IndicatorList indicators = null;

        public IndicatorList Indicators
        {
            get { return indicators; }
            set
            {
                if (indicators == null)
                {
                    indicators = value;
                }
                else
                {
                    indicators.Merge(value);
                }
            }
        }

        private string[] label = { "X", "Y" , "Z"};

        private DataList()
        {
        }

        public DataList(string name)
        {
            this.Name = name;
            saccades = new SaccadeList(name);
            fixations = new FixationList(name);
            waveForms = new WaveFormList(name);
            indicators = new IndicatorList(name);
        }

        public DataList(string name, string[] label, DataType type)
        {
            this.Name = name;
            this.label = label;
            this.Type = type;
            saccades = new SaccadeList(name);
            fixations = new FixationList(name);
            waveForms = new WaveFormList(name);
            indicators = new IndicatorList(name);
        }

        public static DataList NewInstance(DataList instance)
        {
            return new DataList(instance.Name, instance.label,instance.Type);
        }

        public string GetLabel(int i)
        {
            return label[i % label.Length];
        }

        public List<Item> List
        {
            get { return list; }
            set { list = value; }
        }

        /// <summary>
        /// Calcola la velocità
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public DataList Velocity(int position)
        {
            DataType type = DataType.VelocityDegreePerSec;
            if (this.Type == DataType.Pixels) type = DataType.VelocityPixelsPerSec;
            if (this.Type == DataType.Tick) type = DataType.VelocityTickPerSec;
            DataList velocity = new DataList(this.Name + " Velocity", label, type);

            Item prev = null;
            foreach (Item item in list)
            {
                double[] vel = new double[item.Value.Length+1];
                long time = item.Time;
                if (prev != null)
                {
                    double normal = 0;
                    for (int i = 0; i < item.Value.Length; i++)
                    {
                        if ((position < 0) || (i == position))
                        {
                            vel[i] = 1000.0 * (item.Value[i] - prev.Value[i]) / Math.Max(3, item.Time - prev.Time);
                            normal += vel[i] * vel[i];
                        }
                    }
                    vel[item.Value.Length] = Math.Sqrt(normal);
                }
                
                velocity.List.Add(new Item(vel, time));
                prev = item;
            }

            return velocity;
        }

        public Item Max(int valuePosition)
        {
            return Max(valuePosition, 0, List.Count);
        }

        public Item Max(int valuePosition, int startIdx, int endIdx)
        {
            Item itemRet = new Item(0, 0, 0);
            double maxValue = Double.NegativeInfinity;
            for (int i = startIdx; i < endIdx; i++)
            {
                Item item = List[i];
                if ((valuePosition < item.Value.Length) && (item.Value[valuePosition] > maxValue))
                {
                    itemRet = item;
                    maxValue = item.Value[valuePosition];
                }
            }

            return itemRet;
        }

        public Item Min(int valuePosition)
        {
            return Min(valuePosition, 0, List.Count);
        }
        
        public Item Min(int valuePosition, int startIdx, int endIdx)
        {
            Item itemRet = new Item(0,0,0);
            double minValue = Double.PositiveInfinity;
            for (int i = startIdx; i < endIdx; i++)
            {
                Item item = List[i];
                if ((valuePosition < item.Value.Length) && (item.Value[valuePosition] < minValue))
                {
                    itemRet = item;
                    minValue = item.Value[valuePosition];
                }
            }

            return itemRet;
        }

        public double Mean(int valuePosition, int startIdx, int endIdx)
        {
            double ret = 0;
            for (int i = startIdx; i < endIdx; i++)
            {
                Item item = List[i];
                if ((valuePosition < item.Value.Length) )
                {
                    ret += item.Value[valuePosition];
                }
            }

            return ret / (double)(endIdx-startIdx);
        }

        /// <summary>
        /// Clone current object
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            DataList obj = DataList.NewInstance(this);
            foreach (Item it in this.List)
            {
                obj.List.Add(it.Clone());
            }
            obj.Saccades = (SaccadeList)this.Saccades.Clone();
            obj.Fixations = (FixationList)this.Fixations.Clone();
            obj.WaveForms = (WaveFormList)this.WaveForms.Clone();
            obj.Indicators = (IndicatorList)this.Indicators.Clone();

            return obj;
        }

    }

    [Serializable()]
    public class Item
    {

        public static int POSITIONX = 0;
        public static int POSITIONY = 1;

        private bool valid = true;

        public bool Valid
        {
            get { return valid; }
            set { valid = value; }
        }

        private double[] value = null;
        private double[] reference = null;
        private double[] distracter = null;

        private double pupil = 0;

        public double Pupil
        {
            get { return pupil; }
            set { pupil = value; }
        }

        public double[] Reference
        {
            get { return reference; }
            set { reference = value; }
        }

        public double[] Distracter
        {
            get { return distracter; }
            set { distracter = value; }
        }

        public double[] Value
        {
            get { return this.value; }
            set { this.value = value; }
        }


        private long time = 0;

        public long Time
        {
            get { return time; }
            set { time = value; }
        }

        public Item(double x, double y, long time)
        {
            value = new double[] { x, y };
            this.time = time;
        }

        public Item(double x, double y, double pupil, double refX, double refY, long time)
        {
            value = new double[] { x, y };
            reference = new double[] { refX, refY };
            this.pupil = pupil;
            this.time = time;
        }

        public Item(double[] value, long time)
        {
            this.value = value;
            this.time = time;
        }

        public Item(double[] value, double pupil, double[] reference, long time)
        {
            this.value = value;
            this.pupil = pupil;
            this.time = time;
            this.reference = reference;
        }

        public Item(double[] value, double pupil, double[] reference, double[] distracter, long time)
        {
            this.value = value;
            this.pupil = pupil;
            this.time = time;
            this.reference = reference;
            this.distracter = distracter;
        }

        public int CountValue
        {
            get {
                if (value == null) return 0;
                return value.Length;
            }
        }

        public int CountReference
        {
            get {
            if (reference == null) return 0;
            return reference.Length;
            }
        }

        public Item Clone()
        {
            if (this.distracter!=null) return new Item((double[])this.value.Clone(), this.pupil, (double[])this.reference.Clone(), (double[])this.distracter.Clone(), this.time);
            else return new Item((double[])this.value.Clone(), this.pupil, (double[])this.reference.Clone(), null, this.time);
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < value.Length; i++)
            {
                str += value[i] + " ";
            }
            str += pupil + " ";
            str += time + " ";
            for (int i = 0; i < reference.Length; i++)
            {
                str += reference[i] + " ";
            }
            return str.Trim();
        }

        public double dinstance(Item item)
        {
            double ret = 0;
            for (int i = 0; i < this.value.Length; i++)
            {
                ret += dinstance(item, i);
            }
            return ret;
        }

        public double dinstance(Item item, int position)
        {
            if ((position >= value.Length) && (position >= item.Value.Length)) return 0;
            return value[position] - item.Value[position];
        }

        public void sum(double[] value)
        {
            for (int i = 0; i < value.Length && i < this.value.Length; i++)
            {
                this.value[i] += value[i];
            }
        }

        public void multiply(double[] value)
        {
            for (int i = 0; i < value.Length && i < this.value.Length; i++)
            {
                this.value[i] *= value[i];
            }
        }
    }
}
