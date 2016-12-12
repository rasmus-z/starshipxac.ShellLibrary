using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    ///     Gets the grouping method to be used when a view is grouped by a property,
    ///     and retrieves the grouping type.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb761542(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum PROPDESC_GROUPING_RANGE
    {
        /// <summary>
        ///     Displays individual values.
        /// </summary>
        PDGR_DISCRETE = 0,

        /// <summary>
        ///     Displays static alphanumeric ranges.
        /// </summary>
        PDGR_ALPHANUMERIC = 1,

        /// <summary>
        ///     Displays static size ranges.
        /// </summary>
        PDGR_SIZE = 2,

        /// <summary>
        ///     Displays dynamically created ranges.
        /// </summary>
        PDGR_DYNAMIC = 3,

        /// <summary>
        ///     Displays month and year groups.
        /// </summary>
        PDGR_DATE = 4,

        /// <summary>
        ///     Displays percent groups.
        /// </summary>
        PDGR_PERCENT = 5,

        /// <summary>
        ///     Displays percent groups returned by <c>IPropertyDescription.GetEnumTypeList</c>.
        /// </summary>
        PDGR_ENUMERATED = 6
    }
}