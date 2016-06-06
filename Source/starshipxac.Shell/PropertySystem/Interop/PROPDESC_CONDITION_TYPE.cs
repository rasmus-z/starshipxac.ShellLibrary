using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb762523(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum PROPDESC_CONDITION_TYPE
    {
        PDCOT_NONE = 0,
        PDCOT_STRING = 1,
        PDCOT_SIZE = 2,
        PDCOT_DATETIME = 3,
        PDCOT_BOOLEAN = 4,
        PDCOT_NUMBER = 5
    }
}