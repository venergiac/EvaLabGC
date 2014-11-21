using System;
using System.Collections.Generic;
using System.Text;
using EVALab.Analysis.Data;

namespace EVALab.Analysis
{
    [Serializable()]
    public class Trial : DataObject
    {
        private List<DataList> data = new List<DataList>();

        public List<DataList> Data
        {
            get { return data; }
            set { data = value; }
        }


        private string note = "";

        public string Note
        {
            get { return note; }
            set { note = value; }
        }

        private string group = "";

        public string Group
        {
            get { return group; }
            set { group = value; }
        }

    }
}
