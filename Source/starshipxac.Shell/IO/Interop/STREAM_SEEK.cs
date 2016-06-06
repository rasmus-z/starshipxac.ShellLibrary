using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.IO.Interop
{
    /// <summary>
    ///     ストリームシーク種別を定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/aa380359(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum STREAM_SEEK
    {
        STREAM_SEEK_SET = 0,
        STREAM_SEEK_CUR = 1,
        STREAM_SEEK_END = 2
    }
}