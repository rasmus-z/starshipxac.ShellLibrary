using System;
using System.Runtime.InteropServices;
using starshipxac.Windows.Interop;

namespace starshipxac.Windows.Devices.Interop
{
    /// <summary>
    /// ���j�^�[�����`���܂��B
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
        /// <see cref="MONITORINFO"/>�\���̂̃T�C�Y�B
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
    }
}