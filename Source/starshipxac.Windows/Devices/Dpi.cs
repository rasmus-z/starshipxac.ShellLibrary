using System;
using starshipxac.Windows.Devices.Interop;

namespace starshipxac.Windows.Devices
{
    /// <summary>
    ///     モニターの DPI値を定義します。
    /// </summary>
    public struct Dpi : IEquatable<Dpi>
    {
        static Dpi()
        {
            Default = new Dpi(96, 96);
        }

        private Dpi(int dpiX, int dpiY)
        {
            this.X = dpiX;
            this.Y = dpiY;
        }

        internal static Dpi Create(IntPtr hMonitor)
        {
            uint dpiX = 0;
            uint dpiY = 0;
            return MultiMonitorNativeMethods.GetDpiForMonitor(hMonitor, MONITOR_DPI_TYPE.MDT_DEFAULT, ref dpiX, ref dpiY) == IntPtr.Zero ? new Dpi((int) dpiX, (int) dpiY) : Default;
        }

        public static Dpi Default { get; }

        public int X { get; }

        public int Y { get; }

        public static bool operator ==(Dpi left, Dpi right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Dpi left, Dpi right)
        {
            return !Equals(left, right);
        }

        public bool Equals(Dpi other)
        {
            return (this.X == other.X) && (this.Y == other.Y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            return Equals((Dpi) obj);
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode();
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }
    }
}