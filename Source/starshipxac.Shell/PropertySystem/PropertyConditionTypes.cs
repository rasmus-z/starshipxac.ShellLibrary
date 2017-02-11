using System;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    ///     Define the condition type to use when displaying properties in the Query Builder user interface.
    /// </summary>
    public enum PropertyConditionTypes
    {
        /// <summary>
        ///     Default condition type.
        /// </summary>
        None = PROPDESC_CONDITION_TYPE.PDCOT_NONE,

        /// <summary>
        ///     String.
        /// </summary>
        String = PROPDESC_CONDITION_TYPE.PDCOT_STRING,

        /// <summary>
        ///     Size.
        /// </summary>
        Size = PROPDESC_CONDITION_TYPE.PDCOT_SIZE,

        /// <summary>
        ///     Date and time.
        /// </summary>
        DateTime = PROPDESC_CONDITION_TYPE.PDCOT_DATETIME,

        /// <summary>
        ///     Boolean value.
        /// </summary>
        Boolean = PROPDESC_CONDITION_TYPE.PDCOT_BOOLEAN,

        /// <summary>
        ///     Number value.
        /// </summary>
        Number = PROPDESC_CONDITION_TYPE.PDCOT_NUMBER,
    }
}