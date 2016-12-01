using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     Contains a unique identifier for a thumbnail in the system thumbnail cache.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb759843(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal struct WTS_THUMBNAILID
    {
        /// <summary>
        ///     An array of 16 bytes that make up a unique identifier for a thumbnail in the system thumbnail cache.
        /// </summary>
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 16)]
        internal byte rgbKey;
    }
}