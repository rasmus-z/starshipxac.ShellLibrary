using System;
using System.Runtime.InteropServices;

namespace starshipxac.Windows.Shell.Controls.Interop
{
    /// <summary>
    /// �C���[�W���X�g��Ԃ�ێ����܂��B
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct IMAGELISTSTATS
    {
        public UInt32 cbSIze;
        public int cAlloc;
        public int cUsed;
        public int cStandby;
    }
}