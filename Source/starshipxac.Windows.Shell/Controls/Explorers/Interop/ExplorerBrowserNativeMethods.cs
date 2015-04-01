using System;
using System.Runtime.InteropServices;
using System.Security;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Controls.Explorers.Interop
{
    [SuppressUnmanagedCodeSecurity]
    internal static class ExplorerBrowserNativeMethods
    {
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT ConnectToConnectionPoint(
            [In] [MarshalAs(UnmanagedType.IUnknown)] object punk,
            ref Guid riidEvent,
            [MarshalAs(UnmanagedType.Bool)] bool fConnect,
            [In] [MarshalAs(UnmanagedType.IUnknown)] object punkTarget,
            ref uint pdwCookie,
            ref IntPtr ppcpOut);
    }
}