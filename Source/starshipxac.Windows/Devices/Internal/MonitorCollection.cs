using System;
using System.Collections;
using System.Collections.Generic;
using starshipxac.Windows.Devices.Interop;
using starshipxac.Windows.Interop;

namespace starshipxac.Windows.Devices.Internal
{
    internal class MonitorCollection : IEnumerable<Screen>
    {
        private List<Screen> monitors;

        public MonitorCollection()
        {
        }

        public IEnumerator<Screen> GetEnumerator()
        {
            this.monitors = new List<Screen>();
            MultiMonitorNativeMethods.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, EnumMonitors, IntPtr.Zero);

            return this.monitors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private bool EnumMonitors(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData)
        {
            var monitor = Screen.Create(hMonitor);
            if (monitor != null)
            {
                this.monitors.Add(monitor);
            }
            return true;
        }
    }
}