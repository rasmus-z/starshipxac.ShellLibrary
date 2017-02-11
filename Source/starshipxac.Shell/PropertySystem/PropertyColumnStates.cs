using System;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    ///     Define how to display properties.
    /// </summary>
    [Flags]
    public enum PropertyColumnStates : uint
    {
        /// <summary>
        ///     Default.
        /// </summary>
        Default = SHCOLSTATE.SHCOLSTATE_DEFAULT,

        /// <summary>
        ///     Display string.
        /// </summary>
        StringType = SHCOLSTATE.SHCOLSTATE_TYPE_STR,

        /// <summary>
        ///     Display integer value.
        /// </summary>
        IntegerType = SHCOLSTATE.SHCOLSTATE_TYPE_INT,

        /// <summary>
        ///     Display date.
        /// </summary>
        DateType = SHCOLSTATE.SHCOLSTATE_TYPE_DATE,

        /// <summary>
        ///     Mask <c>StringType</c>, <c>IntegerType</c> and <c>DateType</c>.
        /// </summary>
        TypeMask = SHCOLSTATE.SHCOLSTATE_TYPEMASK,

        OnByDefault = SHCOLSTATE.SHCOLSTATE_ONBYDEFAULT,

        Slow = SHCOLSTATE.SHCOLSTATE_SLOW,

        Extended = SHCOLSTATE.SHCOLSTATE_EXTENDED,

        SecondaryUI = SHCOLSTATE.SHCOLSTATE_SECONDARYUI,

        Hidden = SHCOLSTATE.SHCOLSTATE_HIDDEN,

        PreferVariantCompare = SHCOLSTATE.SHCOLSTATE_PREFER_VARCMP,

        PreferFormatForDisplay = SHCOLSTATE.SHCOLSTATE_PREFER_FMTCMP,

        NoSortByFolders = SHCOLSTATE.SHCOLSTATE_NOSORTBYFOLDERNESS,

        ViewOnly = SHCOLSTATE.SHCOLSTATE_VIEWONLY,

        BatchRead = SHCOLSTATE.SHCOLSTATE_BATCHREAD,

        NoGroupBy = SHCOLSTATE.SHCOLSTATE_NO_GROUPBY,

        FixedWidth = SHCOLSTATE.SHCOLSTATE_FIXED_WIDTH,

        NoDpiScale = SHCOLSTATE.SHCOLSTATE_NODPISCALE,

        FixedRatio = SHCOLSTATE.SHCOLSTATE_FIXED_RATIO,

        DisplayMask = SHCOLSTATE.SHCOLSTATE_DISPLAYMASK,
    }
}