using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows;
using System.Windows.Interop;
using starshipxac.Windows.Devices.Internal;
using starshipxac.Windows.Devices.Interop;
using starshipxac.Windows.Interop;

namespace starshipxac.Windows.Devices
{
    /// <summary>
    /// PCに接続しているすべてのディスプレイスクリーン情報を取得します。
    /// </summary>
    public static class MultiScreen
    {
        /// <summary>
        /// メインスクリーン情報を取得します。
        /// </summary>
        public static Screen GetPrimaryMonitor()
        {
            return EnumerateAllMonitors().FirstOrDefault(x => x.IsPrimary);
        }

        /// <summary>
        /// すべてのスクリーン情報を取得します。
        /// </summary>
        public static IEnumerable<Screen> EnumerateAllMonitors()
        {
            return new MonitorCollection();
        }

        /// <summary>
        /// 指定したウィンドウが表示されているスクリーンを取得します。
        /// </summary>
        /// <param name="windowHandle">ウィンドウハンドル。</param>
        /// <returns>スクリーン情報。</returns>
        internal static Screen FromHandle(IntPtr windowHandle)
        {
            var hMonitor = MultiMonitorNativeMethods.MonitorFromWindow(windowHandle, MonitorFlags.MONITOR_DEFAULTTONEAREST);
            return Screen.Create(hMonitor);
        }

        /// <summary>
        /// 指定したウィンドウが表示されているスクリーンを取得します。
        /// </summary>
        /// <param name="window">ウィンドウ。</param>
        /// <returns>スクリーン情報。</returns>
        public static Screen FromWindow(Window window)
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
        /// 指定した座標が含まれるスクリーンを取得します。
        /// </summary>
        /// <param name="point">検査する座標。</param>
        /// <returns>スクリーン情報。</returns>
        public static Screen FromPoint(Point point)
        {
            var pt = new POINT
            {
                X = (int)point.X,
                Y = (int)point.Y,
            };
            var hMonitor = MultiMonitorNativeMethods.MonitorFromPoint(pt, MonitorFlags.MONITOR_DEFAULTTONEAREST);
            return Screen.Create(hMonitor);
        }

        /// <summary>
        /// 指定した四角形が含まれるスクリーンを取得します。
        /// </summary>
        /// <param name="rect">検査する四角形</param>
        /// <returns>スクリーン情報。</returns>
        public static Screen FromRectangle(Rect rect)
        {
            var rc = new RECT
            {
                Left = (int)rect.Left,
                Right = (int)rect.Right,
                Top = (int)rect.Top,
                Bottom = (int)rect.Bottom,
            };
            var hMonitor = MultiMonitorNativeMethods.MonitorFromRect(ref rc, MonitorFlags.MONITOR_DEFAULTTONEAREST);
            return Screen.Create(hMonitor);
        }
    }
}