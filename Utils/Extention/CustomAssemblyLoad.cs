using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Loader;
using System.Reflection;

namespace Softka.Utils.Extention
{
    public class CustomAssemblyLoad : AssemblyLoadContext
    {
        public IntPtr LoadUnmanagedLibrary(string Path)
        {
            return LoadUnmanagedDll(Path);
        }

        protected override IntPtr LoadUnmanagedDll(string DllName)
        {
            return LoadUnmanagedDllFromPath(DllName);
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            throw new NotImplementedException();
        }
    }
}