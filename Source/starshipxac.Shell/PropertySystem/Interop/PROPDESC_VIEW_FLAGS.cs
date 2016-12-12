using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    ///     These flags describe properties in property description list strings.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb762528(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum PROPDESC_VIEW_FLAGS
    {
        /// <summary>
        ///     Show this property by default.
        /// </summary>
        PDVF_DEFAULT = 0,

        /// <summary>
        ///     This property should be centered.
        /// </summary>
        PDVF_CENTERALIGN = 0x1,

        /// <summary>
        ///     This property should be right aligned.
        /// </summary>
        PDVF_RIGHTALIGN = 0x2,

        /// <summary>
        ///     Show this property as the beginning of the next collection of properties in the view.
        /// </summary>
        PDVF_BEGINNEWGROUP = 0x4,

        /// <summary>
        ///     Fill the remainder of the view area with the content of this property.
        /// </summary>
        PDVF_FILLAREA = 0x8,

        /// <summary>
        ///     Sort this property in reverse (descending) order.
        ///     Applies to a property in a list of sorted properties.
        /// </summary>
        PDVF_SORTDESCENDING = 0x10,

        /// <summary>
        ///     Show this property only if it is present.
        /// </summary>
        PDVF_SHOWONLYIFPRESENT = 0x20,

        /// <summary>
        ///     This property should be shown by default in a view (where applicable).
        /// </summary>
        PDVF_SHOWBYDEFAULT = 0x40,

        /// <summary>
        ///     This property should be shown by default in the primary column selection UI.
        /// </summary>
        PDVF_SHOWINPRIMARYLIST = 0x80,

        /// <summary>
        ///     This property should be shown by default in the secondary column selection UI.
        /// </summary>
        PDVF_SHOWINSECONDARYLIST = 0x100,

        /// <summary>
        ///     Hide the label of this property if the view normally shows the label.
        /// </summary>
        PDVF_HIDELABEL = 0x200,

        /// <summary>
        ///     This property should not be displayed as a column in the UI.
        /// </summary>
        PDVF_HIDDEN = 0x800,

        /// <summary>
        ///     This property can be wrapped to the next row.
        /// </summary>
        PDVF_CANWRAP = 0x1000,

        /// <summary>
        ///     A mask used to retrieve all flags.
        /// </summary>
        PDVF_MASK_ALL = 0x1bff
    }
}