using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Controls.Explorers.Interop
{
    [ComImport]
    [Guid(ExplorerBrowserIIDGuid.IServiceProvider)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IServiceProvider
    {
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall)]
        HRESULT QueryService(ref Guid guidService, ref Guid riid, out IntPtr ppvObject);
    };
}