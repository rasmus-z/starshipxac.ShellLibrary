using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     The direction in which the items are sorted.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb759817(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum SORTDIRECTION
    {
        /// <summary>
        ///     The items are sorted in ascending order.
        ///     Whether the sort is alphabetical, numerical, and so on, is determined by the data type of the column indicated in propkey.
        /// </summary>
        SORT_DESCENDING = -1,

        /// <summary>
        ///     The items are sorted in descending order.
        ///     Whether the sort is alphabetical, numerical, and so on, is determined by the data type of the column indicated in propkey.
        /// </summary>
        SORT_ASCENDING = 1
    }
}