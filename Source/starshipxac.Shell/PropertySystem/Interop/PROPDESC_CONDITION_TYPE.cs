using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    ///     Describes the condition type to use when displaying the property in the query builder UI
    ///     in Windows Vista, but not in Windows 7 and later.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb762523(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum PROPDESC_CONDITION_TYPE
    {
        /// <summary>
        ///     The default value; it means the condition type is unspecified.
        /// </summary>
        PDCOT_NONE = 0,

        /// <summary>
        ///     Use the string condition type.
        /// </summary>
        PDCOT_STRING = 1,

        /// <summary>
        ///     Use the size condition type.
        /// </summary>
        PDCOT_SIZE = 2,

        /// <summary>
        ///     Use the date/time condition type.
        /// </summary>
        PDCOT_DATETIME = 3,

        /// <summary>
        ///     Use the Boolean condition type.
        /// </summary>
        PDCOT_BOOLEAN = 4,

        /// <summary>
        ///     Use the number condition type.
        /// </summary>
        PDCOT_NUMBER = 5
    }
}