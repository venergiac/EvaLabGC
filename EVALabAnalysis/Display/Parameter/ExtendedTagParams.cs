using System;
using System.Collections.Generic;
using System.Text;

namespace EVALabAnalysis.Display.Parameter
{
    public class ExtendedTagParams : TagParams
    {
        public ExtendedTagParams(string name, string group, string note) : base(name)
        {
            this.group = group;
            this.note = note;
        }


        protected string group;

        public string Group
        {
            get { return group; }
            set { group = value; }
        }

        protected string note;

        public string Note
        {
            get { return note; }
            set { note = value; }
        }

    }
}
