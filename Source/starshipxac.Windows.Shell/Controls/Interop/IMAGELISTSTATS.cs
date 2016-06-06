using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace starshipxac.Windows.Shell.Controls.Interop
{
    /// <summary>
    /// イメージリスト状態を保持します。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal struct IMAGELISTSTATS
    {
        public UInt32 cbSIze;
        public int cAlloc;
        public int cUsed;
        public int cStandby;
    }
}