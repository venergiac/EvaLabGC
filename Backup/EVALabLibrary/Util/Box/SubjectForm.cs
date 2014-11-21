using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EVALab.Util.Meta;

namespace EVALab.Util.Box
{
    public partial class SubjectForm : Form
    {

        public SubjectForm(string experiment)
        {
            this.Experiment = experiment;
            this.Name = experiment;
            InitializeComponent();
        }

        private ExperimentSettings experimentSettings = new ExperimentSettings();

        public ExperimentSettings ExperimentSettings
        {
            get { return experimentSettings; }
            set { experimentSettings = value; }
        }

        private string experiment;

        public string Experiment
        {
            set { experiment = value; }
        }

        public string SelectedPath
        {
            get { return textBoxDirectory.Text; }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            experimentSettings.Name = textBoxName.Text.ToUpper();
            experimentSettings.Surname = textBoxSurname.Text.ToUpper();
            string g = comboBoxSex.SelectedItem.ToString();
            experimentSettings.Gender = (g.Length>0) ? g[0] : 'X';
            experimentSettings.ExperimentName = experiment;
            experimentSettings.BirthDate = dateTimePickerBirth.Value;
            experimentSettings.ExperimentDate = dateTimePickerExperiment.Value;
            experimentSettings.Details = textBox3.Text;
            experimentSettings.Id = textBoxId.Text.ToUpper();
            experimentSettings.Distance = (float)numericUpDownDistance.Value;
            experimentSettings.Width = (float)numericUpDownWidth.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string name = textBoxName.Text.ToUpper();
            string surname = textBoxSurname.Text.ToUpper();

            textBoxId.Text = ((name.Length > 0) ? "" + name[0] : "X") +
                ((surname.Length > 0) ? ""+surname[0] : "X") + 
                "_" +
                this.dateTimePickerExperiment.Value.Day +
                this.dateTimePickerExperiment.Value.Month +
                this.dateTimePickerExperiment.Value.Year +
                this.dateTimePickerExperiment.Value.Hour +
                this.dateTimePickerExperiment.Value.Minute + 
                "_" + 
                experiment;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ExtendedFolderBrowser folder = new ExtendedFolderBrowser();
            if (folder.ShowDialog() != DialogResult.Cancel)
            {
                this.textBoxDirectory.Text = folder.SelectedPath;
                this.buttonOK.Enabled = true;
            }
        }

        private void SubjectForm_Load(object sender, EventArgs e)
        {
            comboBoxSex.SelectedIndex = 0;
        }
    }
}
