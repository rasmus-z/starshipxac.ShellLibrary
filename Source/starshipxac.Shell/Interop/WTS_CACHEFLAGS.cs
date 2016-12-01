using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     Thumbnail cache types.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb774624(v=vs.85).aspx
    /// </remarks>
    [Flags]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum WTS_CACHEFLAGS
    {
        /// <summary>
        ///     None.
        /// </summary>
        WTS_DEFAULT = 0,

        /// <summary>
        ///     Set when the returned bitmap dimensions are less than <c>cxyRequestedThumbSize</c>.
        /// </summary>
        WTS_LOWQUALITY = 0x1,

        /// <summary>
        ///     Set when the returned image is in the cache.
        /// </summary>
        WTS_CACHED = 0x2
    }
}