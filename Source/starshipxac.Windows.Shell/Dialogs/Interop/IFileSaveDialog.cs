using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Windows.Shell.Dialogs.Interop
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb775688(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ShellIID.IFileSaveDialog)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IFileSaveDialog : IFileDialog2
    {
#pragma warning disable 0108

        #region IModalWindow Method

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [PreserveSig]
        int Show([In] IntPtr parent);

        #endregion

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetFileTypes(
            [In] UInt32 cFileTypes,
            [In] ref COMDLG_FILTERSPEC rgFilterSpec);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetFileTypeIndex([In] UInt32 iFileType);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetFileTypeIndex(out UInt32 piFileType);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Advise(
            [In] [MarshalAs(UnmanagedType.Interface)] IFileDialogEvents pfde,
            out UInt32 pdwCookie);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Unadvise([In] UInt32 dwCookie);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetOptions([In] FILEOPENDIALOGOPTIONS fos);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetOptions(out FILEOPENDIALOGOPTIONS pfos);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetDefaultFolder([In] [MarshalAs(UnmanagedType.Interface)] IShellItem psi);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetFolder([In] [MarshalAs(UnmanagedType.Interface)] IShellItem psi);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetFolder([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetCurrentSelection([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetFileName([In] [MarshalAs(UnmanagedType.LPWStr)] string pszName);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetTitle([In] [MarshalAs(UnmanagedType.LPWStr)] string pszTitle);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetOkButtonLabel([In] [MarshalAs(UnmanagedType.LPWStr)] string pszText);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetFileNameLabel([In] [MarshalAs(UnmanagedType.LPWStr)] string pszLabel);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetResult([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void AddPlace(
            [In] [MarshalAs(UnmanagedType.Interface)] IShellItem psi,
            FDAP fdap);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetDefaultExtension([In] [MarshalAs(UnmanagedType.LPWStr)] string pszDefaultExtension);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Close([MarshalAs(UnmanagedType.Error)] int hr);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetClientGuid([In] ref Guid guid);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void ClearClientData();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetFilter([MarshalAs(UnmanagedType.Interface)] IntPtr pFilter);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetSaveAsItem([In] [MarshalAs(UnmanagedType.Interface)] IShellItem psi);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetProperties([In] [MarshalAs(UnmanagedType.Interface)] IntPtr pStore);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int SetCollectedProperties(
            [In] IPropertyDescriptionList pList,
            [In] bool fAppendDefault);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [PreserveSig]
        HRESULT GetProperties(out IPropertyStore ppStore);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void ApplyProperties(
            [In] [MarshalAs(UnmanagedType.Interface)] IShellItem psi,
            [In] [MarshalAs(UnmanagedType.Interface)] IntPtr pStore,
            [In] [ComAliasName("ShellObjects.wireHWND")] ref IntPtr hwnd,
            [In] [MarshalAs(UnmanagedType.Interface)] IntPtr pSink);

#pragma warning restore 0108
    }
}