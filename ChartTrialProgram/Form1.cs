using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ChartTrialProgram
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
    private ChartControl.ChartControlForm chartControlForm1;
    private System.Windows.Forms.Button AddValueButton;
    private System.Windows.Forms.TextBox ValueTextBox;
    private Button BarButton;
    private Button GraphButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

      chartControlForm1.InitChart();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
      this.AddValueButton = new System.Windows.Forms.Button();
      this.ValueTextBox = new System.Windows.Forms.TextBox();
      this.BarButton = new System.Windows.Forms.Button();
      this.GraphButton = new System.Windows.Forms.Button();
      this.chartControlForm1 = new ChartControl.ChartControlForm();
      this.SuspendLayout();
      // 
      // AddValueButton
      // 
      this.AddValueButton.Location = new System.Drawing.Point(163, 136);
      this.AddValueButton.Name = "AddValueButton";
      this.AddValueButton.Size = new System.Drawing.Size(64, 23);
      this.AddValueButton.TabIndex = 2;
      this.AddValueButton.Text = "Add Value";
      this.AddValueButton.Click += new System.EventHandler(this.AddValueButton_Click);
      // 
      // ValueTextBox
      // 
      this.ValueTextBox.Location = new System.Drawing.Point(175, 110);
      this.ValueTextBox.Name = "ValueTextBox";
      this.ValueTextBox.Size = new System.Drawing.Size(40, 20);
      this.ValueTextBox.TabIndex = 3;
      // 
      // BarButton
      // 
      this.BarButton.Location = new System.Drawing.Point(75, 128);
      this.BarButton.Name = "BarButton";
      this.BarButton.Size = new System.Drawing.Size(53, 23);
      this.BarButton.TabIndex = 4;
      this.BarButton.Text = "Bar";
      this.BarButton.UseVisualStyleBackColor = true;
      this.BarButton.Click += new System.EventHandler(this.BarButton_Click);
      // 
      // GraphButton
      // 
      this.GraphButton.Location = new System.Drawing.Point(16, 128);
      this.GraphButton.Name = "GraphButton";
      this.GraphButton.Size = new System.Drawing.Size(53, 23);
      this.GraphButton.TabIndex = 5;
      this.GraphButton.Text = "Graph";
      this.GraphButton.UseVisualStyleBackColor = true;
      this.GraphButton.Click += new System.EventHandler(this.GraphButton_Click);
      // 
      // chartControlForm1
      // 
      this.chartControlForm1.Location = new System.Drawing.Point(16, 16);
      this.chartControlForm1.Name = "chartControlForm1";
      this.chartControlForm1.Size = new System.Drawing.Size(247, 88);
      this.chartControlForm1.TabIndex = 0;
      this.chartControlForm1.Load += new System.EventHandler(this.chartControlForm1_Load);
      // 
      // Form1
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(268, 165);
      this.Controls.Add(this.GraphButton);
      this.Controls.Add(this.BarButton);
      this.Controls.Add(this.ValueTextBox);
      this.Controls.Add(this.AddValueButton);
      this.Controls.Add(this.chartControlForm1);
      this.Name = "Form1";
      this.Text = "Graph/Chart Form";
      this.Click += new System.EventHandler(this.Form1_Click);
      this.Load += new System.EventHandler(this.Form1_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

    private void Form1_Load(object sender, System.EventArgs e)
    {
    }

    private void chartControlForm1_Load(object sender, System.EventArgs e)
    {
    
    }

    private void Form1_Click(object sender, System.EventArgs e)
    {
       
    }

    private void button1_Click(object sender, System.EventArgs e)
    {
      chartControlForm1.InitChart();
    }

    private void AddValueButton_Click(object sender, System.EventArgs e)
    {
      double ValueAdd;
      try
      {
        ValueAdd = Convert.ToDouble(ValueTextBox.Text);
      }
      catch
      {
        return;
      }
      chartControlForm1.AddValue((float)ValueAdd);
    }

    private void BarButton_Click(object sender, EventArgs e)
    {
      chartControlForm1.OpenType = ChartControl.ChartControlOpenType.Bar;
      chartControlForm1.RefreshControl();
    }

    private void GraphButton_Click(object sender, EventArgs e)
    {
      chartControlForm1.OpenType = ChartControl.ChartControlOpenType.Graph;
      chartControlForm1.RefreshControl();
    }
	}
}
