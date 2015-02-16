using System;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    /// ソート順を定義します。
    /// </summary>
    public enum PropertySortDescription
    {
        /// <summary>
        /// 規定の昇順または降順のソート。
        /// </summary>
        General = PROPDESC_SORTDESCRIPTION.PDSD_GENERAL,

        /// <summary>
        /// アルファベット順。
        /// </summary>
        AToZ = PROPDESC_SORTDESCRIPTION.PDSD_A_Z,

        /// <summary>
        /// 低い順または高い順。
        /// </summary>
        LowestToHighest = PROPDESC_SORTDESCRIPTION.PDSD_LOWEST_HIGHEST,

        /// <summary>
        /// 小さい順または大きい順。
        /// </summary>
        SmallestToBiggest = PROPDESC_SORTDESCRIPTION.PDSD_SMALLEST_BIGGEST,

        /// <summary>
        /// 古い順または新しい順。
        /// </summary>
        OldestToNewest = PROPDESC_SORTDESCRIPTION.PDSD_OLDEST_NEWEST,
    }
}