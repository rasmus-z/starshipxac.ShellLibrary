using System;
using System.Diagnostics.CodeAnalysis;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    ///     Define sort description.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum PropertySortDescription
    {
        /// <summary>
        ///     Sort by prescribed ascending or descending order.
        /// </summary>
        General = PROPDESC_SORTDESCRIPTION.PDSD_GENERAL,

        /// <summary>
        ///     Alphabetical order.
        /// </summary>
        AToZ = PROPDESC_SORTDESCRIPTION.PDSD_A_Z,

        /// <summary>
        ///     Order in ascending or descending order.
        /// </summary>
        LowestToHighest = PROPDESC_SORTDESCRIPTION.PDSD_LOWEST_HIGHEST,

        /// <summary>
        ///     In ascending order or descending order.
        /// </summary>
        SmallestToBiggest = PROPDESC_SORTDESCRIPTION.PDSD_SMALLEST_BIGGEST,

        /// <summary>
        ///     Order by oldest or newest.
        /// </summary>
        OldestToNewest = PROPDESC_SORTDESCRIPTION.PDSD_OLDEST_NEWEST,
    }
}