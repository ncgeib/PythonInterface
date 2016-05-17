namespace SimplePythonGraph
{
    partial class SimplePythonGraphForm
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
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimplePythonGraphForm));
            this.ButtonPlot = new System.Windows.Forms.Button();
            this.TextPlotCode = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pyplot1 = new PythonInterface.pyplot();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonPlot
            // 
            this.ButtonPlot.AutoSize = true;
            this.ButtonPlot.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ButtonPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonPlot.Location = new System.Drawing.Point(0, 0);
            this.ButtonPlot.Name = "ButtonPlot";
            this.ButtonPlot.Padding = new System.Windows.Forms.Padding(10);
            this.ButtonPlot.Size = new System.Drawing.Size(286, 207);
            this.ButtonPlot.TabIndex = 1;
            this.ButtonPlot.Text = "Plot custom code";
            this.ButtonPlot.UseVisualStyleBackColor = true;
            this.ButtonPlot.Click += new System.EventHandler(this.button1_Click);
            // 
            // TextPlotCode
            // 
            this.TextPlotCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextPlotCode.Location = new System.Drawing.Point(0, 0);
            this.TextPlotCode.Multiline = true;
            this.TextPlotCode.Name = "TextPlotCode";
            this.TextPlotCode.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.TextPlotCode.Size = new System.Drawing.Size(366, 207);
            this.TextPlotCode.TabIndex = 2;
            this.TextPlotCode.Text = resources.GetString("TextPlotCode.Text");
            this.TextPlotCode.TextChanged += new System.EventHandler(this.TextPlotCode_TextChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(10, 308);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.TextPlotCode);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ButtonPlot);
            this.splitContainer1.Size = new System.Drawing.Size(662, 207);
            this.splitContainer1.SplitterDistance = 366;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 3;
            // 
            // pyplot1
            // 
            this.pyplot1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pyplot1.Location = new System.Drawing.Point(10, 10);
            this.pyplot1.Name = "pyplot1";
            this.pyplot1.Size = new System.Drawing.Size(662, 298);
            this.pyplot1.TabIndex = 0;
            // 
            // SimplePythonGraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 525);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pyplot1);
            this.Name = "SimplePythonGraphForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "SimplePythonGraph";
            this.Load += new System.EventHandler(this.SimplePythonGraphForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button ButtonPlot;
        private System.Windows.Forms.TextBox TextPlotCode;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private PythonInterface.pyplot pyplot1;
    }
}

