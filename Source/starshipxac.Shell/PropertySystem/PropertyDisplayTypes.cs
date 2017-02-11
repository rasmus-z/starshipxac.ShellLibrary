using System;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    ///     Define property display types.
    /// </summary>
    public enum PropertyDisplayTypes
    {
        /// <summary>
        ///     String
        /// </summary>
        String = PROPDESC_DISPLAYTYPE.PDDT_STRING,

        /// <summary>
        ///     Number
        /// </summary>
        Number = PROPDESC_DISPLAYTYPE.PDDT_NUMBER,

        /// <summary>
        ///     Boolean
        /// </summary>
        Boolean = PROPDESC_DISPLAYTYPE.PDDT_BOOLEAN,

        /// <summary>
        ///     Date and time
        /// </summary>
        DateTime = PROPDESC_DISPLAYTYPE.PDDT_DATETIME,

        /// <summary>
        ///     Enumerated
        /// </summary>
        Enumerated = PROPDESC_DISPLAYTYPE.PDDT_ENUMERATED
    }
}