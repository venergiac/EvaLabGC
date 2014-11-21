using System;
using System.Collections;
using System.Text;
using System.ComponentModel;

namespace EVALabGC.Store
{
    public class Experiment
    {
        private string name = "";
        private Hashtable map = null;

        [CategoryAttribute("Info"),
        DescriptionAttribute("Id"), ReadOnly(true)]
        public string Id
        {
            get { return name + " at " + creation.ToLongTimeString(); }
        }
        private DateTime creation = DateTime.Now;

        [CategoryAttribute("Info"),
        DescriptionAttribute("Creation date"), ReadOnly(true)]
        public DateTime Creation
        {
            get { return creation; }
            set { creation = value; }
        }

        public Experiment(string name, Hashtable map)
        {
            this.name = name;
            this.map = map;
        }

        private string buffer = "";

        public string Buffer
        {
            get { return buffer; }
            set { buffer = value; }
        }

        private double velocityPeak = 0;

        [CategoryAttribute("Saccade"),
        DescriptionAttribute("Max velocity in pixel/ms"), ReadOnly(true)]
        public double VelocityPeak
        {
            get { return velocityPeak; }
            set { velocityPeak = value; }
        }
        private double velocityMean = 0;

        [CategoryAttribute("Saccade"),
        DescriptionAttribute("Mean velocity in pixel/ms"), ReadOnly(true)]
        public double VelocityMean
        {
            get { return velocityMean; }
            set { velocityMean = value; }
        }

        public virtual string ToXml() 
        {
            string xml = "<type>";
            xml += "<id>" +Id+"</id>";
            xml += "<date>"+Creation.ToLongDateString() + "</date>";
            xml += "<settings>";
            foreach (string key in map.Keys)
            {
                xml += "<param name=\"" + key + "\">" + map[key] + "</param>";
            }
            xml += "</settings>";
            xml += "</type>";

            return xml;
        }
    }
}
