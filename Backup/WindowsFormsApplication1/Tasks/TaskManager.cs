using System;
using System.Collections.Generic;
using System.Text;
using EVALabGC.Object;
using System.Collections;
using System.Xml.XPath;
using System.IO;
using EVALab.Util.Box;

namespace EVALabGC.Object
{
    public class TaskManager
    {

        private List<Task> tasks = new List<Task>();

        public List<Task> Tasks
        {
            get { return tasks; }
            set { tasks = value; }
        }

        private TaskManager()
        {
        }

        private static TaskManager mngr = new TaskManager();
        public static TaskManager Instance()
        {
            return mngr;
        }

        public Task ParseFile(string filename, Stream stream)
        {

            FileInfo info = new FileInfo(filename);
            
            XPathDocument doc = new XPathDocument(stream);
            XPathNavigator nav = doc.CreateNavigator();
            try
            {
                string id = GetXPathNode(nav, "/task[@id]", "id");
                string name = GetXPathNode(nav, "/task/name",null);
                string description = GetXPathNode(nav, "/task/description", null);
                Hashtable map = GetXPathNodes(nav, "/task/settings/param[@name]", "name");
                if (map["file"] == null) map.Add("file",filename);
                if (map["path"] == null) map.Add("path", info.DirectoryName);
                Task task = new Task(id, name, description, map);
                tasks.Add(task);
                return task;

           }
            catch (Exception ex)
            {
                ExceptionForm.Show(null, "Error on parsing file", ex);
                return null;
            }
        }

        private string GetXPathNode(XPathNavigator nav, string xpath)
        {
            return GetXPathNode(nav, xpath, null);
        }

        private string GetXPathNode(XPathNavigator nav, string xpath, string attribute)
        {
            // Compile a standard XPath expression

            XPathExpression expr;
            expr = nav.Compile(xpath);
            XPathNodeIterator iterator = nav.Select(expr);

            // Iterate on the node set
            while (iterator.MoveNext())
            {

                XPathNavigator nav2 = iterator.Current.Clone();
                if (attribute != null) return nav2.GetAttribute(attribute, nav2.BaseURI);
                return iterator.Current.Value;
            }

            return null;
        }

        private Hashtable GetXPathNodes(XPathNavigator nav, string xpath, string attribute)
        {
            // Compile a standard XPath expression
            Hashtable map = new Hashtable();
            XPathExpression expr;
            expr = nav.Compile(xpath);
            XPathNodeIterator iterator = nav.Select(expr);

            // Iterate on the node set
            while (iterator.MoveNext())
            {

                XPathNavigator nav2 = iterator.Current.Clone();
                string key = nav2.GetAttribute(attribute, nav2.BaseURI);
                string value = iterator.Current.Value;
                map.Add(key, value);
            }

            return map;
        }


        public Task MakeCalibrationTask()
        {
            return new Task(Task.TaskTypeId.Calibration.ToString(), Task.TaskTypeId.Calibration.ToString(), "Calibration form", new Hashtable(), false, false);
        }

        public Task MakeValidationTask()
        {
            return new Task(Task.TaskTypeId.Validation.ToString(), Task.TaskTypeId.Validation.ToString(), "Validation form", new Hashtable(), true, false);
        }
    }
}
