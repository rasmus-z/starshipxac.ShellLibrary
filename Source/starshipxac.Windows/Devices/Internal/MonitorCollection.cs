using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using starshipxac.Windows.Devices.Interop;
using starshipxac.Windows.Interop;

namespace starshipxac.Windows.Devices.Internal
{
    internal class MonitorCollection : IEnumerable<Monitor>
    {
        private List<Monitor> monitors;

        public MonitorCollection()
        {
        }

        public IEnumerator<Monitor> GetEnumerator()
        {
            this.monitors = new List<Monitor>();
            MultiMonitorNativeMethods.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, EnumMonitors, IntPtr.Zero);

            return this.monitors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region Create Monitors

        private bool EnumMonitors(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData)
        {
            var monitorInfo = MONITORINFOEX.Create();
            var success = MultiMonitorNativeMethods.GetMonitorInfo(hMonitor, ref monitorInfo);
            if (success)
            {
                var screen = new Monitor(hMonitor)
                {
                    DeviceName = monitorInfo.szDevice,
                    IsPrimary = (monitorInfo.dwFlags == MultiMonitorNativeMethods.MONITORINFOF_PRIMARY),
                    Bounds = CreateRect(monitorInfo.rcMonitor),
                    WorkingArea = CreateRect(monitorInfo.rcWork)
                };
                this.monitors.Add(screen);
            }
            return true;
        }

        private static Rect CreateRect(RECT rect)
        {
            var width = rect.Right - rect.Left;
            var height = rect.Bottom - rect.Top;
            return new Rect(rect.Left, rect.Top, width, height);
        }

        #endregion
    }
}