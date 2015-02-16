using System;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    /// プロパティ表示方法を定義します。
    /// </summary>
    [Flags]
    public enum PropertyViewFlags
    {
        Default = PROPDESC_VIEW_FLAGS.PDVF_DEFAULT,

        CenterAlign = PROPDESC_VIEW_FLAGS.PDVF_CENTERALIGN,

        RightAlign = PROPDESC_VIEW_FLAGS.PDVF_RIGHTALIGN,

        BeginNewGroup = PROPDESC_VIEW_FLAGS.PDVF_BEGINNEWGROUP,

        FillArea = PROPDESC_VIEW_FLAGS.PDVF_FILLAREA,

        SortDescending = PROPDESC_VIEW_FLAGS.PDVF_SORTDESCENDING,

        ShowOnlyIfPresent = PROPDESC_VIEW_FLAGS.PDVF_SHOWONLYIFPRESENT,

        ShowByDefault = PROPDESC_VIEW_FLAGS.PDVF_SHOWBYDEFAULT,

        ShowInPrimaryList = PROPDESC_VIEW_FLAGS.PDVF_SHOWINPRIMARYLIST,

        ShowInSecondaryList = PROPDESC_VIEW_FLAGS.PDVF_SHOWINSECONDARYLIST,

        HideLabel = PROPDESC_VIEW_FLAGS.PDVF_HIDELABEL,

        Hidden = PROPDESC_VIEW_FLAGS.PDVF_HIDDEN,

        CanWrap = PROPDESC_VIEW_FLAGS.PDVF_CANWRAP,

        MaskAll = PROPDESC_VIEW_FLAGS.PDVF_MASK_ALL,
    }
}