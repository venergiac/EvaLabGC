namespace EVALabGC.Tasks.Forms.Xna
{
    partial class XnaForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        protected override void InitializeComponent()
        {
            this.modelViewerControl1 = new EVALabGC.Tasks.Forms.Xna.ModelViewerControl();
            this.SuspendLayout();
            // 
            // modelViewerControl1
            // 
            this.modelViewerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelViewerControl1.Location = new System.Drawing.Point(0, 0);
            this.modelViewerControl1.Name = "modelViewerControl1";
            this.modelViewerControl1.Size = new System.Drawing.Size(594, 574);
            this.modelViewerControl1.TabIndex = 0;
            this.modelViewerControl1.Text = "modelViewerControl1";
            // 
            // XnaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 574);
            this.Controls.Add(this.modelViewerControl1);
            this.Name = "XnaForm";
            this.Text = "XnaForm";
            this.ResumeLayout(false);

        }

        #endregion

        private ModelViewerControl modelViewerControl1;
    }
}