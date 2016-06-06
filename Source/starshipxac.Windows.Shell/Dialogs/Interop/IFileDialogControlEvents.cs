using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Dialogs.Interop
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb775936(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ShellIID.IFileDialogControlEvents)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IFileDialogControlEvents
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnItemSelected(
            [In] [MarshalAs(UnmanagedType.Interface)] IFileDialogCustomize pfdc,
            [In] UInt32 dwIDCtl,
            [In] UInt32 dwIDItem);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnButtonClicked(
            [In] [MarshalAs(UnmanagedType.Interface)] IFileDialogCustomize pfdc,
            [In] UInt32 dwIDCtl);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnCheckButtonToggled(
            [In] [MarshalAs(UnmanagedType.Interface)] IFileDialogCustomize pfdc,
            [In] UInt32 dwIDCtl,
            [In] bool bChecked);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnControlActivating(
            [In] [MarshalAs(UnmanagedType.Interface)] IFileDialogCustomize pfdc,
            [In] UInt32 dwIDCtl);
    }
}