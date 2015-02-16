using System;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb761090(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ShellIID.IShellItemFilter)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IShellItemFilter
    {
        [PreserveSig]
        HRESULT IncludeItem([In] IShellItem psi);

        [PreserveSig]
        HRESULT GetEnumFlagsForItem(
            [In] IShellItem psi,
            [Out] out SHCONTF pgrfFlags);
    }
}