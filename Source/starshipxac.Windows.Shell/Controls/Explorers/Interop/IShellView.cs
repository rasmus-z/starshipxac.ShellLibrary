using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Controls.Explorers.Interop
{
    [ComImport]
    [Guid(ExplorerBrowserIIDGuid.IShellView)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IShellView
    {
#pragma warning disable 108
        // IOleWindow
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetWindow(out IntPtr phwnd);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT ContextSensitiveHelp(bool fEnterMode);

        // IShellView
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT TranslateAccelerator(IntPtr pmsg);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT EnableModeless(bool fEnable);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT UIActivate(uint uState);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT Refresh();

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT CreateViewWindow(
            [MarshalAs(UnmanagedType.IUnknown)] object psvPrevious,
            IntPtr pfs,
            [MarshalAs(UnmanagedType.IUnknown)] object psb,
            IntPtr prcView,
            out IntPtr phWnd);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT DestroyViewWindow();

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetCurrentInfo(out IntPtr pfs);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT AddPropertySheetPages(uint dwReserved, IntPtr pfn, uint lparam);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT SaveViewState();

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT SelectItem(IntPtr pidlItem, uint uFlags);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetItemObject(
            SVGIO uItem,
            ref Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppv);
#pragma warning restore 108
    }
}