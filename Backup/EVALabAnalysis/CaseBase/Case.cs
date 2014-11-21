using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVALabAnalysis.CaseBase
{
    public class Case
    {

        public Case(string name, string description, string diagnosis)
        {
            this.name = name;
            this.description = description;
            this.diagnosis = diagnosis;
        }

        string name = "Anonymous";

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string description = "Unknown";

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        string diagnosis = "Unknown";

        public string Diagnosis
        {
            get { return diagnosis; }
            set { diagnosis = value; }
        }

        List<IFeature> features = new List<IFeature>();

        public List<IFeature> Features
        {
            get { return features; }
        }

    }
}
