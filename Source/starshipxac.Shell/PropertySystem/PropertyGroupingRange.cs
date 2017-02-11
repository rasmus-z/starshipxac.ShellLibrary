using System;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    ///     Define grouping range.
    /// </summary>
    public enum PropertyGroupingRange
    {
        /// <summary>
        ///     Individual values.
        /// </summary>
        Discrete = PROPDESC_GROUPING_RANGE.PDGR_DISCRETE,

        /// <summary>
        ///     A range of alphanumeric characters.
        /// </summary>
        Alphanumeric = PROPDESC_GROUPING_RANGE.PDGR_ALPHANUMERIC,

        /// <summary>
        ///     Size range.
        /// </summary>
        Size = PROPDESC_GROUPING_RANGE.PDGR_SIZE,

        /// <summary>
        ///     Dynamic range.
        /// </summary>
        Dynamic = PROPDESC_GROUPING_RANGE.PDGR_DYNAMIC,

        /// <summary>
        ///     A group of years and months.
        /// </summary>
        Date = PROPDESC_GROUPING_RANGE.PDGR_DATE,

        /// <summary>
        ///     Percent group.
        /// </summary>
        Percent = PROPDESC_GROUPING_RANGE.PDGR_PERCENT,

        /// <summary>
        ///     Enumerated value group.
        /// </summary>
        Enumerated = PROPDESC_GROUPING_RANGE.PDGR_ENUMERATED,
    }
}