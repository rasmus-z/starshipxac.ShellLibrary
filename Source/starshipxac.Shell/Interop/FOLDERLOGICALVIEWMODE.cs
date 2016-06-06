using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb762509(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum FOLDERLOGICALVIEWMODE
    {
        FLVM_UNSPECIFIED = -1,
        FLVM_FIRST = 1,
        FLVM_DETAILS = 1,
        FLVM_TILES = 2,
        FLVM_ICONS = 3,
        FLVM_LIST = 4,
        FLVM_CONTENT = 5,
        FLVM_LAST = 5
    }
}