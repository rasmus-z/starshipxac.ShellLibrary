using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb761686(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ShellIID.IModalWindow)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IModalWindow
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [PreserveSig]
        int Show([In] IntPtr parent);
    }
}