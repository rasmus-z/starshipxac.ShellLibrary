using System;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Media.Imaging.Interop
{
    internal static class StockIconsNativeMethods
    {
        [PreserveSig]
        [DllImport("Shell32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = false)]
        internal static extern HRESULT SHGetStockIconInfo(
            SHSTOCKICONID identifier,
            UInt32 flags,
            ref SHSTOCKICONINFO info);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyIcon(IntPtr hIcon);
    }
}