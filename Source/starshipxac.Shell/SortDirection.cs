using System;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell
{
    /// <summary>
    /// アイテムのソート方向を定義します。
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        /// デフォルトのソート方向。
        /// </summary>
        Default = 0,

        /// <summary>
        /// 逆順のソート方向。
        /// </summary>
        Descending = SORTDIRECTION.SORT_DESCENDING,

        /// <summary>
        /// 正順のソート方向。
        /// </summary>
        Ascending = SORTDIRECTION.SORT_ASCENDING,
    }
}