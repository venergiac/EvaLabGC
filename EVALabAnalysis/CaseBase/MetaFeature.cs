using System;
using System.Collections.Generic;
using System.Text;

namespace EVALabAnalysis.CaseBase
{
    public class MetaFeature
    {
        private string name = "";
        private bool enabled = true;


        public MetaFeature(string name, bool enabled)
        {
            this.name = name;
            this.enabled = enabled;
        }

        public String Name
        {
            get
            {
                return name;
            }
        }

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }
    }
}
