using System;
using System.Diagnostics.Contracts;
using System.Windows;

namespace starshipxac.Windows.Devices
{
    /// <summary>
    /// モニター情報を保持します。
    /// </summary>
    public class Monitor : IEquatable<Monitor>
    {
        /// <summary>
        /// モニターハンドルを指定して、
        /// <see cref="Monitor"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="hMonitor">モニターハンドル。</param>
        internal Monitor(IntPtr hMonitor)
        {
            Contract.Requires<ArgumentNullException>(hMonitor != IntPtr.Zero);

            this.Handle = hMonitor;
        }

        /// <summary>
        /// モニターハンドルを取得します。
        /// </summary>
        internal IntPtr Handle { get; private set; }

        /// <summary>
        /// デバイス名を取得します。
        /// </summary>
        public string DeviceName { get; internal set; }

        /// <summary>
        /// 第一モニターかどうかを判定する値を取得します。
        /// </summary>
        public bool IsPrimary { get; internal set; }

        /// <summary>
        /// モニターのサイズを取得します。
        /// </summary>
        public Rect Bounds { get; internal set; }

        /// <summary>
        /// モニター内のアプリケーション動作領域のサイズを取得します。
        /// </summary>
        public Rect WorkingArea { get; internal set; }

        public static bool operator ==(Monitor x, Monitor y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(Monitor x, Monitor y)
        {
            return !Equals(x, y);
        }

        public bool Equals(Monitor other)
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
            return Equals((Monitor)obj);
        }

        public override int GetHashCode()
        {
            return this.Handle.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("Monitor: {{DeviceName={0}}}", this.DeviceName);
        }
    }
}