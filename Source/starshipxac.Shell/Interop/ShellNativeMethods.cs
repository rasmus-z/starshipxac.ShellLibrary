using System;
using System.Runtime.InteropServices;
using System.Text;
using starshipxac.Shell.Interop.KnownFolder;

namespace starshipxac.Shell.Interop
{
    internal static class ShellNativeMethods
    {
        #region Shell Api

        internal static void FreeKnownFolderDefinitionFields(ref KNOWNFOLDER_DEFINITION pKFD)
        {
            Marshal.FreeCoTaskMem(pKFD.pszName);
            Marshal.FreeCoTaskMem(pKFD.pszDescription);
            Marshal.FreeCoTaskMem(pKFD.pszRelativePath);
            Marshal.FreeCoTaskMem(pKFD.pszParsingName);
            Marshal.FreeCoTaskMem(pKFD.pszTooltip);
            Marshal.FreeCoTaskMem(pKFD.pszLocalizedName);
            Marshal.FreeCoTaskMem(pKFD.pszIcon);
            Marshal.FreeCoTaskMem(pKFD.pszSecurity);
        }

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        internal static extern uint ExtractIconEx(string szFileName, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall,
            uint nIcons);

        internal static uint ExtractIconEx_GetCount(string szFileName)
        {
            return ExtractIconEx(szFileName, -1, null, null, 0);
        }

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern uint ILGetSize(IntPtr pidl);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern PIDL ILFindLastID(PIDL pidl);

        [DllImport("shell32.dll", CharSet = CharSet.None)]
        internal static extern void ILFree(IntPtr pidl);

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int PathParseIconLocation(
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconFile);

        [DllImport("shell32.dll", ExactSpelling = true, PreserveSig = false)]
        internal static extern void SHBindToParent(
            [In] [MarshalAs(UnmanagedType.LPStruct)] ITEMIDLIST pidl,
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out object ppv,
            IntPtr ppidlLast);

        [DllImport("shell32.dll", ExactSpelling = true, PreserveSig = false)]
        internal static extern void SHBindToParent(
            [In] [MarshalAs(UnmanagedType.Struct)] PIDL pidl,
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out IShellFolder pParentShellFolder,
            IntPtr ppidlLast = default(IntPtr));

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT SHCreateShellItemArrayFromDataObject(
            System.Runtime.InteropServices.ComTypes.IDataObject pdo,
            ref Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out IShellItemArray iShellItemArray);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT SHCreateItemFromParsingName(
            [MarshalAs(UnmanagedType.LPWStr)] string path,
            IntPtr pbc,
            ref Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out IShellItem2 shellItem);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT SHCreateItemFromParsingName(
            [MarshalAs(UnmanagedType.LPWStr)] string path,
            IntPtr pbc,
            ref Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out IShellItem shellItem);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT SHCreateItemFromIDList(
            IntPtr pidl,
            ref Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out IShellItem2 ppv);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT SHParseDisplayName(
            [MarshalAs(UnmanagedType.LPWStr)] string pszName,
            IntPtr pbc,
            out IntPtr ppidl,
            UInt32 sfgaoIn,
            out UInt32 psfgaoOut);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT SHGetIDListFromObject(IntPtr iUnknown,
            out IntPtr ppidl);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT SHGetDesktopFolder(
            [MarshalAs(UnmanagedType.Interface)] out IShellFolder ppshf);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT SHCreateShellItem(
            IntPtr pidlParent,
            [In] [MarshalAs(UnmanagedType.Interface)] IShellFolder psfParent,
            IntPtr pidl,
            [MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

        [DllImport("shell32.dll", EntryPoint = "SHGetPathFromIDListW", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SHGetPathFromIDList(IntPtr pidl, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszPath);

        [DllImport("shell32.dll", EntryPoint = "SHGetFileInfoW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr SHGetFileInfo(string pszPath, UInt32 dwFileAttributes, ref SHFILEINFO psfi,
            UInt32 cbFileInfo, UInt32 uFlags);

        // SHGetFileInfo
        [DllImport("shell32.dll", EntryPoint = "SHGetFileInfoW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr SHGetFileInfo(PIDL pidl, UInt32 dwFileAttributes, ref SHFILEINFO psfi,
            UInt32 cbFileInfo, UInt32 uFlags);

        internal static IntPtr SHGetFileInfo(PIDL pidl, UInt32 dwFileAttributes, ref SHFILEINFO psfi, UInt32 uFlags)
        {
            var cbFileInfo = (UInt32)Marshal.SizeOf(psfi);
            return SHGetFileInfo(pidl, dwFileAttributes, ref psfi, cbFileInfo, (UInt32)uFlags);
        }

        [DllImport("shlwapi.dll", BestFitMapping = false, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = false,
            ThrowOnUnmappableChar = true)]
        internal static extern int SHLoadIndirectString(string pszSource, StringBuilder pszOutBuf, int cchOutBuf,
            IntPtr ppvReserved);

        #endregion Shell Api

        #region IUnknown Functions

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        internal static extern void IUnknown_AtomicRelease([In] [Out] ref IntPtr ppunk);

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        internal static extern HRESULT IUnknown_GetWindow([In] [MarshalAs(UnmanagedType.IUnknown)] object punk,
            [Out] out IntPtr phwnd);

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        internal static extern HRESULT IUnknown_QueryService(
            [In] [MarshalAs(UnmanagedType.IUnknown)] object punk,
            [In] Guid guidService,
            [In] Guid riid,
            [Out] out IntPtr ppvOut);

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        internal static extern void IUnknown_Set(
            [In] [Out] [MarshalAs(UnmanagedType.IUnknown)] ref object ppunk,
            [In] [MarshalAs(UnmanagedType.IUnknown)] object punk);

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT IUnknown_SetSite(
            [In] [MarshalAs(UnmanagedType.IUnknown)] object punk,
            [In] [MarshalAs(UnmanagedType.IUnknown)] object punkSite);

        #endregion

        internal static readonly HRESULT InPlaceStringTruncated = (HRESULT)0x00401A0;
    }
}