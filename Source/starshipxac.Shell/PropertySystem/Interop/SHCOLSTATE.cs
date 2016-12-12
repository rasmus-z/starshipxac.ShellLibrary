using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    ///     Describes how a property should be treated.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb762538(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum SHCOLSTATE : uint
    {
        /// <summary>
        ///     The value is displayed according to default settings for the column.
        /// </summary>
        SHCOLSTATE_DEFAULT = 0,

        /// <summary>
        ///     The value is displayed as a string.
        /// </summary>
        SHCOLSTATE_TYPE_STR = 0x1,

        /// <summary>
        ///     The value is displayed as an integer.
        /// </summary>
        SHCOLSTATE_TYPE_INT = 0x2,

        /// <summary>
        ///     The value is displayed as a date/time.
        /// </summary>
        SHCOLSTATE_TYPE_DATE = 0x3,

        /// <summary>
        ///     A mask for display type values <see cref="SHCOLSTATE_TYPE_STR"/>, <see cref="SHCOLSTATE_TYPE_STR"/>, and <see cref="SHCOLSTATE_TYPE_DATE"/>.
        /// </summary>
        SHCOLSTATE_TYPEMASK = 0xf,

        /// <summary>
        ///     The column should be on by default in Details view.
        /// </summary>
        SHCOLSTATE_ONBYDEFAULT = 0x10,

        /// <summary>
        ///     Will be slow to compute. Perform on a background thread.
        /// </summary>
        SHCOLSTATE_SLOW = 0x20,

        /// <summary>
        ///     Provided by a handler, not the folder.
        /// </summary>
        SHCOLSTATE_EXTENDED = 0x40,

        /// <summary>
        ///     Not displayed in the context menu, but is listed in the <c>More...</c> dialog.
        /// </summary>
        SHCOLSTATE_SECONDARYUI = 0x80,

        /// <summary>
        ///     Not displayed in the UI.
        /// </summary>
        SHCOLSTATE_HIDDEN = 0x100,

        /// <summary>
        ///     VarCmp produces same result as <c>IShellFolder.CompareIDs</c>.
        /// </summary>
        SHCOLSTATE_PREFER_VARCMP = 0x200,

        /// <summary>
        ///     <c>PSFormatForDisplay</c> produces same result as <c>IShellFolder.CompareIDs</c>.
        /// </summary>
        SHCOLSTATE_PREFER_FMTCMP = 0x400,

        /// <summary>
        ///     Do not sort folders separately.
        /// </summary>
        SHCOLSTATE_NOSORTBYFOLDERNESS = 0x800,

        /// <summary>
        ///     Only displayed in the UI.
        /// </summary>
        SHCOLSTATE_VIEWONLY = 0x10000,

        /// <summary>
        ///     Marks columns with values that should be read in a batch.
        /// </summary>
        SHCOLSTATE_BATCHREAD = 0x20000,

        /// <summary>
        ///     Grouping is disabled for this column.
        /// </summary>
        SHCOLSTATE_NO_GROUPBY = 0x40000,

        /// <summary>
        ///     Can't resize the column.
        /// </summary>
        SHCOLSTATE_FIXED_WIDTH = 0x1000,

        /// <summary>
        ///     The width is the same in all dpi.
        /// </summary>
        SHCOLSTATE_NODPISCALE = 0x2000,

        /// <summary>
        ///     Fixed width and height ratio.
        /// </summary>
        SHCOLSTATE_FIXED_RATIO = 0x4000,

        /// <summary>
        ///     Filters out new display flags.
        /// </summary>
        SHCOLSTATE_DISPLAYMASK = 0xf000
    }
}