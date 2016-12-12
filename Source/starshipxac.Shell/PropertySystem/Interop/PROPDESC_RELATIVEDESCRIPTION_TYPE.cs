using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    ///     Describes the relative description type for a property description,
    ///     as determined by the relativeDescriptionType attribute of the displayInfo element.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb762526(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum PROPDESC_RELATIVEDESCRIPTION_TYPE
    {
        /// <summary>
        ///     General type.
        /// </summary>
        PDRDT_GENERAL = 0,

        /// <summary>
        ///     Date type.
        /// </summary>
        PDRDT_DATE = 1,

        /// <summary>
        ///     Size type.
        /// </summary>
        PDRDT_SIZE = 2,

        /// <summary>
        ///     Count type.
        /// </summary>
        PDRDT_COUNT = 3,

        /// <summary>
        ///     Revision type.
        /// </summary>
        PDRDT_REVISION = 4,

        /// <summary>
        ///     Length type.
        /// </summary>
        PDRDT_LENGTH = 5,

        /// <summary>
        ///     Duration type.
        /// </summary>
        PDRDT_DURATION = 6,

        /// <summary>
        ///     Speed type.
        /// </summary>
        PDRDT_SPEED = 7,

        /// <summary>
        ///     Rate type.
        /// </summary>
        PDRDT_RATE = 8,

        /// <summary>
        ///     Rating type.
        /// </summary>
        PDRDT_RATING = 9,

        /// <summary>
        ///     Priority type.
        /// </summary>
        PDRDT_PRIORITY = 10
    }
}