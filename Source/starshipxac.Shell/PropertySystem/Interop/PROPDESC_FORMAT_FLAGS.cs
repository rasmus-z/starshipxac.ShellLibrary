using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    ///     Used by property description helper functions, such as <c>PSFormatForDisplay</c>,
    ///     to indicate the format of a property string.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb762525(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum PROPDESC_FORMAT_FLAGS
    {
        /// <summary>
        ///     Use the format settings specified in the property's .propdesc file.
        /// </summary>
        PDFF_DEFAULT = 0,

        /// <summary>
        ///     Precede the value with the property's display name.
        ///     If the hideLabelPrefix attribute of the labelInfo element in the property's .propinfo file is set to true, then this flag is ignored.
        /// </summary>
        PDFF_PREFIXNAME = 0x1,

        /// <summary>
        ///     Treat the string as a file name.
        /// </summary>
        PDFF_FILENAME = 0x2,

        /// <summary>
        ///     Byte sizes are always displayed in KB, regardless of size.
        ///     This enables clean alignment of the values in the column.
        ///     This flag applies only to properties that have been declared as type Integer in the displayType attribute of the displayInfo element in the property's .propinfo file. This flag overrides the numberFormat setting.
        /// </summary>
        PDFF_ALWAYSKB = 0x4,

        /// <summary>
        ///     Reserved.
        /// </summary>
        PDFF_RESERVED_RIGHTTOLEFT = 0x8,

        /// <summary>
        ///     Display time as "hh:mm am/pm".
        /// </summary>
        PDFF_SHORTTIME = 0x10,

        /// <summary>
        ///     Display time as "hh:mm:ss am/pm".
        /// </summary>
        PDFF_LONGTIME = 0x20,

        /// <summary>
        ///     Hide the time portion of datetime.
        /// </summary>
        PDFF_HIDETIME = 0x40,

        /// <summary>
        ///     Display date as "MM/DD/YY". For example, "03/21/04".
        /// </summary>
        PDFF_SHORTDATE = 0x80,

        /// <summary>
        ///     Display date as "DayOfWeek, Month day, year". For example, "Monday, March 21, 2009".
        /// </summary>
        PDFF_LONGDATE = 0x100,

        /// <summary>
        /// Hide the date portion of datetime
        /// </summary>
        PDFF_HIDEDATE = 0x200,

        /// <summary>
        ///     Use friendly date descriptions. For example, "Yesterday".
        /// </summary>
        PDFF_RELATIVEDATE = 0x400,

        /// <summary>
        ///     Return the invitation text if formatting failed or the value was empty.
        ///     Invitation text is text displayed in a text box as a cue for the user, such as "Enter your name".
        ///     Formatting can fail if the data entered is not of an expected type, such as when alpha characters have been entered in a phone-number field.
        /// </summary>
        PDFF_USEEDITINVITATION = 0x800,

        /// <summary>
        ///     If this flag is used, the <c>PDFF_USEEDITINVITATION</c> flag must also be specified.
        ///     When the formatting flags are <c>PDFF_READONLY</c> | <c>PDFF_USEEDITINVITATION</c>
        ///     and the algorithm would have shown invitation text, a string is returned that indicates the value is "Unknown" instead of returning the invitation text.
        /// </summary>
        PDFF_READONLY = 0x1000,

        /// <summary>
        ///     Do not detect reading order automatically.
        ///     Useful when converting to ANSI to omit the Unicode reading order characters.
        ///     However, reading order characters for some values are still returned.
        /// </summary>
        PDFF_NOAUTOREADINGORDER = 0x2000
    }
}