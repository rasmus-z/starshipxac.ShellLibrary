using System;
using System.Runtime.InteropServices;
using starshipxac.Windows.Interop;

namespace starshipxac.Windows.Devices.Interop
{
    /// <summary>
    /// ���j�^�[�����`���܂��B
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
        /// <see cref="MONITORINFOEX"/>�\���̂̃T�C�Y�B
        /// </summary>
        public int cbSize;

        /// <summary>
        /// ���j�^�[�̃T�C�Y�B
        /// </summary>
        public RECT rcMonitor;

        /// <summary>
        /// ���[�N�G���A�̃T�C�Y�B
        /// </summary>
        public RECT rcWork;

        /// <summary>
        /// ���j�^�[�̑����B
        /// </summary>
        public uint dwFlags;

        /// <summary>
        /// �f�o�C�X���B
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