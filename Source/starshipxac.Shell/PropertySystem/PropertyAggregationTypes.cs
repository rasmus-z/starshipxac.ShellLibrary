using System;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    ///     Define how to display property values when multiple items are selected.
    /// </summary>
    public enum PropertyAggregationTypes
    {
        /// <summary>
        ///     Display "Multiple Values" string.
        /// </summary>
        Default = PROPDESC_AGGREGATION_TYPE.PDAT_DEFAULT,

        /// <summary>
        ///     Display first selected value.
        /// </summary>
        First = PROPDESC_AGGREGATION_TYPE.PDAT_FIRST,

        /// <summary>
        ///     Displays the total of the selected values.
        ///     <para>
        ///         (The type is other than <c>VT_LPWSTR</c>, <c>VT_BOOL</c> and <c>VT_FILETIME</c>)
        ///     </para>
        /// </summary>
        Sum = PROPDESC_AGGREGATION_TYPE.PDAT_SUM,

        /// <summary>
        ///     Display the average of the selected values.
        ///     <para>
        ///         (The type is other than <c>VT_LPWSTR</c>, <c>VT_BOOL</c> and <c>VT_FILETIME</c>)
        ///     </para>
        /// </summary>
        Average = PROPDESC_AGGREGATION_TYPE.PDAT_AVERAGE,

        /// <summary>
        ///     Displays the time range of the selected value.
        ///     <para>
        ///         (The type is <c>VT_FILETIME</c>)
        ///     </para>
        /// </summary>
        DateRange = PROPDESC_AGGREGATION_TYPE.PDAT_DATERANGE,

        /// <summary>
        ///     Displays a concatenated string of all values.
        ///     The order of the individual values in the string is undefined.
        ///     <para>
        ///         Duplicate values are omitted.
        ///         If the value occurs more than once, it will be displayed only once.
        ///     </para>
        /// </summary>
        Union = PROPDESC_AGGREGATION_TYPE.PDAT_UNION,

        /// <summary>
        ///     Display the maximum value of the selected value.
        /// </summary>
        Max = PROPDESC_AGGREGATION_TYPE.PDAT_MAX,

        /// <summary>
        ///     Display the minimal value of the selected value.
        /// </summary>
        Min = PROPDESC_AGGREGATION_TYPE.PDAT_MIN
    }
}