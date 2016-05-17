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


namespace PythonInterfaceConnection
{
    public partial class PythonInterfaceConnection : Form
    {
        int a = 5;
        int b = 3;
        double[] c = { 1, 2, 3 };
        double[,] d = { { 1, 2, 3 }, { 4, 5, 6 } };

        PythonConnection Python;

        public PythonInterfaceConnection()
        {
            InitializeComponent();
            Python = new PythonConnection();
            Python.ImportScipyStack();
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            Python.Set("a", a);
            Python.Set("b", b);
            Python.Set("c", c);
            Python.Set("d", d);
            Python.RunString(textCode.Text);
            textOutput.Text = "a = " + Print(Python.Get("a")) + Environment.NewLine;
            textOutput.Text += "b = " + Print(Python.Get("b")) + Environment.NewLine;
            textOutput.Text += "c = " + Print(Python.Get("c")) + Environment.NewLine;
            textOutput.Text += "d = " + Print(Python.Get("d")) + Environment.NewLine;
        }

        private string Print(dynamic data)
        {
            System.Type DataType = data.GetType();
            if (DataType.IsArray)
            {
                string result = "[";
                if (data.Rank == 1)
                {
                    foreach (object d in data)
                    {
                        result += Convert.ToString(d) + ", ";
                    }
                }
                if (data.Rank == 2)
                {
                    result += Environment.NewLine;
                    for (int i=0; i<data.GetLength(0); i++)
                    {
                        result += "  [";
                        for (int j=0; j<data.GetLength(1); j++)
                        {
                            result += Convert.ToString(data[i,j]) + ", ";
                        }
                        result += "]" + Environment.NewLine;
                    }
                }
                result += "]";
                return result;
            } else
            {
                return Convert.ToString(data);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void buttonPrintLocal_Click(object sender, EventArgs e)
        {
            textLocalDictionary.Text = "";
            foreach (string k in Python.GetLocalNames())
            {
                textLocalDictionary.Text += k + " = " + Print(Python.Get(k)) + Environment.NewLine;
            }
        }

        private void buttonClearLocal_Click(object sender, EventArgs e)
        {
            Python.ClearLocals();
        }

        private void buttonRelativePath_Click(object sender, EventArgs e)
        {           
            try
            {
                Python.AddRelativePath(textRelativePath.Text);
            }
            catch (Exception p)
            {
                MessageBox.Show(p.Message, "Error");
            }
        }

        private void buttonLoadModule_Click(object sender, EventArgs e)
        {
            try
            {
                Python.Import(textLoadModule.Text);
            } catch (Exception p)
            {
                MessageBox.Show(p.Message, "Error");
            }
        }

        private void buttonRunScript_Click(object sender, EventArgs e)
        {
            Python.RunString(textScriptModule.Text);
        }

        private void ButtonProfile_Click(object sender, EventArgs e)
        {
            double[,] arr = new double[1000, 1000];
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Python.Set("a", arr);
            sw.Stop();
            string settime = Convert.ToString(sw.ElapsedMilliseconds) + "ms";
            sw.Reset();
            sw.Start();
            double[,] arr_out = Python.Get("a");
            sw.Stop();
            string gettime = Convert.ToString(sw.ElapsedMilliseconds) + "ms";

            LabelProfile.Text = "1000x1000 array copy" + Environment.NewLine +
                "in: " + settime + Environment.NewLine +
                "out: " + gettime + Environment.NewLine;

        }
    }
}
