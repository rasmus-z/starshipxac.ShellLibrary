using System;
using System.Collections;
using System.Collections.Generic;
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

        private bool EnumMonitors(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData)
        {
            var monitor = Monitor.Create(hMonitor);
            if (monitor != null)
            {
                this.monitors.Add(monitor);
            }
            return true;
        }
    }
}