using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    ///     Specifies the state of a property.
    ///     They are set manually by the code that is hosting the in-memory property store cache.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb762531(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum PSC_STATE
    {
        /// <summary>
        ///     The property has not been altered.
        /// </summary>
        PSC_NORMAL = 0,

        /// <summary>
        ///     The requested property does not exist for the file or stream on
        ///     which the property handler was initialized.
        /// </summary>
        PSC_NOTINSOURCE = 1,

        /// <summary>
        ///     The property has been altered but has not yet been committed to the file or stream.
        /// </summary>
        PSC_DIRTY = 2,

        PSC_READONLY = 3
    }
}