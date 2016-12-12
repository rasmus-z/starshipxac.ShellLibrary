using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    ///
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb761551(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum PROPDESC_SORTDESCRIPTION
    {
        /// <summary>
        ///     Default. "Sort going up", "Sort going down"
        /// </summary>
        PDSD_GENERAL = 0,

        /// <summary>
        ///     "A on top", "Z on top"
        /// </summary>
        PDSD_A_Z = 1,

        /// <summary>
        ///     "Lowest on top", "Highest on top"
        /// </summary>
        PDSD_LOWEST_HIGHEST = 2,

        /// <summary>
        ///     "Smallest on top", "Largest on top"
        /// </summary>
        PDSD_SMALLEST_BIGGEST = 3,

        /// <summary>
        ///     "Oldest on top", "Newest on top"
        /// </summary>
        PDSD_OLDEST_NEWEST = 4
    }
}