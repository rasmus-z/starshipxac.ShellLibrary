using System;
using starshipxac.Windows.Dialogs.Interop;

namespace starshipxac.Windows.Dialogs
{
    /// <summary>
    /// タスクダイアログ標準ボタンを定義します。
    /// </summary>
    [Flags]
    public enum TaskDialogCommonButtons
    {
        /// <summary>
        /// 未定義のボタン。
        /// </summary>
        None = 0,

        /// <summary>
        /// OKボタン。
        /// </summary>
        Ok = TASKDIALOG_COMMON_BUTTON_FLAGS.TDCBF_OK_BUTTON,

        /// <summary>
        /// 「はい」ボタン。
        /// </summary>
        Yes = TASKDIALOG_COMMON_BUTTON_FLAGS.TDCBF_YES_BUTTON,

        /// <summary>
        /// 「いいえ」ボタン。
        /// </summary>
        No = TASKDIALOG_COMMON_BUTTON_FLAGS.TDCBF_NO_BUTTON,

        /// <summary>
        /// キャンセルボタン。
        /// </summary>
        Cancel = TASKDIALOG_COMMON_BUTTON_FLAGS.TDCBF_CANCEL_BUTTON,

        /// <summary>
        /// 「再試行」ボタン。
        /// </summary>
        Retry = TASKDIALOG_COMMON_BUTTON_FLAGS.TDCBF_RETRY_BUTTON,

        /// <summary>
        /// 閉じるボタン。
        /// </summary>
        Close = TASKDIALOG_COMMON_BUTTON_FLAGS.TDCBF_CLOSE_BUTTON,

        MinCustomControlId = Close + 1
    }
}