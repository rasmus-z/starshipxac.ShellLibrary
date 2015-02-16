using System;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    /// プロパティの表示方法を定義します。
    /// </summary>
    [Flags]
    public enum PropertyColumnStates : uint
    {
        /// <summary>
        /// 規定値。
        /// </summary>
        Default = SHCOLSTATE.SHCOLSTATE_DEFAULT,

        /// <summary>
        /// 文字列を表示します。
        /// </summary>
        StringType = SHCOLSTATE.SHCOLSTATE_TYPE_STR,

        /// <summary>
        /// 整数値を表示します。
        /// </summary>
        IntegerType = SHCOLSTATE.SHCOLSTATE_TYPE_INT,

        /// <summary>
        /// 時刻を表示します。
        /// </summary>
        DateType = SHCOLSTATE.SHCOLSTATE_TYPE_DATE,

        /// <summary>
        /// <c>StringType</c>, <c>IntegerType</c>および<c>DateType</c>をマスクします。
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