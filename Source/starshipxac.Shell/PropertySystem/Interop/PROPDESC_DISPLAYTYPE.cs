using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    ///     Contains a pointer to a value that indicates the display type.
    ///     One of the following values.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb761535(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum PROPDESC_DISPLAYTYPE
    {
        /// <summary>
        ///     The value is displayed as a string.
        /// </summary>
        PDDT_STRING = 0,

        /// <summary>
        ///     The value is displayed as an integer.
        /// </summary>
        PDDT_NUMBER = 1,

        /// <summary>
        ///     The value is displayed as a Boolean value.
        /// </summary>
        PDDT_BOOLEAN = 2,

        /// <summary>
        ///     The value is displayed as date and time.
        /// </summary>
        PDDT_DATETIME = 3,

        /// <summary>
        ///     The value is displayed as an enumerated type-list.
        ///     Use <see cref="IPropertyDescription.GetEnumTypeList(ref Guid, out IPropertyEnumTypeList)"/> to handle this type. 
        /// </summary>
        PDDT_ENUMERATED = 4
    }
}