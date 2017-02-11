using System;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    ///     Define the attributes of the <c>TypeInfo</c> element in the property's <c>.propdesc</c> file.
    /// </summary>
    [Flags]
    public enum PropertyTypeFlags
    {
        Default = PROPDESC_TYPE_FLAGS.PDTF_DEFAULT,

        MultipleValues = PROPDESC_TYPE_FLAGS.PDTF_MULTIPLEVALUES,

        IsInnate = PROPDESC_TYPE_FLAGS.PDTF_ISINNATE,

        IsGroup = PROPDESC_TYPE_FLAGS.PDTF_ISGROUP,

        CanGroupBy = PROPDESC_TYPE_FLAGS.PDTF_CANGROUPBY,

        CanStackBy = PROPDESC_TYPE_FLAGS.PDTF_CANSTACKBY,

        IsTreeProperty = PROPDESC_TYPE_FLAGS.PDTF_ISTREEPROPERTY,

        IncludeInFullTextQuery = PROPDESC_TYPE_FLAGS.PDTF_INCLUDEINFULLTEXTQUERY,

        IsViewable = PROPDESC_TYPE_FLAGS.PDTF_ISVIEWABLE,

        IsQueryable = PROPDESC_TYPE_FLAGS.PDTF_ISQUERYABLE,

        CanBePurged = PROPDESC_TYPE_FLAGS.PDTF_CANBEPURGED,

        IsSystemProperty = PROPDESC_TYPE_FLAGS.PDTF_ISSYSTEMPROPERTY,

        MaskAll = PROPDESC_TYPE_FLAGS.PDTF_MASK_ALL,
    }
}