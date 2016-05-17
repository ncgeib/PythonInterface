using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using Python.Runtime;

namespace PythonInterface
{
    /// <summary>
    /// This control is a simple matplotlib plotting interface.
    /// 
    /// It needs a valid python connection to work and manages a local
    /// python environment (the local dictionary, not the global one).
    /// Graphs are plotted in memory and passed back as an array. This approach
    /// is reasonably fast but is probably not suitable for real-time plotting.
    /// </summary>
    public partial class pyplot : UserControl
    {

        PythonConnection Python;

        /// <summary>
        /// This local dictionary is used for plotting non-interactively on the control.
        /// </summary>
        public PyDict Local;

        bool IsInitialized = false;

        int SaveDpi = 300;

        public pyplot()
        {
            InitializeComponent();
        }

        public void Initialize(PythonConnection python)
        {
            this.Python = python;
            this.Python.NewEnvironment();

            string CurrentDirectory = Directory.GetCurrentDirectory();
            string FileLocation = Path.GetFullPath(Path.Combine(CurrentDirectory, "pyplot.mplstyle"));
            using (Stream newFile = new FileStream(FileLocation, FileMode.Create))
            {
                System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("PythonInterface.pyplot.mplstyle").CopyTo(newFile);
            }

            if (!this.Python.MatplotlibLoaded)
            {
                this.Python.ImportMatplotlib();
                this.Python.RunString("plt.style.use('pyplot.mplstyle')");
            }

            Local = this.Python.PopLocal();
            IsInitialized = true;
            InvalidateFigsize();
        }

        private void InvalidateFigsize()
        {
            if (!IsInitialized)
                return;
            Graphics g = this.CreateGraphics();
            double dpix = 96.0;
            double dpiy = 96.0;
            try
            {
                dpix = g.DpiX;
                dpiy = g.DpiY;
            }
            finally
            {
                g.Dispose();
            }
            double width = this.Width / dpix;
            double height = this.Height / dpiy;
            double dpi = (dpix + dpiy) / 2.0;

            Python.PushLocal(Local);
            Python.Set("__figwidth__", width);
            Python.Set("__figheight__", height);
            Python.Set("__dpi__", dpi);
            Local = Python.PopLocal();
        }

        public void plot(double[] x, double[] y)
        {
            Python.PushLocal(Local);
            Python.Set("x", x);
            Python.Set("y", y);
            Python.RunString(
                "fig = plt.figure()\n" +
                "ax = fig.add_subplot(111)\n" +
                "ax.plot(x, y)\n" +
                "del x\n" +
                "del y\n"
                );
            Local = Python.PopLocal();
            UpdatePlot();
        }

        /// <summary>
        /// Can be used to create a plot from python code.
        /// Only the object oriented interface is supported. This means that you create a figure instance
        /// with fig = plt.figure() and work on this. 
        /// Variables have to be set via pyplot.Set() beforehand.
        /// </summary>
        /// <param name="code"></param>
        public void BuildPlot(string code)
        {
            RunString(code);            
            UpdatePlot();
        }

        public void BuildPlotTry(string code)
        {
            RunStringTry(code);
            UpdatePlot();
        }

        public void BuildPlotScript(string filename)
        {
            BuildPlot(File.ReadAllText(filename));
        }

        public void BuildPlotScriptTry(string filename)
        {
            BuildPlotTry(File.ReadAllText(filename));
        }

        public void ClearPlot()
        {
            using (Py.GIL())
            {
                if (Local.HasKey("fig"))
                {
                    Python.PushLocal(Local);
                    Python.RunString(
                        "fig.clear()\n" +
                        "plt.close(fig)\n" +
                        "del fig\n" +
                        "gc.collect()"
                    );
                    Local = Python.PopLocal();
                }
            }
        }

        public void RunString(string code)
        {
            Python.PushLocal(Local);
            Python.RunString(code);
            Local = Python.PopLocal();
        }

        public void RunStringTry(string code)
        {
            Python.PushLocal(Local);
            Python.RunStringTry(code);
            Local = Python.PopLocal();
        }

        public void Set(string name, dynamic content)
        {
            Python.PushLocal(Local);
            Python.Set(name, content);
            Local = Python.PopLocal();
        }

        /// <summary>
        /// Plots the figure and shows it.
        /// Assumes that there is a local 
        /// </summary>
        public void UpdatePlot()
        {
            using (Py.GIL())
            {
                if (Local.HasKey("fig"))
                {
                    Python.PushLocal(Local);
                    Python.RunString(
                        "fig.set_size_inches(__figwidth__, __figheight__)" + Environment.NewLine +
                        "fig.tight_layout()" + Environment.NewLine +
                        "buf = io.BytesIO()" + Environment.NewLine +
                        "fig.savefig(buf, dpi=__dpi__, format='png')" + Environment.NewLine +
                        "buf_out = np.array(buf.getbuffer(), dtype = np.uint8)"
                    );
                    byte[] buf = Python.Get("buf_out");
                    Python.RunString(
                        "del buf_out\n" +
                        "del buf"
                    );
                    Local = Python.PopLocal();
                    Stream stream = new MemoryStream(buf);
                    Image image = Image.FromStream(stream);
                    PictureBox.Image = image;
                }
            }            
        }

        public void SavePlot()
        {
            using (Py.GIL())
            {
                if (Local.HasKey("fig"))
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                    saveFileDialog1.Filter = "image files (*.png)|*.png";
                    saveFileDialog1.FilterIndex = 1;
                    saveFileDialog1.RestoreDirectory = true;

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        Python.PushLocal(Local);
                        Python.Set("__filename__", saveFileDialog1.FileName);
                        Python.Set("__savedpi__", SaveDpi);
                        Python.RunString(
                            "fig.set_size_inches(__figwidth__, __figheight__)" + Environment.NewLine +
                            "fig.tight_layout()" + Environment.NewLine +
                            "fig.savefig(__filename__, dpi=__savedpi__, format='png')"
                        );
                        Local = Python.PopLocal();
                    }
                }
            }
        }

        private void PictureBox_SizeChanged(object sender, EventArgs e)
        {
            if (IsInitialized)
            {
                InvalidateFigsize();
                UpdatePlot();
            }
        }

        private void savePlotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsInitialized)
                SavePlot();
        }
    }
}
