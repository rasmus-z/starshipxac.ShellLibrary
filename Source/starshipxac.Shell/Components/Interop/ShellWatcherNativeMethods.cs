using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Components.Interop
{
    /// <summary>
    ///     シェル通知APIを定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb762119(v=vs.85).aspx
    /// </remarks>
    internal static class ShellWatcherNativeMethods
    {
        [DllImport("shell32.dll", SetLastError = true, EntryPoint = "#2", CharSet = CharSet.Auto)]
        internal static extern uint SHChangeNotifyRegister(
            IntPtr hwnd,
            Int32 fSources,
            UInt32 fEvents,
            UInt32 wMsg,
            Int32 cEntries,
            ref SHChangeNotifyEntry pshcne);

        [DllImport("shell32.dll")]
        internal static extern IntPtr SHChangeNotification_Lock(
            IntPtr hChange,
            UInt32 dwProcId,
            out IntPtr pppidl,
            out uint plEvent);

        [DllImport("shell32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean SHChangeNotification_Unlock(IntPtr hLock);

        [DllImport("shell32.dll", SetLastError = true, EntryPoint = "#4", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean SHChangeNotifyDeregister(uint ulID);
    }
}