using System;

namespace starshipxac.Windows.Dialogs
{
    /// <summary>
    /// タスクダイアログ標準ボタンIDを定義します。
    /// </summary>
    public enum TaskDialogCommonButtonId
    {
        Ok = 1,
        Cancel = 2,
        Abort = 3,
        Retry = 4,
        Ignore = 5,
        Yes = 6,
        No = 7,
        Close = 8,

        MinCustomControlId = Close + 1
    }
}