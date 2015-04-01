using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.Interop;

namespace starshipxac.Windows.Shell.Controls.Explorers.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb775541(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ExplorerBrowserIIDGuid.IFolderView2)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IFolderView2 : IFolderView
    {
#pragma warning disable 108,114
        // IFolderView
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetCurrentViewMode(out uint pViewMode);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetCurrentViewMode(uint ViewMode);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetFolder(ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Item(int iItemIndex, out IntPtr ppidl);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT ItemCount(uint uFlags, out int pcItems);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT Items(uint uFlags, ref Guid riid, [Out] [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetSelectionMarkedItem(out int piItem);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetFocusedItem(out int piItem);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetItemPosition(IntPtr pidl, out POINT ppt);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetSpacing([Out] out POINT ppt);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetDefaultSpacing(out POINT ppt);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetAutoArrange();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SelectItem(int iItem, uint dwFlags);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SelectAndPositionItems(uint cidl, IntPtr apidl, ref POINT apt, uint dwFlags);

        // IFolderView2
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetGroupBy(IntPtr key, bool fAscending);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetGroupBy(ref IntPtr pkey, ref bool pfAscending);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetViewProperty(IntPtr pidl, IntPtr propkey, object propvar);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetViewProperty(IntPtr pidl, IntPtr propkey, out object ppropvar);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetTileViewProperties(IntPtr pidl, [MarshalAs(UnmanagedType.LPWStr)] string pszPropList);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetExtendedTileViewProperties(IntPtr pidl, [MarshalAs(UnmanagedType.LPWStr)] string pszPropList);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetText(int iType, [MarshalAs(UnmanagedType.LPWStr)] string pwszText);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetCurrentFolderFlags(uint dwMask, uint dwFlags);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetCurrentFolderFlags(out uint pdwFlags);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetSortColumnCount(out int pcColumns);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetSortColumns(IntPtr rgSortColumns, int cColumns);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetSortColumns(out IntPtr rgSortColumns, int cColumns);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetItem(int iItem, ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetVisibleItem(int iStart, bool fPrevious, out int piItem);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetSelectedItem(int iStart, out int piItem);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetSelection(bool fNoneImpliesFolder, out IShellItemArray ppsia);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetSelectionState(IntPtr pidl, out uint pdwFlags);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void InvokeVerbOnSelection([In] [MarshalAs(UnmanagedType.LPWStr)] string pszVerb);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT SetViewModeAndIconSize(int uViewMode, int iImageSize);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetViewModeAndIconSize(out int puViewMode, out int piImageSize);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetGroupSubsetCount(uint cVisibleRows);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetGroupSubsetCount(out uint pcVisibleRows);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetRedraw(bool fRedrawOn);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void IsMoveInSameFolder();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void DoRename();
#pragma warning restore 108,114
    }
}