using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.Interop;

namespace starshipxac.Windows.Shell.Controls.Explorers.Interop
{
    /// <summary>
    /// <see cref="IExplorerBrowser"/>��������܂��B
    /// </summary>
    [ComImport]
    [TypeLibType(TypeLibTypeFlags.FCanCreate)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid(ShellCLSID.ExplorerBrowser)]
    internal class ExplorerBrowserNative : IExplorerBrowser
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void Initialize(IntPtr hwndParent, [In] ref RECT prc, [In] FOLDERSETTINGS pfs);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void Destroy();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void SetRect([In] [Out] ref IntPtr phdwp, RECT rcBrowser);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void SetPropertyBag([MarshalAs(UnmanagedType.LPWStr)] string pszPropertyBag);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void SetEmptyText([MarshalAs(UnmanagedType.LPWStr)] string pszEmptyText);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern HRESULT SetFolderSettings(FOLDERSETTINGS pfs);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern HRESULT Advise(IntPtr psbe, out uint pdwCookie);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern HRESULT Unadvise(uint dwCookie);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void SetOptions([In] EXPLORER_BROWSER_OPTIONS dwFlag);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void GetOptions(out EXPLORER_BROWSER_OPTIONS pdwFlag);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void BrowseToIDList(IntPtr pidl, uint uFlags);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern HRESULT BrowseToObject([MarshalAs(UnmanagedType.IUnknown)] object punk, uint uFlags);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void FillFromObject([MarshalAs(UnmanagedType.IUnknown)] object punk, int dwFlags);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void RemoveAll();

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern HRESULT GetCurrentView(ref Guid riid, out IntPtr ppv);
    }
}