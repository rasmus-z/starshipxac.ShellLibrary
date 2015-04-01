using System;

namespace starshipxac.Windows.Dialogs
{
    /// <summary>
    /// タスクダイアログの実行結果を定義します。
    /// </summary>        
    public enum TaskDialogSelectedButton
    {
        /// <summary>
        /// 定義外のボタンクリック。
        /// </summary>
        None = 0x0000,

        /// <summary>
        /// OKボタンクリック。
        /// </summary>
        Ok = 0x0001,

        /// <summary>
        /// 「はい」ボタンクリック。
        /// </summary>
        Yes = 0x0002,

        /// <summary>
        /// 「いいえ」ボタンクリック。
        /// </summary>
        No = 0x0004,

        /// <summary>
        /// キャンセルボタンクリック。
        /// </summary>
        Cancel = 0x0008,

        /// <summary>
        /// 「再試行」ボタンクリック。
        /// </summary>
        Retry = 0x0010,

        /// <summary>
        /// 閉じるボタンクリック。
        /// </summary>
        Close = 0x0020,

        /// <summary>
        /// カスタムボタンクリック。
        /// </summary>
        CustomButtonClicked = 0x0100,
    }
}