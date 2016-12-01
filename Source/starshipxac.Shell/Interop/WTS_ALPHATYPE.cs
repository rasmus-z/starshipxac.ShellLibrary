using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     Thumbnail alpha types.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb774612(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum WTS_ALPHATYPE
    {
        /// <summary>
        ///     The bitmap is an unknown format.
        ///     The Shell tries nonetheless to detect whether the image has an alpha channel.
        /// </summary>
        WTSAT_UNKNOWN = 0,

        /// <summary>
        ///     The bitmap is an RGB image without alpha.
        ///     The alpha channel is invalid and the Shell ignores it.
        /// </summary>
        WTSAT_RGB = 1,

        /// <summary>
        ///     The bitmap is an ARGB image with a valid alpha channel.
        /// </summary>
        WTSAT_ARGB = 2
    }
}