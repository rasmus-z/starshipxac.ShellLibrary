using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    ///     Describes how property values are displayed when multiple items are selected.
    ///     For a particular property, <c>PROPDESC_AGGREGATION_TYPE</c> describes how the property should be displayed
    ///     when multiple items that have a value for the property are selected,
    ///     such as whether the values should be summed, or averaged, or just displayed with the default "Multiple Values" string.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb762522(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum PROPDESC_AGGREGATION_TYPE
    {
        /// <summary>
        ///     Display the string "Multiple Values".
        /// </summary>
        PDAT_DEFAULT = 0,

        /// <summary>
        ///     Display the first value in the selection.
        /// </summary>
        PDAT_FIRST = 1,

        /// <summary>
        ///     Display the sum of the selected values.
        ///     This flag is never returned for data types <c>VT_LPWSTR</c>, <c>VT_BOOL</c>, and <c>VT_FILETIME</c>.
        /// </summary>
        PDAT_SUM = 2,

        /// <summary>
        ///     Display the numerical average of the selected values.
        ///     This flag is never returned for data types <c>VT_LPWSTR</c>, <c>VT_BOOL</c>, and <c>VT_FILETIME</c>.
        /// </summary>
        PDAT_AVERAGE = 3,

        /// <summary>
        ///     Display the date range of the selected values.
        ///     This flag is returned only for values of the <c>VT_FILETIME</c> data type.
        /// </summary>
        PDAT_DATERANGE = 4,

        /// <summary>
        ///     Display a concatenated string of all the values.
        ///     The order of individual values in the string is undefined.
        ///     The concatenated string omits duplicate values;
        ///     if a value occurs more than once, it appears only once in the concatenated string.
        /// </summary>
        PDAT_UNION = 5,

        /// <summary>
        ///     Display the highest of the selected values.
        /// </summary>
        PDAT_MAX = 6,

        /// <summary>
        ///     Display the lowest of the selected values.
        /// </summary>
        PDAT_MIN = 7
    }
}