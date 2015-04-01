using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Dialogs.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb775876(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ShellIID.IFileDialogEvents)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IFileDialogEvents
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [PreserveSig]
        HRESULT OnFileOk([In] [MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [PreserveSig]
        HRESULT OnFolderChanging(
            [In] [MarshalAs(UnmanagedType.Interface)] IFileDialog pfd,
            [In] [MarshalAs(UnmanagedType.Interface)] IShellItem psiFolder);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnFolderChange([In] [MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnSelectionChange([In] [MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnShareViolation(
            [In] [MarshalAs(UnmanagedType.Interface)] IFileDialog pfd,
            [In] [MarshalAs(UnmanagedType.Interface)] IShellItem psi,
            out FDE_SHAREVIOLATION_RESPONSE pResponse);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnTypeChange([In] [MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnOverwrite([In] [MarshalAs(UnmanagedType.Interface)] IFileDialog pfd,
            [In] [MarshalAs(UnmanagedType.Interface)] IShellItem psi,
            out FDE_OVERWRITE_RESPONSE pResponse);
    }
}