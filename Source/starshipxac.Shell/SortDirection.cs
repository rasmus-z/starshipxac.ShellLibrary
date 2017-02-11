using System;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell
{
    /// <summary>
    ///     Define item sort direction.
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        ///     Default sort direction.
        /// </summary>
        Default = 0,

        /// <summary>
        ///     Descending sort direction.
        /// </summary>
        Descending = SORTDIRECTION.SORT_DESCENDING,

        /// <summary>
        ///     Ascending sort direction.
        /// </summary>
        Ascending = SORTDIRECTION.SORT_ASCENDING
    }
}