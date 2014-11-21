using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace EvaLab.EOG.Test
{
    public partial class TestParallelForm : Form
    {
        private ParallelPort port = null;
        private ExperimentRunner runner = null;
        private IContext ctx = null;
        public TestParallelForm()
        {
            InitializeComponent();
        }

        public void SetContext(IContext ctx)
        {
            this.ctx = ctx;
            this.port = ctx.GetParallelPort();
            this.runner = ctx.GetExperimentRunner();
        }

        public static bool Show(IWin32Window owner, IContext ctx)
        {
            TestParallelForm form = new TestParallelForm();
            form.SetContext(ctx);
            DialogResult result = form.ShowDialog(owner);
            return result == DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            port.LedBar = false;
            for (int i = 0; i < 8; i++)
            {
                port.Reset();
                port.Show(0x1 << i);
                Thread.Sleep(1000);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            port.LedBar = true;
            for (int i = -207; i < 207; i++)
            {
                port.Show(i);
                Thread.Sleep(100);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            port.Reset();
        }

        //17
        private void button5_Click(object sender, EventArgs e)
        {
            port.LedBar = false;
            port.set_pin(0x200);
        }

        //14
        private void button4_Click(object sender, EventArgs e)
        {
            port.LedBar = false;
            port.set_pin(0x800);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            port.LedBar = false;
            port.Show(1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            runner.InitForTest(ctx);
            this.textBox1.Text = "Millisecond interval to wait " + runner.HotTestStart();
        }
    }
}
