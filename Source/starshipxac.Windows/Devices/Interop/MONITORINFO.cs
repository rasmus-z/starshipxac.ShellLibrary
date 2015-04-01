using System;
using System.Runtime.InteropServices;
using starshipxac.Windows.Interop;

namespace starshipxac.Windows.Devices.Interop
{
    /// <summary>
    /// モニター情報を定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/dd145065(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct MONITORINFO
    {
        public static MONITORINFO Create()
        {
            return new MONITORINFO()
            {
                cbSize = 40
            };
        }

        /// <summary>
        /// <see cref="MONITORINFO"/>構造体のサイズ。
        /// </summary>
        public int cbSize;

        /// <summary>
        /// モニターのサイズ。
        /// </summary>
        public RECT rcMonitor;

        /// <summary>
        /// ワークエリアのサイズ。
        /// </summary>
        public RECT rcWork;

        /// <summary>
        /// モニターの属性。
        /// </summary>
        public uint dwFlags;
    }
}