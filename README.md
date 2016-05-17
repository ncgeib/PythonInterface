# PythonInterface
A light wrapper around Python.NET for embedding python in C#.

## Installation & Configuration
The project needs a working copy of Python.NET (which must be added as a reference to the project) and a working installation of python (with numpy and matplotlib).

## Usage
Example usage would be

    Python = new PythonConnection();
    Python.ImportScipyStack();

where it attempts to read the python directory either from the environment variable "WINPYTHON" which points to the python home or a working "PYTHONPATH". Otherwise provide the python home as an optional parameter to PythonConnection().
Then you can do stuff like this
    
    int a = 5;
    int b = 3;
    Python.Set("a", a);
    Python.Set("b", b);
    Python.RunString("c = a+b");
    int c = Python.Get("c", c);

where standard C# types (int, double, string) are converted to the corresponding python types. C# arrays are converted to numpy arrays and back

    double[] c = { 1, 2, 3 };
    Python.Set("c", c);
    Python.RunString("d = c+c**2");
    double[] d = Python.Get("d");
    
## Plotting
There is also a component that allows to show matplotlib plots in normal Windows Forms GUIs.

    pyplot1.Initialize(Python);
    double[] x = { 1.0, 2.0, 3.0, 4.0 };
    double[] y = { 1.0, 2.0, 1.0, 2.0 };
    pyplot1.plot(x, y);
    pyplot1.Clear();
    pyplot1.BuildPlotScript("myscript.py");
  
The python script myscript.py must create a matplotlib plot using the object oriented syntax and at the end of the script,
the figure instance must be stored in the variable "fig".

The component has to be initialized with an existing PythonConnection instance. Internally, however, it manages it own local dictionary, so no variables are overwritten while plotting.
The global dictionary (e.g. imports) is shared, but this should normally not be a big problem.

## Notes
The code in this project is not very sophisticated, merely how to achieve this using Python.NET is not well documented (in my opinion). You can probably write something much better and cleaner with little effort.
There were two pitfalls which you may avoid by looking at my implementation:

1. The conversion of multidimensional numpy arrays to C# arrays and back. Especially, ensure that the numpy arrays are contiguous.
2. Redraw a matplotlib plot repeatedly without having memory leaks. This requires careful usage of the OO interface and manual calls to the garbage collector (apparently). Look at the implementation for details.


