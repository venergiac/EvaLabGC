using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using User;

namespace ChartTest
{
	public class ShareChart : Chart
	{
		private ArrayList date_array;

		public ShareChart()
		{
			date_array = new ArrayList();
		}

		public void AddDate(string date)
		{
			date_array.Add(date);
		}

		
		public override string TranslateXValue(float val, int disp_precision)
		{
			if (Math.Round(val, disp_precision) == (double) ((int) val))
			{
				if (val >= 0 && val < date_array.Count)
				{
					return (string) date_array[date_array.Count - (int) val - 1];
				}
			}

			// Otherwise return an empty string, so no reference value is
			// displayed
			return "";
		}
		
	}

	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class ChartTest : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button m_btnLeft;
		private System.Windows.Forms.Button m_btnUp;
		private System.Windows.Forms.Button m_btnDown;
		private System.Windows.Forms.Button m_btnRight;
		private System.Windows.Forms.Button m_btnZoomIn;
		private System.Windows.Forms.Button m_btnZoomOut;
		private Chart m_chart;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.Button btnToggle;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ChartTest()
		{
			InitializeComponent();

			// Add a few control buttons
			m_btnZoomIn = AddButton("../../zoom-in.bmp", 6);
			m_btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);

			m_btnZoomOut = AddButton("../../zoom-out.bmp", 5);
			m_btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);

			m_btnLeft = AddButton("../../left.bmp", 3);
			m_btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
			
			m_btnUp = AddButton("../../up.bmp", 2);
			m_btnUp.Click += new System.EventHandler(this.btnUp_Click);
			
			m_btnDown = AddButton("../../down.bmp", 1);
			m_btnDown.Click += new System.EventHandler(this.btnDown_Click);
			
			m_btnRight = AddButton("../../right.bmp", 0);
			m_btnRight.Click += new System.EventHandler(this.btnRight_Click);
			
			// Create the chart itself; default to values rather than dates
			CreateChart(false);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnReset = new System.Windows.Forms.Button();
			this.btnToggle = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnReset
			// 
			this.btnReset.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.btnReset.Location = new System.Drawing.Point(8, 346);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(48, 23);
			this.btnReset.TabIndex = 0;
			this.btnReset.Text = "Reset";
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// btnToggle
			// 
			this.btnToggle.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.btnToggle.Location = new System.Drawing.Point(64, 346);
			this.btnToggle.Name = "btnToggle";
			this.btnToggle.Size = new System.Drawing.Size(48, 23);
			this.btnToggle.TabIndex = 1;
			this.btnToggle.Text = "Dates";
			this.btnToggle.Click += new System.EventHandler(this.btnToggle_Click);
			// 
			// ChartTest
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(656, 373);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnToggle,
																		  this.btnReset});
			this.Name = "ChartTest";
			this.Text = "Demonstration Share Price Chart (MSFT)";
			this.Resize += new System.EventHandler(this.Form1_Resize);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new ChartTest());
		}

		private Button AddButton(string str_image, int index)
		{
			Button button = new Button();
			button.Text = "";
			Image image = Image.FromFile(str_image);
			button.Width = 20;
			button.Height = 20;
			button.Image = image;
			
			// Inset the button from the bottom right corner of the form
			button.Left = Right - 32 - (index * 24);
			button.Top = Bottom - 50;
			button.FlatStyle = FlatStyle.Popup;
			
			// When you resize the form, make sure the button adjusts
			// its position accordingly
			button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		
			// Add it to the form
			Controls.Add(button);
			return button;
		}

		private PointF[] LoadShare(string str_share)
		{
			// Open the share price file, and read the closing prices
			StreamReader reader = File.OpenText(str_share);
			ArrayList price_array = new ArrayList();			
			int day_index = 0;

			// Consume the opening line
			reader.ReadLine();

			while (true)
			{
				string str_line = reader.ReadLine();

				if (str_line == "")
					break;

				if (str_line == null)
					break;

				Regex reg = new Regex(",");
				string[] term_array = str_line.Split(',');
				
				// Extract the seventh term from every line; skip the rest
				// This is the daily closing price, adjusted for splits
				price_array.Add(new PointF((float) day_index, (float) 
					Double.Parse(term_array[6])));
				
				if (m_chart.ToString() == "ChartTest.ShareChart")
					((ShareChart) m_chart).AddDate(term_array[0]);

				day_index ++;
			}
			
			reader.Close();
		
			// Convert the ArrayList into a PointF[] array
			PointF[] point_array = new PointF[price_array.Count];
			price_array.CopyTo(point_array);
			
			return point_array;
		}

		private PointF[] CalcMovingAverage(PointF[] study, int days)
		{
			// Calculate a moving average for the specified share
			PointF[] ma_array = new PointF[study.Length - days];

			for (int i = 0; i < study.Length - days; i++)
			{
				float running_count = 0;

				for (int j = i; j < i + days; j ++)
					running_count += study[j].Y;

				// The first MA entry we can calculate will be at (days-1)
				ma_array[i].X = i + days - 1;
				ma_array[i].Y = running_count / days;
			}
		
			return ma_array;
		}

		private void CreateChart(bool use_dates)
		{
			// Create a new chart
			m_chart = use_dates ? new ShareChart() : new Chart();
						
			// Load some share information
			PointF[] shareprice_array = LoadShare("../../msft.txt");
			
			// Set a few general layout preferences for the chart
			// as a whole
			m_chart.Margin = 50;
			m_chart.Font = new Font("Courier", 10);
			m_chart.XLabel = use_dates ? "Trading Date" :
				"Trading Days since November 25 2002";
			m_chart.YLabel = "Share Price ($)";
			
			// Create a new study from the share price array; the chart's
			// default behaviour is to treat this as the primary study
			Chart.Study primary_study = m_chart.AddStudy(shareprice_array);
			
			// Set the study's display preferences
			primary_study.StudyPen = new Pen(Color.Black, 1);
						
			// Calculate the share's 9 day moving average
			PointF[] ma_array = CalcMovingAverage(shareprice_array, 9);
			
			// Add a secondary study created from the moving average
			Chart.Study secondary_study = m_chart.AddStudy(ma_array);
			
			// Set a few display preferences specific to this study
			secondary_study.StudyPen = new Pen(Color.Blue, 2);
			secondary_study.StudyPen.DashStyle = DashStyle.Dot;
			
			// Start the chart zoomed in 20%
			m_chart.Zoom(20);
		}

		private void Draw()
		{
			// Don't allow user to scroll beyond chart bounds
			m_btnLeft.Enabled = !m_chart.AtMinX();
			m_btnRight.Enabled = !m_chart.AtMaxX();
			m_btnUp.Enabled = !m_chart.AtMinY();
			m_btnDown.Enabled = !m_chart.AtMaxY();
	
			m_chart.Draw(this);
		}

		private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Draw();
		}

		private RectangleF Offset(RectangleF rect, float x, float y)
		{
			rect.Offset(x, y);
			return rect;
		}

		private SizeF GetMoveStep()
		{
			// Movement step = 1/20th window size
			SizeF size = new SizeF(Width/20, Height/20);
			return m_chart.Transform(m_chart.ScreenRect, m_chart.DataRect, size);
		}

		private void btnLeft_Click(object sender, System.EventArgs e)
		{
			m_chart.DataRect = Offset(m_chart.DataRect, -GetMoveStep().Width, 0);
			Draw();
		}

		private void btnRight_Click(object sender, System.EventArgs e)
		{
			m_chart.DataRect = Offset(m_chart.DataRect, GetMoveStep().Width, 0);
			Draw();
		}
		
		private void btnUp_Click(object sender, System.EventArgs e)
		{
			m_chart.DataRect = Offset(m_chart.DataRect, 0, GetMoveStep().Height);
			Draw();
		}

		private void btnDown_Click(object sender, System.EventArgs e)
		{
			m_chart.DataRect = Offset(m_chart.DataRect, 0, -GetMoveStep().Height);
			Draw();
		}

		private void btnZoomIn_Click(object sender, System.EventArgs e)
		{
			m_chart.Zoom(10);
			Draw();
		}

		private void btnZoomOut_Click(object sender, System.EventArgs e)
		{
			m_chart.Zoom(-10);
			Draw();
		}

		private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (m_chart.ScreenRect.Contains(e.X, e.Y))
			{
				m_chart.Snap(new PointF(e.X, e.Y));
				Draw();
			}
		}

		private void Form1_Resize(object sender, System.EventArgs e)
		{
			Draw();
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			m_chart.Reset();
			Draw();
		}

		private void btnToggle_Click(object sender, System.EventArgs e)
		{
			CreateChart(btnToggle.Text == "Dates");
			btnToggle.Text = btnToggle.Text == "Dates" ? "Values" : "Dates";
			Draw();
		}
	}	
}
