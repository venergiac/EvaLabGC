using System;
using System.Collections.Generic;
using System.Text;

namespace EVALab.Util.Meta
{
    public class ExperimentSettings
    {
        private string id = "";

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name = "";

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string surname = "";

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        private char gender = 'M';

        public char Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        private DateTime birthDate = DateTime.Now;

        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }
        private DateTime experimentDate = DateTime.Now;

        public DateTime ExperimentDate
        {
            get { return experimentDate; }
            set { experimentDate = value; }
        }

        private string experimentName = "";

        public string ExperimentName
        {
            get { return experimentName; }
            set { experimentName = value; }
        }

        private string type = "";

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private string details = "";

        public string Details
        {
            get { return details; }
            set { details = value; }
        }

        private float distance = 75;

        public float Distance
        {
            get { return distance; }
            set { distance = value; }
        }
        private float width = 100;

        public float Width
        {
            get { return width; }
            set { width = value; }
        }

        public string ToXML()
        {
            string ret = "<settings id=\""+this.id+"\">";

            ret +="<subject>";
            ret += "<name><![CDATA[" + this.name + "]]></name>";
            ret += "<surname><![CDATA[" + this.surname + "]]></surname>";
            ret += "<gender>" + this.gender + "</gender>";
            ret += "<birth><![CDATA[" + this.birthDate.ToShortDateString() + "]]></birth>";
            ret += "<details><![CDATA[" + this.details + "]]></details>";
            ret += "</subject>";

            ret += "<experiment>";
            ret += "<name><![CDATA[" + this.experimentName + "]]></name>";
            ret += "<date><![CDATA[" + this.experimentDate.ToString() + "]]></date>";
            ret += "<type><![CDATA[" + this.type + "]]></type>";
            ret += "<width><![CDATA[" + this.width + "]]></width>";
            ret += "<distance><![CDATA[" + this.distance + "]]></distance>";
            ret += "</experiment>";


            ret += "</settings>";

            return ret;
        }
    }
}
