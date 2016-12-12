using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Components.Interop
{
    /// <summary>
    ///     Shell Functions
    /// </summary>
    /// <remarks>
    ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb776426(v=vs.85).aspx
    /// </remarks>
    internal static class ShellWatcherNativeMethods
    {
        /// <summary>
        ///     Registers a window to receive notifications from the file system or Shell,
        ///     if the file system supports notifications.
        /// </summary>
        /// <param name="hwnd">A handle to the window that receives the change or notification messages.</param>
        /// <param name="fSources">One or more of the following values that indicate the type of events for which to receive notifications.</param>
        /// <param name="fEvents">Change notification events for which to receive notification. See the <c>SHCNE</c> flags listed in <c>SHChangeNotify</c> for possible values.</param>
        /// <param name="wMsg">Message to be posted to the window procedure.</param>
        /// <param name="cEntries">Number of entries in the pshcne array.</param>
        /// <param name="pshcne">Array of <see cref="SHChangeNotifyEntry"/> structures that contain the notifications. </param>
        /// <returns>Returns a handle (HLOCK) to the locked memory.</returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb762120(v=vs.85).aspx
        /// </remarks>
        [DllImport("shell32.dll", SetLastError = true, EntryPoint = "#2", CharSet = CharSet.Auto)]
        internal static extern uint SHChangeNotifyRegister(
            IntPtr hwnd,
            Int32 fSources,
            UInt32 fEvents,
            UInt32 wMsg,
            Int32 cEntries,
            ref SHChangeNotifyEntry pshcne);

        /// <summary>
        ///     Locks the shared memory associated with a Shell change notification event.
        /// </summary>
        /// <param name="hChange">A handle to a window received as a <c>wParam</c> in the specified Shell change notification message.</param>
        /// <param name="dwProcId">The process ID (<c>lParam</c> in the message callback).</param>
        /// <param name="pppidl">The address of a pointer to a <c>PIDLIST_ABSOLUTE</c> that, when this function returns successfully, receives the list of affected PIDLs.</param>
        /// <param name="plEvent">A pointer to a <c>LONG</c> value that, when this function returns successfully, receives the Shell change notification ID of the event that took place.</param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/dd378425(v=vs.85).aspx
        /// </remarks>
        [DllImport("shell32.dll")]
        internal static extern IntPtr SHChangeNotification_Lock(
            IntPtr hChange,
            UInt32 dwProcId,
            out IntPtr pppidl,
            out uint plEvent);

        /// <summary>
        ///     Unlocks shared memory for a change notification.
        /// </summary>
        /// <param name="hLock">A handle to the memory lock.</param>
        /// <returns>Returns <c>true</c> on success; otherwise, <c>false</c>.</returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb762117(v=vs.85).aspx
        /// </remarks>
        [DllImport("shell32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean SHChangeNotification_Unlock(IntPtr hLock);

        /// <summary>
        ///     Unregisters the client's window process from receiving <c>SHChangeNotify</c> messages.
        /// </summary>
        /// <param name="ulID">A value of type ULONG that specifies the registration ID returned by <see cref="SHChangeNotifyRegister(IntPtr, int, uint, uint, int, ref SHChangeNotifyEntry)"/>.</param>
        /// <returns>Returns <c>true</c> if the specified client was found and removed; otherwise <c>false</c>.</returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb762119(v=vs.85).aspx
        /// </remarks>
        [DllImport("shell32.dll", SetLastError = true, EntryPoint = "#4", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean SHChangeNotifyDeregister(uint ulID);
    }
}