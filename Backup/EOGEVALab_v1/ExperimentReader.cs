using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using EvaLab.EOG.Trigger;
using EVALab.Analysis.Data;
using System.Xml.XPath;
using System.Collections;
using System.Reflection;

namespace EvaLab.EOG
{
    /// <summary>
    /// Read experiment file
    /// </summary>
    public class ExperimentReader
    {

        /// <summary>
        /// data items
        /// </summary>
        private List<ExperimentItem> items = new List<ExperimentItem>();
        private List<ITrigger> triggers = new List<ITrigger>();

        public List<ExperimentItem> Items
        {
            get { return items; }
        }

        public List<ITrigger> Triggers
        {
            get { return triggers; }
        }

        public List<ExperimentItem> ParseXML(string filename)
        {
            XPathDocument doc = new XPathDocument(new StreamReader(filename));
            XPathNavigator nav = doc.CreateNavigator();

            Hashtable map = DataManager.GetXPathNodes(nav, "/task/settings/param[@name]", "name");

            items = new List<ExperimentItem>();
            triggers = new List<ITrigger>();

            if (map["triggers"] != null)
            {
                string data = (string)map["triggers"];
                ParseStreamTrigger(new StreamReader(new System.IO.MemoryStream(
                                        System.Text.Encoding.UTF8.GetBytes(data))));
            }
            if (map["commands"] != null)
            {
                string data = (string)map["commands"];
                ParseStreamCommands(new StreamReader(new System.IO.MemoryStream(
                                        System.Text.Encoding.UTF8.GetBytes(data))));
            }

            return this.items;
        }

        /// <summary>
        /// OLD WAY (DEPRECATO)
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public List<ExperimentItem> Parse(string filename)
        {
            StreamReader sw = null;
            try
            {
                sw = new StreamReader(filename);
                return ParseStreamCommands(sw);
            }
            finally
            {
                if (sw != null) sw.Close();
            }
        }

        /// <summary>
        /// Parse file
        /// </summary>
        /// <param name="filename"></param>
        public List<ExperimentItem> ParseStreamCommands(StreamReader sw)
        {
            try
            {
                while (!sw.EndOfStream)
                {
                    string line = sw.ReadLine();
                    line = line.Trim();
                    if ((line != null) && (line.Length > 0))
                    {
                        ExperimentItem item = parseLine(line);
                        item.Trigger = findTrigger(item.Name);
                        items.Add(item);
                    }

                }
            } catch(Exception e) {
                throw e;
            }
            

            return items;
        }

        /// <summary>
        /// Parse file
        /// </summary>
        /// <param name="filename"></param>
        private void ParseStreamTrigger(StreamReader sw)
        {
            try
            {
                while (!sw.EndOfStream)
                {
                    string line = sw.ReadLine();
                    line = line.Trim();
                    if ((line != null) && (line.Length>0))
                    {
                        triggers.Add(parseTrigger(line));
                    }

                }

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private ITrigger findTrigger(string name)
        {
            foreach (ITrigger trigger in triggers)
            {
                if (trigger.Name.Equals(name)) {
                    return trigger;
                }
            }
            return null;
        }

        private ITrigger parseTrigger(string line)
        { 

            string[] pars = line.Split(' ');
            string name = pars[0];
            string className = pars[1];
            ITrigger trigger = (ITrigger)Assembly.GetExecutingAssembly().CreateInstance(className);
            
            string[] pars2 = new string[pars.Length-2];
            for (int i = 0; i < pars2.Length;  i++)
            {
                pars2[i] = pars[i + 2];
            }

            trigger.Init(name,pars2);
            
            return trigger;
        }

        /// <summary>
        /// parse single lie
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private ExperimentItem parseLine(string line)
        {
            string[] valuesLine = line.Split(' ');
            string name="";
            int duration=0;
            int[] values = new int[valuesLine.Length - 2];
            for (int i = 0; i < valuesLine.Length; i++)
            {
                if (i == 0)
                {
                    name = valuesLine[i];
                }
                else if (i == 1)
                {
                    duration = Int32.Parse(valuesLine[i]);
                }
                else
                {
                    values[i - 2] = Int32.Parse(valuesLine[i]);
                }
            }

            return new ExperimentItem(name, duration, values);
        }

    }

    /// <summary>
    /// basic experiment item
    /// </summary>
    public class ExperimentItem
    {

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int duration;

        public int Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        private int[] values;

        public int[] Values
        {
            get { return values; }
            set { values = value; }
        }

        private ITrigger trigger;
        public ITrigger Trigger
        {
            get { return trigger; }
            set { trigger = value; }
        }

        public ExperimentItem(string name, int duration, int[] values)
        {
            this.name = name;
            this.duration = duration;
            this.values = values;
        }
    }
}
