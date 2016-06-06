using System;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    ///     グループ化範囲を定義します。
    /// </summary>
    public enum PropertyGroupingRange
    {
        /// <summary>
        ///     個別の値。
        /// </summary>
        Discrete = PROPDESC_GROUPING_RANGE.PDGR_DISCRETE,

        /// <summary>
        ///     英数字の範囲。
        /// </summary>
        Alphanumeric = PROPDESC_GROUPING_RANGE.PDGR_ALPHANUMERIC,

        /// <summary>
        ///     サイズの範囲。
        /// </summary>
        Size = PROPDESC_GROUPING_RANGE.PDGR_SIZE,

        /// <summary>
        ///     動的な範囲。
        /// </summary>
        Dynamic = PROPDESC_GROUPING_RANGE.PDGR_DYNAMIC,

        /// <summary>
        ///     年と月のグループ。
        /// </summary>
        Date = PROPDESC_GROUPING_RANGE.PDGR_DATE,

        /// <summary>
        ///     パーセントグループ。
        /// </summary>
        Percent = PROPDESC_GROUPING_RANGE.PDGR_PERCENT,

        /// <summary>
        ///     列挙値グループ。
        /// </summary>
        Enumerated = PROPDESC_GROUPING_RANGE.PDGR_ENUMERATED,
    }
}