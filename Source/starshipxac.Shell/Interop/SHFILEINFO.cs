using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    /// シェルファイル情報を定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb759792(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal struct SHFILEINFO
    {
        internal IntPtr hIcon;
        internal int iIcon;
        internal UInt32 dwAttributes;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        internal string szDisplayName;

        [MarshalAs(UnmanagedType.LPWStr, SizeConst = 80)]
        internal string szTypeName;
    }
}