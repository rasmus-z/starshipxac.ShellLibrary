using System;
using System.Diagnostics.CodeAnalysis;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    ///     Define property description format flags.
    /// </summary>
    [Flags]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum PropertyDescriptionFormatFlags
    {
        /// <summary>
        ///     Use the formatting specified in the property's <c>.propdesc</c> file.
        /// </summary>
        Default = PROPDESC_FORMAT_FLAGS.PDFF_DEFAULT,

        /// <summary>
        ///     Prefix the property display name with the value.
        /// </summary>
        /// <remarks>
        ///     If the <c>hideLabelPrefix </c> attribute of the <c>labelInfo</c> element in the property's <c>.propinfo</c> file is
        ///     set to <c>true</c> Flags are ignored.
        /// </remarks>
        PrefixName = PROPDESC_FORMAT_FLAGS.PDFF_PREFIXNAME,

        /// <summary>
        ///     Create a character string as a file name.
        /// </summary>
        FileName = PROPDESC_FORMAT_FLAGS.PDFF_FILENAME,

        /// <summary>
        ///     Always set the byte size to kilobytes.
        /// </summary>
        AlwaysKB = PROPDESC_FORMAT_FLAGS.PDFF_ALWAYSKB,

        /// <summary>
        ///     Reserved.
        /// </summary>
        RightToLeft = PROPDESC_FORMAT_FLAGS.PDFF_RESERVED_RIGHTTOLEFT,

        /// <summary>
        ///     Create a short form of time string.
        ///     <para>
        ///         Ex: <c>'hh:mm am/pm'</c>
        ///     </para>
        /// </summary>
        ShortTime = PROPDESC_FORMAT_FLAGS.PDFF_SHORTTIME,

        /// <summary>
        ///     Create a long form of time string.
        ///     <para>
        ///         Ex: <c>'hh:mm:ss am/pm'</c>
        ///     </para>
        /// </summary>
        LongTime = PROPDESC_FORMAT_FLAGS.PDFF_LONGTIME,

        /// <summary>
        ///     Hide the time part of the date and time.
        /// </summary>
        HideTime = PROPDESC_FORMAT_FLAGS.PDFF_HIDETIME,

        /// <summary>
        ///     Create a short date string.
        ///     <para>
        ///         Ex: <c>'3/21/04'</c>
        ///     </para>
        /// </summary>
        ShortDate = PROPDESC_FORMAT_FLAGS.PDFF_SHORTDATE,

        /// <summary>
        ///     Create a long date style string.
        ///     <para>
        ///         Ex: <c>'Monday, March 21, 2004'</c>
        ///     </para>
        /// </summary>
        LongDate = PROPDESC_FORMAT_FLAGS.PDFF_LONGDATE,

        /// <summary>
        ///     Hide the date part of the date and time.
        /// </summary>
        HideDate = PROPDESC_FORMAT_FLAGS.PDFF_HIDEDATE,

        /// <summary>
        ///     Create a date like "Yesterday".
        /// </summary>
        RelativeDate = PROPDESC_FORMAT_FLAGS.PDFF_RELATIVEDATE,

        /// <summary>
        ///     A character string displayed in the text box as a clue for the user such as "Please enter your name".
        /// </summary>
        UseEditInvitation = PROPDESC_FORMAT_FLAGS.PDFF_USEEDITINVITATION,

        /// <summary>
        ///     When using this flag, you also need to specify the <see cref="UseEditInvitation" /> flag.
        ///     If the formatting flag is <see cref="ReadOnly" />, <see cref="UseEditInvitation" /> returns a string
        ///     that is "unknown".
        /// </summary>
        ReadOnly = PROPDESC_FORMAT_FLAGS.PDFF_READONLY,

        /// <summary>
        ///     It does not automatically detect reading order.
        /// </summary>
        NoAutoReadingOrder = PROPDESC_FORMAT_FLAGS.PDFF_NOAUTOREADINGORDER,
    }
}