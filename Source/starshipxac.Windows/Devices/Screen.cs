using System;
using System.Diagnostics.Contracts;
using System.Windows;
using starshipxac.Windows.Devices.Interop;
using starshipxac.Windows.Interop;

namespace starshipxac.Windows.Devices
{
    /// <summary>
    ///     スクリーン情報を保持します。
    /// </summary>
    public class Screen : IEquatable<Screen>
    {
        /// <summary>
        ///     モニターハンドルを指定して、
        ///     <see cref="Screen" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="hMonitor">モニターハンドル。</param>
        internal Screen(IntPtr hMonitor)
        {
            Contract.Requires<ArgumentNullException>(hMonitor != IntPtr.Zero);

            this.Handle = hMonitor;
        }

        internal static Screen Create(IntPtr hMonitor)
        {
            var monitorInfo = MONITORINFOEX.Create();
            var success = MultiMonitorNativeMethods.GetMonitorInfo(hMonitor, ref monitorInfo);
            if (!success)
            {
                return null;
            }

            var dpi = Dpi.Create(hMonitor);
            return new Screen(hMonitor)
            {
                DeviceName = monitorInfo.szDevice,
                IsPrimary = monitorInfo.dwFlags == MultiMonitorNativeMethods.MONITORINFOF_PRIMARY,
                Dpi = dpi,
                Bounds = CreateRect(monitorInfo.rcMonitor, dpi),
                WorkingArea = CreateRect(monitorInfo.rcWork, dpi)
            };
        }

        /// <summary>
        ///     モニターハンドルを取得します。
        /// </summary>
        internal IntPtr Handle { get; }

        /// <summary>
        ///     デバイス名を取得します。
        /// </summary>
        public string DeviceName { get; private set; }

        /// <summary>
        ///     第一スクリーンかどうかを判定する値を取得します。
        /// </summary>
        public bool IsPrimary { get; private set; }

        /// <summary>
        ///     スクリーンの DPIを取得します。
        /// </summary>
        public Dpi Dpi { get; private set; }

        /// <summary>
        ///     スクリーンのサイズを取得します。
        /// </summary>
        public Rect Bounds { get; private set; }

        /// <summary>
        ///     スクリーン内のアプリケーション動作領域のサイズを取得します。
        /// </summary>
        public Rect WorkingArea { get; private set; }

        public bool Equals(Screen other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return this.Handle.Equals(other.Handle);
        }

        private static Rect CreateRect(RECT rect, Dpi dpi)
        {
            var factorX = dpi.X/(double)Dpi.Default.X;
            var factorY = dpi.Y/(double)Dpi.Default.Y;

            var left = rect.Left/factorX;
            var top = rect.Top/factorY;
            var width = rect.Width/factorX;
            var height = rect.Height/factorY;

            return new Rect(left, top, width, height);
        }

        public static bool operator ==(Screen x, Screen y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(Screen x, Screen y)
        {
            return !Equals(x, y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return Equals((Screen)obj);
        }

        public override int GetHashCode()
        {
            return this.Handle.GetHashCode();
        }

        public override string ToString()
        {
            return $"{{DeviceName: {this.DeviceName}}}";
        }
    }
}