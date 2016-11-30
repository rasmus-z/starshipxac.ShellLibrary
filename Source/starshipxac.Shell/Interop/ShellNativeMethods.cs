using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using starshipxac.Shell.Interop.KnownFolder;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     Shell Functions
    /// </summary>
    /// <remarks>
    ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb776426(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class ShellNativeMethods
    {
        #region Shell Functions

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

        /// <summary>
        ///     The ExtractIconEx function creates an array of handles to large or small icons extracted from the specified executable file, DLL, or icon file.
        /// </summary>
        /// <param name="szFileName"></param>
        /// <param name="nIconIndex"></param>
        /// <param name="phiconLarge"></param>
        /// <param name="phiconSmall"></param>
        /// <param name="nIcons"></param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb776417%28v=vs.85%29.aspx?f=255&MSPPError=-2147217396
        /// </remarks>
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        internal static extern uint ExtractIconEx(string szFileName, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall,
            uint nIcons);

        internal static uint ExtractIconEx_GetCount(string szFileName)
        {
            return ExtractIconEx(szFileName, -1, null, null, 0);
        }

        /// <summary>
        ///     Returns the size, in bytes, of an ITEMIDLIST structure.
        /// </summary>
        /// <param name="pidl"></param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb776443(v=vs.85).aspx
        /// </remarks>
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern uint ILGetSize(IntPtr pidl);

        /// <summary>
        ///     Returns a pointer to the last SHITEMID structure in an ITEMIDLIST structure.
        /// </summary>
        /// <param name="pidl"></param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb776440(v=vs.85).aspx
        /// </remarks>
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern PIDL ILFindLastID(PIDL pidl);

        /// <summary>
        ///     Frees an ITEMIDLIST structure allocated by the Shell.
        /// </summary>
        /// <param name="pidl"></param>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb776441(v=vs.85).aspx
        /// </remarks>
        [DllImport("shell32.dll", CharSet = CharSet.None)]
        internal static extern void ILFree(IntPtr pidl);

        /// <summary>
        ///     Takes a pointer to a fully qualified item identifier list (PIDL),
        ///     and returns a specified interface pointer on the parent object.
        /// </summary>
        /// <param name="pidl"></param>
        /// <param name="riid"></param>
        /// <param name="ppv"></param>
        /// <param name="ppidlLast"></param>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb762114(v=vs.85).aspx
        /// </remarks>
        [DllImport("shell32.dll", ExactSpelling = true, PreserveSig = false)]
        internal static extern void SHBindToParent(
            [In] [MarshalAs(UnmanagedType.LPStruct)] ITEMIDLIST pidl,
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out object ppv,
            IntPtr ppidlLast);

        /// <summary>
        ///     Takes a pointer to a fully qualified item identifier list (PIDL),
        ///     and returns a specified interface pointer on the parent object.
        /// </summary>
        /// <param name="pidl"></param>
        /// <param name="riid"></param>
        /// <param name="pParentShellFolder"></param>
        /// <param name="ppidlLast"></param>
        [DllImport("shell32.dll", ExactSpelling = true, PreserveSig = false)]
        internal static extern void SHBindToParent(
            [In] [MarshalAs(UnmanagedType.Struct)] PIDL pidl,
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out IShellFolder pParentShellFolder,
            IntPtr ppidlLast = default(IntPtr));

        /// <summary>
        ///     Creates a Shell item array object from a data object. 
        /// </summary>
        /// <param name="pdo"></param>
        /// <param name="riid"></param>
        /// <param name="iShellItemArray"></param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb762145%28v=vs.85%29.aspx?f=255&MSPPError=-2147217396
        /// </remarks>
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT SHCreateShellItemArrayFromDataObject(
            System.Runtime.InteropServices.ComTypes.IDataObject pdo,
            ref Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out IShellItemArray iShellItemArray);

        /// <summary>
        ///     Creates and initializes a Shell item object from a parsing name.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="pbc"></param>
        /// <param name="riid"></param>
        /// <param name="shellItem"></param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb762134(v=vs.85).aspx
        /// </remarks>
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT SHCreateItemFromParsingName(
            [MarshalAs(UnmanagedType.LPWStr)] string path,
            IntPtr pbc,
            ref Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out IShellItem2 shellItem);

        /// <summary>
        ///     Creates and initializes a Shell item object from a parsing name.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="pbc"></param>
        /// <param name="riid"></param>
        /// <param name="shellItem"></param>
        /// <returns></returns>
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT SHCreateItemFromParsingName(
            [MarshalAs(UnmanagedType.LPWStr)] string path,
            IntPtr pbc,
            ref Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out IShellItem shellItem);

        /// <summary>
        ///     Creates and initializes a Shell item object from a pointer to an item identifier list (PIDL).
        ///     The resulting shell item object supports the IShellItem interface.
        /// </summary>
        /// <param name="pidl"></param>
        /// <param name="riid"></param>
        /// <param name="ppv"></param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb762133(v=vs.85).aspx
        /// </remarks>
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT SHCreateItemFromIDList(
            IntPtr pidl,
            ref Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out IShellItem2 ppv);

        /// <summary>
        ///     Translates a Shell namespace object's display name into an item identifier list and returns the attributes of the object.
        ///     This function is the preferred method to convert a string to a pointer to an item identifier list (PIDL).
        /// </summary>
        /// <param name="pszName"></param>
        /// <param name="pbc"></param>
        /// <param name="ppidl"></param>
        /// <param name="sfgaoIn"></param>
        /// <param name="psfgaoOut"></param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb762236(v=vs.85).aspx
        /// </remarks>
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT SHParseDisplayName(
            [MarshalAs(UnmanagedType.LPWStr)] string pszName,
            IntPtr pbc,
            out IntPtr ppidl,
            UInt32 sfgaoIn,
            out UInt32 psfgaoOut);

        /// <summary>
        ///     Retrieves the pointer to an item identifier list (PIDL) of an object.
        /// </summary>
        /// <param name="iUnknown"></param>
        /// <param name="ppidl"></param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb762184(v=vs.85).aspx
        /// </remarks>
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT SHGetIDListFromObject(IntPtr iUnknown,
            out IntPtr ppidl);

        /// <summary>
        ///     Retrieves the IShellFolder interface for the desktop folder, which is the root of the Shell's namespace.
        /// </summary>
        /// <param name="ppshf"></param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb762175%28v=vs.85%29.aspx?f=255&MSPPError=-2147217396
        /// </remarks>
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT SHGetDesktopFolder(
            [MarshalAs(UnmanagedType.Interface)] out IShellFolder ppshf);

        /// <summary>
        ///     Create a Shell item, given a parent folder and a child item ID.
        /// </summary>
        /// <param name="pidParent"></param>
        /// <param name="psfParent"></param>
        /// <param name="pidl"></param>
        /// <param name="riid"></param>
        /// <param name="ppvItem"></param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb762137%28v=vs.85%29.aspx?f=255&MSPPError=-2147217396
        /// </remarks>
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT SHCreateItemWithParent(
            IntPtr pidParent,
            [In] [MarshalAs(UnmanagedType.Interface)] IShellFolder psfParent,
            [In] IntPtr pidl,
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [Out] [MarshalAs(UnmanagedType.Interface, IidParameterIndex = 3)] out IShellItem ppvItem);

        /// <summary>
        ///     Converts an item identifier list to a file system path.
        /// </summary>
        /// <param name="pidl"></param>
        /// <param name="pszPath"></param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb762194(v=vs.85).aspx
        /// </remarks>
        [DllImport("shell32.dll", EntryPoint = "SHGetPathFromIDListW", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SHGetPathFromIDList(IntPtr pidl, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszPath);

        /// <summary>
        ///     Retrieves information about an object in the file system, such as a file, folder, directory, or drive root.
        /// </summary>
        /// <param name="pszPath"></param>
        /// <param name="dwFileAttributes"></param>
        /// <param name="psfi"></param>
        /// <param name="cbFileInfo"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb762179(v=vs.85).aspx
        /// </remarks>
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

        #endregion

        internal static readonly HRESULT InPlaceStringTruncated = (HRESULT)0x00401A0;
    }
}