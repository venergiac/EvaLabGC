using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.ComponentModel;
using EVALabGC.Theeuwes;
using EVALabGC.Tasks.Forms.Validation;
using EVALabGC.Tasks.Forms.Test;

namespace EVALabGC.Object
{
    [DefaultPropertyAttribute("Name")]
    public class Task
    {

        public enum TaskTypeId
        {
            GC,
            MC,
            T,
            ROI,
            XY,
            XY_XNA,
            Calibration,
            Validation,
            Test
        }

        private Hashtable map = new Hashtable();

        [CategoryAttribute("Value Settings"),
        DescriptionAttribute("Parameters")]
        public Hashtable Map
        {
            get { return map; }
            set { map = value; }
        }
        private string id;

        [CategoryAttribute("ID Settings"),
        DescriptionAttribute("Id"),ReadOnly(true)]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        [CategoryAttribute("ID Settings"),
        DescriptionAttribute("Task Name"), ReadOnly(true)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        private string description;

        [CategoryAttribute("ID Settings"),
        DescriptionAttribute("Task Description"), ReadOnly(true)]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private Form1 form = null;

        [CategoryAttribute("Debug"),
        DescriptionAttribute("Debug"), ReadOnly(true)]
        public Form1 Form
        {
            get {
                if ((form == null) || (form.IsDisposed))
                {
                    if (id.Equals(TaskTypeId.GC.ToString()))
                    {
                        form = new GCForm();
                        form.SetOwner(this);
                    } else if (id.Equals(TaskTypeId.MC.ToString()))
                    {
                        form = new MCForm();
                        form.SetOwner(this);
                    } else if (id.Equals(TaskTypeId.T.ToString())) {
                        form = new TForm();
                        form.SetOwner(this);
                    } else if (id.Equals(TaskTypeId.ROI.ToString())) {
                        form = new EVALabGC.ROI.Form();
                        form.SetOwner(this);
                    }
                    else if (id.Equals(TaskTypeId.XY.ToString()))
                    {
                        form = new EVALabGC.ROI.XYForm();
                        form.SetOwner(this);
                    }
                    else if (id.Equals(TaskTypeId.XY_XNA.ToString()))
                    {
                        form = new EVALabGC.Tasks.Forms.Xna.XnaForm();
                        form.SetOwner(this);
                    }
                    else if (id.Equals(TaskTypeId.Calibration.ToString()))
                    {
                        form = new EVALabGC.CalibrateForm();
                        form.SetOwner(this);
                    }
                    else if (id.Equals(TaskTypeId.Validation.ToString()))
                    {
                        form = new ValidationForm();
                        form.SetOwner(this);
                    }
                    else if (id.Equals(TaskTypeId.Test.ToString()))
                    {
                        form = new TestForm();
                        form.SetOwner(this);
                    } 
                }
                return form;
            }
        }

        private bool runnable = true;

        [CategoryAttribute("System"),
        DescriptionAttribute("Is a runnable task"), ReadOnly(true)]
        public bool Runnable
        {
            get { return runnable; }
            set { runnable = value; }
        }

        private bool storeData = true;

        [CategoryAttribute("System"),
        DescriptionAttribute("Data must be saved"), ReadOnly(true)]
        public bool StoreData
        {
            get { return storeData; }
            set { storeData = value; }
        }


        public Task(string id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }

        public Task(string id, string name, string description, Hashtable map)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.map = map;
        }

        public Task(string id, string name, string description, Hashtable map, bool runnable, bool storeData)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.map = map;
            this.runnable = runnable;
            this.storeData = storeData;
        }

        public string Get(string key)
        {
            return (string)map[key];
        }

        public void Set(string key, string value)
        {
            map.Add(key, value);
        }
    }
}
