using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.IO.Interop
{
    /// <summary>
    ///     The <see cref="STREAM_SEEK"/> enumeration values specify the origin from which to calculate the new seek-pointer location.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/aa380359(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum STREAM_SEEK
    {
        /// <summary>
        ///     The new seek pointer is an offset relative to the beginning of the stream.
        ///     In this case, the <c>dlibMove</c> parameter is the new seek position relative to the beginning of the stream.
        /// </summary>
        STREAM_SEEK_SET = 0,

        /// <summary>
        ///     The new seek pointer is an offset relative to the current seek pointer location.
        ///     In this case, the <c>dlibMove</c> parameter is the signed displacement from the current seek position.
        /// </summary>
        STREAM_SEEK_CUR = 1,

        /// <summary>
        ///     The new seek pointer is an offset relative to the end of the stream.
        ///     In this case, the <c>dlibMove</c> parameter is the new seek position relative to the end of the stream.
        /// </summary>
        STREAM_SEEK_END = 2
    }
}