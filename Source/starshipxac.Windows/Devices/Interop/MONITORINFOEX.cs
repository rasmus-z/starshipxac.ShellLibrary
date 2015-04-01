using System;
using System.Runtime.InteropServices;
using starshipxac.Windows.Interop;

namespace starshipxac.Windows.Devices.Interop
{
    /// <summary>
    /// モニター情報を定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/dd145066(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct MONITORINFOEX
    {
        public static MONITORINFOEX Create()
        {
            return new MONITORINFOEX
            {
                cbSize = 40 + 2 * MultiMonitorNativeMethods.CCHDEVICENAME,
                szDevice = string.Empty
            };
        }

        /// <summary>
        /// <see cref="MONITORINFOEX"/>構造体のサイズ。
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

        /// <summary>
        /// デバイス名。
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MultiMonitorNativeMethods.CCHDEVICENAME)]
        public string szDevice;

        public void Initialize()
        {
            this.cbSize = 40 + 2 * MultiMonitorNativeMethods.CCHDEVICENAME;
            this.szDevice = string.Empty;
        }
    }
}