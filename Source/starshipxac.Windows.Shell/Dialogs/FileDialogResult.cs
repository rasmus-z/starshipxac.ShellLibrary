using System;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    /// ダイアログの実行結果を定義します。
    /// </summary>
    public enum FileDialogResult
    {
        /// <summary>
        /// 未実行状態。
        /// </summary>
        None = 0,

        /// <summary>
        /// OKまたは保存。
        /// </summary>
        Ok = 1,

        /// <summary>
        /// キャンセル。
        /// </summary>
        Cancel = 2,
    }
}