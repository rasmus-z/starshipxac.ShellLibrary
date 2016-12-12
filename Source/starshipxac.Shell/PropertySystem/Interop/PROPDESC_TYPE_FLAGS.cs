using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    ///     Describes attributes of the typeInfo element in the property's .propdesc file.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb762527(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum PROPDESC_TYPE_FLAGS
    {
        /// <summary>
        ///     The property uses the default values for all attributes.
        /// </summary>
        PDTF_DEFAULT = 0,

        /// <summary>
        ///     The property can have multiple values.
        /// </summary>
        PDTF_MULTIPLEVALUES = 0x1,

        /// <summary>
        ///     This flag indicates that a property is read-only, and cannot be written to. This value is set by the isInnate attribute of the <c>typeInfo</c> element in the property's .propdesc file.
        /// </summary>
        PDTF_ISINNATE = 0x2,

        /// <summary>
        ///     The property is a group heading. 
        /// </summary>
        PDTF_ISGROUP = 0x4,

        /// <summary>
        ///     The user can group by this property.
        ///     This value is set by the canGroupBy attribute of the typeInfo element in the property's .propdesc file.
        /// </summary>
        PDTF_CANGROUPBY = 0x8,

        /// <summary>
        ///     The user can stack by this property.
        ///     This value is set by the canStackBy attribute of the typeInfo element in the property's .propdesc file.
        /// </summary>
        PDTF_CANSTACKBY = 0x10,

        /// <summary>
        ///     This property contains a hierarchy. This value is set by the <c>isTreeProperty</c>
        ///     attribute of the typeInfo element in the property's .propdesc file.
        /// </summary>
        PDTF_ISTREEPROPERTY = 0x20,

        /// <summary>
        ///     Include this property in any full text query that is performed.
        ///     This value is set by the <c>includeInFullTextQuery</c> attribute
        ///     of the <c>typeInfo</c> element in the property's .propdesc file.
        /// </summary>
        PDTF_INCLUDEINFULLTEXTQUERY = 0x40,

        /// <summary>
        ///     This property is meant to be viewed by the user.
        ///     This influences whether the property shows up in the "Choose Columns" dialog box,
        ///     for example. This value is set by the isViewable attribute of the typeInfo element in the property's .propdesc file.
        /// </summary>
        PDTF_ISVIEWABLE = 0x80,

        /// <summary>
        ///     This property is included in the list of properties that can be queried.
        ///     A queryable property must also be viewable.
        ///     This influences whether the property shows up in the query builder UI.
        ///     This value is set by the isQueryable attribute of the typeInfo element in the property's .propdesc file.
        /// </summary>
        PDTF_ISQUERYABLE = 0x100,

        /// <summary>
        ///     Used with an innate property (that is, a value calculated from other property values) to indicate that it can be deleted.
        ///     This value is used by the Remove Properties UI to determine whether to display a check box next to a property that enables that property to be selected for removal.
        ///     Note that a property that is not innate can always be purged regardless of the presence or absence of this flag.
        /// </summary>
        PDTF_CANBEPURGED = 0x200,

        /// <summary>
        ///     The unformatted (raw) property value should be used for searching.
        /// </summary>
        PDTF_SEARCHRAWVALUE = 0x400,

        /// <summary>
        ///     This property is owned by the system.
        /// </summary>
        PDTF_ISSYSTEMPROPERTY = unchecked((int)0x80000000),

        /// <summary>
        ///     A mask used to retrieve all flags.
        /// </summary>
        PDTF_MASK_ALL = unchecked((int)0x800007ff),
    }
}