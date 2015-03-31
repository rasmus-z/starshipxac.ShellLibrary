using System;

namespace starshipxac.Windows.Dialogs
{
    /// <summary>
    /// 拡張表示方法を定義します。
    /// </summary>
    public enum TaskDialogExpansionMode
    {
        /// <summary>
        /// 表示しない。
        /// </summary>
        Hide,

        /// <summary>
        /// コンテンツを表示する。
        /// </summary>
        ExpandContent,

        /// <summary>
        /// フッターを拡張する。
        /// </summary>
        ExpandFooter
    }
}