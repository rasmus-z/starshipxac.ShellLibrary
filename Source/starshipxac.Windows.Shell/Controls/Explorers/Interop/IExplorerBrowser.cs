using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.Interop;

namespace starshipxac.Windows.Shell.Controls.Explorers.Interop
{
    /// <summary>
    /// エクスプローラーブラウザーインターフェイスを定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb761909(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid(ExplorerBrowserIIDGuid.IExplorerBrowser)]
    internal interface IExplorerBrowser
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Initialize(IntPtr hwndParent, [In] ref RECT prc, [In] FOLDERSETTINGS pfs);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Destroy();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetRect([In] [Out] ref IntPtr phdwp, RECT rcBrowser);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetPropertyBag([MarshalAs(UnmanagedType.LPWStr)] string pszPropertyBag);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetEmptyText([MarshalAs(UnmanagedType.LPWStr)] string pszEmptyText);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT SetFolderSettings(FOLDERSETTINGS pfs);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT Advise(IntPtr psbe, out uint pdwCookie);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT Unadvise([In] uint dwCookie);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetOptions([In] EXPLORER_BROWSER_OPTIONS dwFlag);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetOptions(out EXPLORER_BROWSER_OPTIONS pdwFlag);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void BrowseToIDList(IntPtr pidl, uint uFlags);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT BrowseToObject([MarshalAs(UnmanagedType.IUnknown)] object punk, uint uFlags);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void FillFromObject([MarshalAs(UnmanagedType.IUnknown)] object punk, int dwFlags);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void RemoveAll();

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetCurrentView(ref Guid riid, out IntPtr ppv);
    }
}