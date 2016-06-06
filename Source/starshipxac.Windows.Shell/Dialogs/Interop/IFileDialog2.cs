using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Dialogs.Interop
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/dd378363(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ShellIID.IFileDialog2)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IFileDialog2 : IFileDialog
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetCancelButtonLabel([In] [MarshalAs(UnmanagedType.LPWStr)] string pszLabel);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetNavigationRoot([In] [MarshalAs(UnmanagedType.Interface)] IShellItem psi);
    }
}