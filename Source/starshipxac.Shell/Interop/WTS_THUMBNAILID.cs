using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     サムネイルキャッシュのサムネイルIDを定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb759843(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal struct WTS_THUMBNAILID
    {
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 16)]
        internal byte rgbKey;
    }
}