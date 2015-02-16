using System;
using System.Runtime.InteropServices;

namespace starshipxac.Windows.Shell.Controls.Interop
{
    /// <summary>
    /// イメージリスト状態を保持します。
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