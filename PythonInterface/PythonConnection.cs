using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using System.IO; // we need IO for file path manipulations
using Python.Runtime; // this is the .NET wrapper for python

namespace PythonInterface
{
    public class PythonException : System.ApplicationException
    {
        public PythonException() { }
        public PythonException(string message) { }
        public PythonException(string message, System.Exception inner) { }

        // Constructor needed for serialization 
        // when exception propagates from a remoting server to the client.
        protected PythonException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }

    public class PythonConnection
    {
        /// <summary>
        /// The path to the base directory of a full python distribution (e.g. WinPython).
        /// This is static as one process can only obtain one embedded python interpreter.
        /// </summary>
        /// <param name="PyHome"></param>
        static string PythonHome;

        /// <summary>
        /// The main modules which are loaded after initialization of the interpeter.
        /// </summary>
        static dynamic Main = null;

        /// <summary>
        /// Additional paths which are searched for modules.
        /// </summary>
        private List<string> AdditionalPaths = new List<string>();

        /// <summary>
        /// The whole paths which are searched for modules.
        /// </summary>
        private List<string> Pythonpath;

        /// <summary>
        /// The local and global dictionaries are stacks. In that way you can switch between the environments
        /// transparantly. However, currently only local changing is allowed.
        /// </summary>
        private Stack<PyDict> Locals = new Stack<PyDict>();
        private Stack<PyDict> Globals = new Stack<PyDict>();

        /// <summary>
        /// The reference to the numpy package if it is loaded.
        /// </summary>
        dynamic np = null;

        /// <summary>
        /// The reference to the scipy package if it is loaded.
        /// </summary>
        dynamic scipy = null;

        /// <summary>
        /// The reference to the sys package.
        /// </summary>
        dynamic sys = null;

        /// <summary>
        /// Signals if the numpy package is loaded.
        /// </summary>
        public bool NumpyLoaded = false;

        /// <summary>
        /// Signals if the scipy package is loaded.
        /// </summary>
        public bool ScipyLoaded = false;

        public bool MatplotlibLoaded = false;

        /// <summary>
        /// Initializes a python connection. If you leave the parameter empty you have to set the environment
        /// variable WINPYTHON.
        /// </summary>
        /// <param name="path">The location of the python home folder of a full python distribution (including numpy, maptlotlib, etc.)</param>
        public PythonConnection(string path="")
        {
            if (!PythonEngine.IsInitialized)
            {
                if (path == "")
                {
                    PythonHome = System.Environment.GetEnvironmentVariable("WINPYTHON", System.EnvironmentVariableTarget.Machine);
                    if (PythonHome == null)
                        PythonHome = System.Environment.GetEnvironmentVariable("WINPYTHON", System.EnvironmentVariableTarget.User);
                    if (PythonHome == null)
                        PythonHome = System.Environment.GetEnvironmentVariable("WINPYTHON", System.EnvironmentVariableTarget.Process);
                    if (PythonHome == null)
                        PythonHome = "";
                }
                else
                {
                    PythonHome = path;
                }
                BuildPythonpath(false);
                System.Environment.SetEnvironmentVariable("PYTHONPATH", string.Join(";", Pythonpath));
                PythonEngine.PythonHome = PythonHome;
                PythonEngine.ProgramName = "PythonRuntime";
                PythonEngine.Initialize();
                PythonEngine.BeginAllowThreads();
            }

            using (Py.GIL())
            {
                if (Main == null)
                    Main = Py.Import("__main__");
                Globals.Push(new PyDict(Main.GetAttr("__dict__")));
                Locals.Push(new PyDict());
                sys = Import("sys");
                Import("copy");
                Import("io");
                Import("gc");
            }
        }

        //~PythonConnection() {
        //    PythonEngine.Shutdown();
        //}

        public PyDict Local
        {
            get {
                return Locals.Peek();
            }
        }

        private PyDict Global
        {
            get
            {
                return Globals.Peek();
            }
        }

        /// <summary>
        /// Allows to set variables in the python context. Allowed types are simple types (int, double, string, etc.) and 
        /// arrays of those types.
        /// Numpy must be loaded before trying to pass arrays!
        /// 
        /// TODO: Implement Marshal.Copy for input of arrays...
        /// </summary>
        /// <param name="name"></param>
        /// <param name="content"></param>
        public void Set(string name, dynamic content)
        {
            using (Py.GIL())
            {
                System.Type ValueType = content.GetType();
                // Arrays are always converted to python lists
                if (ValueType.IsArray)
                {
                    if (!NumpyLoaded)
                    {
                        throw new PythonException("numpy must be loaded for array conversion.");
                    }

                    // BlockCopy possibly multidimensional array of arbitrary type to onedimensional byte array
                    System.Type ElementType = ValueType.GetElementType();
                    int nbytes = content.Length * Marshal.SizeOf(ElementType);
                    byte[] data = new byte[nbytes];
                    System.Buffer.BlockCopy(content, 0, data, 0, nbytes);

                    // Create an python tuple with the dimensions of the input array
                    PyObject[] lengths = new PyObject[content.Rank];
                    for (int i = 0; i < content.Rank; i++)
                        lengths[i] = new PyInt(content.GetLength(i));
                    PyTuple shape = new PyTuple(lengths);

                    // Create an empty numpy array in correct shape and datatype
                    dynamic dtype;
                    if (ElementType == typeof(int))
                        dtype = np.int32;
                    else if (ElementType == typeof(double))
                        dtype = np.float64;
                    else
                        throw new System.Exception("Datatype not supported!");
                    dynamic pydata = np.empty(shape, dtype);

                    // Copy the data to that array
                    System.IntPtr ptr = (System.IntPtr)PyInt.AsInt(pydata.__array_interface__["data"][0]).ToInt32();
                    Marshal.Copy(data, 0, ptr, nbytes);

                    // Push the variable to local dictionary
                    UpdateLocals(name, pydata);
                }
                else
                {
                    if (ValueType == typeof(int))
                        UpdateLocals(name, new PyInt(content));
                    else if (ValueType == typeof(string))
                        UpdateLocals(name, new PyString(content));
                    else if (ValueType == typeof(double))
                        UpdateLocals(name, new PyFloat(content));
                    else if (ValueType == typeof(bool))
                    {
                        UpdateLocals(name, new PyInt(System.Convert.ToInt32(content)));
                        RunStringTry(name + " = (" + name + " == 1)");
                    }
                    else
                        throw new PythonException("Datatype not supported!");
                }
            }
        }

        public dynamic Get(string name_in)
        {
            using (Py.GIL())
            {
                dynamic result;
                string name = name_in;

                RunString("__" + name + "__type = str(type(" + name + "))");
                string type = (string)Local.GetItem("__" + name + "__type").AsManagedObject(typeof(string));
                if (type == "<class 'numpy.ndarray'>")
                {
                    // If the array is not contiguous in memory, copy it first.
                    // This overwrites the array (but obviously the contents stay the same).
                    RunString(
                        "if (" + name + ".__array_interface__['strides'] is not None):\n" +
                        "    " + name + " = copy.deepcopy(" + name + ")"
                    );
                    RunString("__" + name + "__ptr = " + name + ".__array_interface__['data'][0]");
                    RunString("__" + name + "__nbytes = " + name + ".nbytes");
                    RunString("__" + name + "__shape = " + name + ".shape");
                    RunString("__" + name + "__dtype = str(" + name + ".dtype)");
                    System.IntPtr ptr = (System.IntPtr)PyInt.AsInt(Local.GetItem("__" + name + "__ptr")).ToInt32();
                    int nbytes = PyInt.AsInt(Local.GetItem("__" + name + "__nbytes")).ToInt32();
                    string dtype = (string)Local.GetItem("__" + name + "__dtype").AsManagedObject(typeof(string));
                    long[] shape = (long[])Local.GetItem("__" + name + "__shape").AsManagedObject(typeof(long[]));

                    byte[] data = new byte[nbytes];
                    Marshal.Copy(ptr, data, 0, nbytes);

                    System.Type result_type;
                    if (dtype == "float64")
                        result_type = typeof(double);
                    else if (dtype == "int32")
                        result_type = typeof(int);
                    else if (dtype == "int64")
                        result_type = typeof(long);
                    else if (dtype == "uint8")
                        result_type = typeof(byte);
                    else
                        throw new PythonException("type not supported!");
                    result = System.Array.CreateInstance(result_type, shape);
                    System.Buffer.BlockCopy(data, 0, result, 0, nbytes);

                    RunString("del __" + name + "__ptr");
                    RunString("del __" + name + "__nbytes");
                    RunString("del __" + name + "__dtype");
                    RunString("del __" + name + "__shape");
                }
                else
                {
                    System.Type result_type;
                    if (type == "<class 'float'>")
                        result_type = typeof(double);
                    else if (type == "<class 'str'>")
                        result_type = typeof(string);
                    else if (type == "<class 'int'>")
                        result_type = typeof(long); // on a 64bit-system just "int" does not work!
                    else
                        throw new PythonException("type not supported!");
                    result = Local.GetItem(name).AsManagedObject(result_type);
                }

                RunString("del __" + name + "__type");
                return result;
            }
            
        }

        private PyList ArrayToPyList(dynamic arr)
        {
            using (Py.GIL())
            {
                System.Type NestedType = arr.GetType().GetElementType();
                PyList res = new PyList();
                if (NestedType == typeof(int))
                {
                    foreach (int d in arr)
                    {
                        res.Append(d.ToPython());
                    }
                }
                else if (NestedType == typeof(double))
                {
                    foreach (double d in arr)
                    {
                        res.Append(d.ToPython());
                    }
                }
                else
                {
                    throw new PythonException("Nested Datatype not supported!");
                }

                return res;
            }
        }

        public void ClearLocals()
        {
            using (Py.GIL())
                Local.Clear();
        }

        public string[] GetLocalNames()
        {
            using (Py.GIL())
            {
                List<string> keys = new List<string>();
                foreach (PyObject k in Local.Keys())
                {
                    keys.Add((string)k.AsManagedObject(typeof(string)));
                }
                return keys.ToArray();
            }
        }

        private void UpdateLocals(string name, dynamic item)
        {
            using (Py.GIL())
            {
                PyDict update = new PyDict();
                update.SetItem(name, item);
                Local.Update(update);
            }
        }

        public void RunString(string code)
        {
            using (Py.GIL()) {
                PyObject ret = PythonEngine.RunString(code, Global.Handle, Local.Handle);
                if (ret == null) {
                    throw new PythonException("The following code produced an exception: " + code);
                }
            }            
        }

        public bool RunStringTry(string code)
        {
            using (Py.GIL())
            {
                string try_code = "resultstr = ''\n" +
                    "try:\n    " +
                    code.Replace("\n", "\n    ") +
                    "\nexcept BaseException as e:\n" +
                    "    resultstr = str(e)\n";
                PythonEngine.RunString(try_code, Global.Handle, Local.Handle);
                string result = Get("resultstr");
                if (result != "")
                {
                    MessageBox.Show("A python exception occured: " + result);
                    return false;
                }
                return true;
            }
        }

        public dynamic Import(string module)
        {
            return Import(module, module);
        }

        public dynamic Import(string module, string alias)
        {
            dynamic res;
            using (Py.GIL())
            {
                res = PythonEngine.ImportModule(module);
                if (res == null)
                {
                    throw new FileNotFoundException("Module '" + module + "' could not be imported.");
                }
                Global.SetItem(alias, res);
            }
            return res;
        }

        public void ImportNumpy()
        {
            np = Import("numpy", "np");
            NumpyLoaded = true;
        }

        public void ImportScipy()
        {
            scipy = Import("scipy");
            ScipyLoaded = true;
        }

        public void ImportScipyStack()
        {
            ImportNumpy();
            ImportScipy();
        }

        public void ImportMatplotlib()
        {
            // Import matplotlib and set the renderer non-interactive
            Import("matplotlib");
            RunString("matplotlib.use('agg')");
            Import("matplotlib.pyplot", "plt");
        }

        public void AddRelativePath(string path)
        {
            string CurrentDirectory = Directory.GetCurrentDirectory();
            string NewPath = Path.GetFullPath(Path.Combine(CurrentDirectory, path));
            AddPath(NewPath);
        }


        public void AddPath(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new System.ArgumentException("Import path does not exist: " + path);
            }
            AdditionalPaths.Add(path);
            BuildPythonpath();
        }

        public void NewEnvironment()
        {
            using (Py.GIL())
                Locals.Push(new PyDict());
        }

        public void PushLocal(PyDict local)
        {
            using (Py.GIL())
                Locals.Push(local);
        }

        public PyDict PopLocal()
        {
            using (Py.GIL())
                return Locals.Pop();
        }

        public void ClearPath()
        {
            AdditionalPaths.Clear();
            BuildPythonpath();
        }

        private void BuildPythonpath(bool AddToSys = true)
        {
                Pythonpath = new List<string>();
                Pythonpath.Add(Path.GetFullPath(Path.Combine(PythonHome, "DLLs")));
                Pythonpath.Add(Path.GetFullPath(Path.Combine(PythonHome, "Lib")));
                Pythonpath.Add(Path.GetFullPath(Path.Combine(PythonHome, "Lib", "site-packages")));
                foreach (string p in AdditionalPaths)
                {
                    Pythonpath.Add(p);
                }
                if (AddToSys)
                {
                    using (Py.GIL())
                    {
                        PyObject NewPath = Pythonpath.ToPython();
                        sys.path = NewPath;
                    }
                }            
        }

    }
}
