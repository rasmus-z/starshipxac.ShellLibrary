using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace starshipxac.Windows.Shell.Media.Imaging.Interop
{
    /// <summary>
    ///     Define stock icon information struct.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb759805(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal struct SHSTOCKICONINFO
    {
        public static SHSTOCKICONINFO Create()
        {
            return new SHSTOCKICONINFO()
            {
                cbSize = (UInt32)Marshal.SizeOf(typeof(SHSTOCKICONINFO))
            };
        }

        public UInt32 cbSize;
        public IntPtr hIcon;
        public Int32 iSysImageIndex;
        public Int32 iIcon;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szPath;
    }
}