using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PythonInterface;

namespace SimplePythonGraph
{
    public partial class SimplePythonGraphForm : Form
    {

        PythonConnection Python;

        public SimplePythonGraphForm()
        {
            InitializeComponent();
            Python = new PythonConnection();
            Python.ImportScipyStack();
        }

        private void SimplePythonGraphForm_Load(object sender, EventArgs e)
        {
            pyplot1.Initialize(Python);
            double[] x = { 1.0, 2.0, 3.0, 4.0 };
            double[] y = { 1.0, 2.0, 1.0, 2.0 };
            pyplot1.plot(x, y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pyplot1.ClearPlot();
            pyplot1.BuildPlot(TextPlotCode.Text);
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TextPlotCode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
