using System;
using System.Collections.Generic;

using System.Text;

namespace EVALabAnalysis.Display.Parameter
{
    public class TagParams
    {
        public TagParams(string name)
        {
            this.name = name;
        }

        protected string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

    }
}
