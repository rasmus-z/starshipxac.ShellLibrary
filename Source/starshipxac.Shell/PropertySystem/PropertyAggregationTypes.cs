using System;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    ///     複数の項目が選択されている場合のプロパティ値の表示方法を定義します。
    /// </summary>
    public enum PropertyAggregationTypes
    {
        /// <summary>
        ///     文字列 "Multiple Values"を表示します。
        /// </summary>
        Default = PROPDESC_AGGREGATION_TYPE.PDAT_DEFAULT,

        /// <summary>
        ///     最初に選択した値を表示します。
        /// </summary>
        First = PROPDESC_AGGREGATION_TYPE.PDAT_FIRST,

        /// <summary>
        ///     選択した値の合計を表示します。
        ///     <para>
        ///         (型が<C>VT_LPWSTR</C>, <c>VT_BOOL</c>および<C>VT_FILETIME</C>以外の場合)
        ///     </para>
        /// </summary>
        Sum = PROPDESC_AGGREGATION_TYPE.PDAT_SUM,

        /// <summary>
        ///     選択した値の平均を表示します。
        ///     <para>
        ///         (型が<C>VT_LPWSTR</C>, <c>VT_BOOL</c>および<C>VT_FILETIME</C>以外の場合)
        ///     </para>
        /// </summary>
        Average = PROPDESC_AGGREGATION_TYPE.PDAT_AVERAGE,

        /// <summary>
        ///     選択した値の時刻の範囲を表示します。
        ///     <para>
        ///         (型が<c>VT_FILETIME</c>の場合。
        ///     </para>
        /// </summary>
        DateRange = PROPDESC_AGGREGATION_TYPE.PDAT_DATERANGE,

        /// <summary>
        ///     すべての値を連結した文字列を表示します。文字列内の個別の値の順序は定義されていません。
        ///     <para>
        ///         重複した値は省略されます。値が複数回発生した場合、一度だけ表示されます。
        ///     </para>
        /// </summary>
        Union = PROPDESC_AGGREGATION_TYPE.PDAT_UNION,

        /// <summary>
        ///     選択した値の最大値を表示します。
        /// </summary>
        Max = PROPDESC_AGGREGATION_TYPE.PDAT_MAX,

        /// <summary>
        ///     選択した値の最小値を表示します。
        /// </summary>
        Min = PROPDESC_AGGREGATION_TYPE.PDAT_MIN
    }
}