using System;
using System.Runtime.InteropServices;
using starshipxac.Windows.Interop;

namespace starshipxac.Windows.Devices.Interop
{
    internal static class MultiMonitorNativeMethods
    {
        internal const int CCHDEVICENAME = 32;
        internal const int MONITORINFOF_PRIMARY = 1;

        [DllImport("user32.dll")]
        internal static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip,
            EnumMonitorsDelegate lpfnEnum, IntPtr dwData);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFOEX lpmi);

        [DllImport("user32.dll")]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr MonitorFromPoint(POINT pt, MonitorFlags dwFlags);

        [DllImport("user32.dll")]
        internal static extern IntPtr MonitorFromRect([In] ref RECT lprc, MonitorFlags dwFlags);

        [DllImport("user32.dll")]
        internal static extern IntPtr MonitorFromWindow(IntPtr hwnd, MonitorFlags dwFlags);

        internal delegate bool EnumMonitorsDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);
    }
}