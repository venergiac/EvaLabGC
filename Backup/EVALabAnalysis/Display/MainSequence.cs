using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EVALab.Analysis.Saccade;
using ZedGraph;

namespace EVALabAnalysis.Display
{
    public partial class MainSequence : UserControl
    {

        private double vMax = 500;
        private double c = 10.0;

        private int colorIdx = 0;
        private Color[] colors = { Color.Red, Color.Black, Color.Yellow, Color.Violet, Color.Turquoise, Color.Blue, Color.Green, Color.Pink };

        public MainSequence()
        {
            InitializeComponent();
        }

        public void AddSaccades(string name, ref SaccadeList list)
        {
            if (list == null) return;

            // Get a reference to the GraphPane
            GraphPane myPane = zedGraphControl1.GraphPane;

            PointPairList curve =  new PointPairList();

            //Plot data
            foreach (Saccade item in list.List)
            {
                curve.Add(item.Amplitude, item.VPeak);
            }

            // Add the curve
            LineItem myCurve = myPane.AddCurve(name, curve, colors[colorIdx % colors.Length], SymbolType.Circle);
            // Don't display the line (This makes a scatter plot)
            myCurve.Line.IsVisible = false;
            // Hide the symbol outline
            myCurve.Symbol.Border.IsVisible = true;
            // Fill the symbol interior with color
            myCurve.Symbol.Fill = new Fill(colors[colorIdx%colors.Length]);
            myCurve.Symbol.Fill.Type = FillType.Solid;
            colorIdx++;

            //add to list checkBox
            int idx = checkedListBox1.Items.Add(list.Name);
            checkedListBox1.SetItemChecked(idx, true);

            //add custom label
            zedGraphControl1.IsShowPointValues = true;
            zedGraphControl1.PointValueEvent += new ZedGraphControl.PointValueHandler(MyPointValueHandler);

            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void InitReference()
        {
            //Vp=Vmax(1-e –a/c)

            // Get a reference to the GraphPane
            GraphPane myPane = zedGraphControl1.GraphPane;
            myPane.CurveList.Clear();
            zedGraphControl1.IsShowHScrollBar = true;
            zedGraphControl1.IsShowVScrollBar = true;

            myPane.XAxis.Title.Text = "Amplitude";
            myPane.YAxis.Title.Text = "Velocity Peak";

            myPane.Title.Text = "Main Sequence";

            PointPairList healthy = new PointPairList();

            //Plot data
            for (double x=-40.0; x<=40.0; x+=0.1)
            {
                double y = vMax * (1.0 - Math.Exp(-Math.Abs( x ) / c));
                healthy.Add(x, y);
            }

            LineItem line = myPane.AddCurve("Healthy subjects", healthy, Color.Black, SymbolType.None);
 
            //add custom label
            zedGraphControl1.IsShowPointValues = true;
            zedGraphControl1.PointValueEvent += new ZedGraphControl.PointValueHandler(MyPointValueHandler);

            //Redraw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        /// <summary>
        /// Display customized tooltips when the mouse hovers over a point
        /// </summary>
        private string MyPointValueHandler(ZedGraphControl control, GraphPane pane,
                        CurveItem curve, int iPt)
        {
            // Get the PointPair that is under the mouse
            PointPair pt = curve[iPt];

            return curve.Label.Text + " has Vpeak " + pt.Y.ToString("f2") + " at " + pt.X.ToString("f2") + " Degree";
        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {
           
        }

        private void MainSequence_Load(object sender, EventArgs e)
        {
            vMax = (double)Properties.Settings.Default["vMax"];
            c = (double)Properties.Settings.Default["c"];
            InitReference();
        }

        private void checkedListBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

    }
}
