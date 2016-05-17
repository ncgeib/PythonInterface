namespace PythonInterface
{
    partial class pyplot
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.contextPlot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.savePlotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.contextPlot.SuspendLayout();
            this.SuspendLayout();
            // 
            // PictureBox
            // 
            this.PictureBox.ContextMenuStrip = this.contextPlot;
            this.PictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBox.Location = new System.Drawing.Point(0, 0);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(249, 248);
            this.PictureBox.TabIndex = 0;
            this.PictureBox.TabStop = false;
            this.PictureBox.SizeChanged += new System.EventHandler(this.PictureBox_SizeChanged);
            // 
            // contextPlot
            // 
            this.contextPlot.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.savePlotToolStripMenuItem});
            this.contextPlot.Name = "contextPlot";
            this.contextPlot.Size = new System.Drawing.Size(123, 26);
            // 
            // savePlotToolStripMenuItem
            // 
            this.savePlotToolStripMenuItem.Name = "savePlotToolStripMenuItem";
            this.savePlotToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.savePlotToolStripMenuItem.Text = "Save Plot";
            this.savePlotToolStripMenuItem.Click += new System.EventHandler(this.savePlotToolStripMenuItem_Click);
            // 
            // pyplot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PictureBox);
            this.Name = "pyplot";
            this.Size = new System.Drawing.Size(249, 248);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.contextPlot.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.ContextMenuStrip contextPlot;
        private System.Windows.Forms.ToolStripMenuItem savePlotToolStripMenuItem;
    }
}
