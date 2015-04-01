using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop.Library
{
    [ComImport]
    [Guid(ShellIID.IShellLibrary)]
    [CoClass(typeof(ShellLibraryCoClass))]
    internal interface IShellLibraryNative : IShellLibrary
    {
    }
}