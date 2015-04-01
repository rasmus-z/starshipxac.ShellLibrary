using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows;
using System.Windows.Interop;
using starshipxac.Windows.Devices.Interop;
using starshipxac.Windows.Interop;

namespace starshipxac.Windows.Devices
{
    /// <summary>
    /// PCに接続しているすべてのモニター情報を保持します。
    /// </summary>
    public static class MultiMonitor
    {
        private static List<Monitor> allMonitors;

        static MultiMonitor()
        {
            CreateAllMonitors();
        }

        /// <summary>
        /// メインモニター情報を取得します。
        /// </summary>
        public static Monitor PrimaryMonitor { get; private set; }

        /// <summary>
        /// すべてのモニター情報を取得します。
        /// </summary>
        public static IReadOnlyList<Monitor> AllMonitors
        {
            get
            {
                return allMonitors;
            }
        }

        /// <summary>
        /// すべてのサブモニター情報を取得します。
        /// </summary>
        public static IReadOnlyList<Monitor> SubMonitors
        {
            get
            {
                return AllMonitors.Where(x => x != PrimaryMonitor).ToList();
            }
        }

        #region Create Monitors

        private static void CreateAllMonitors()
        {
            allMonitors = new List<Monitor>();
            MultiMonitorNativeMethods.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, EnumMonitors, IntPtr.Zero);
        }

        private static bool EnumMonitors(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData)
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
                allMonitors.Add(screen);
                if (screen.IsPrimary)
                {
                    PrimaryMonitor = screen;
                }
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

        /// <summary>
        /// 指定したウィンドウが表示されているモニターを取得します。
        /// </summary>
        /// <param name="windowHandle">ウィンドウハンドル。</param>
        /// <returns>モニター情報。</returns>
        internal static Monitor FromHandle(IntPtr windowHandle)
        {
            var hMonitor = MultiMonitorNativeMethods.MonitorFromWindow(windowHandle, MonitorFlags.MONITOR_DEFAULTTONEAREST);
            return AllMonitors.FirstOrDefault(s => s.Handle == hMonitor);
        }

        /// <summary>
        /// 指定したウィンドウが表示されているモニターを取得します。
        /// </summary>
        /// <param name="window">ウィンドウ。</param>
        /// <returns>モニター情報。</returns>
        public static Monitor FromWindow(Window window)
        {
            Contract.Requires<ArgumentNullException>(window != null);

            var source = (HwndSource)PresentationSource.FromVisual(window);
            if (source == null)
            {
                return null;
            }
            return FromHandle(source.Handle);
        }

        /// <summary>
        /// 指定した座標が含まれるモニターを取得します。
        /// </summary>
        /// <param name="point">検査する座標。</param>
        /// <returns>モニター情報。</returns>
        public static Monitor FromPoint(Point point)
        {
            var pt = new POINT
            {
                X = (int)point.X,
                Y = (int)point.Y,
            };
            var hMonitor = MultiMonitorNativeMethods.MonitorFromPoint(pt, MonitorFlags.MONITOR_DEFAULTTONEAREST);
            return AllMonitors.FirstOrDefault(s => s.Handle == hMonitor);
        }

        /// <summary>
        /// 指定した四角形が含まれるモニターを取得します。
        /// </summary>
        /// <param name="rect">検査する四角形</param>
        /// <returns>モニター情報。</returns>
        public static Monitor FromRectangle(Rect rect)
        {
            var rc = new RECT
            {
                Left = (int)rect.Left,
                Right = (int)rect.Right,
                Top = (int)rect.Top,
                Bottom = (int)rect.Bottom,
            };
            var hMonitor = MultiMonitorNativeMethods.MonitorFromRect(ref rc, MonitorFlags.MONITOR_DEFAULTTONEAREST);
            return AllMonitors.FirstOrDefault(s => s.Handle == hMonitor);
        }

        public static Rect GetBounds(Point point)
        {
            var screen = FromPoint(point);
            return screen.Bounds;
        }

        public static Rect GetBounds(Rect rect)
        {
            var screen = FromRectangle(rect);
            return screen.Bounds;
        }

        public static Rect GetWorkingArea(Point point)
        {
            var screen = FromPoint(point);
            return screen.WorkingArea;
        }

        public static Rect GetWorkingArea(Rect rect)
        {
            var screen = FromRectangle(rect);
            return screen.WorkingArea;
        }
    }
}