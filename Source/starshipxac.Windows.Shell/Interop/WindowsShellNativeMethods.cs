using System;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.Controls.Interop;

namespace starshipxac.Windows.Shell.Interop
{
    internal static class WindowsShellNativeMethods
    {
        #region Shell Api

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        internal static extern int ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        internal static extern int SHFileOperation([In] ref SHFILEOPSTRUCT lpFileOp);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        internal static extern int SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, uint dwFlags);

        [DllImport("shell32.dll")]
        internal static extern HRESULT SHGetImageList(int iImageList, ref Guid riid, out IImageList ppv);

        #endregion
    }
}