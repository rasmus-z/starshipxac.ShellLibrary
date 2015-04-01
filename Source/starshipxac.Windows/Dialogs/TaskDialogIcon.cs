using System;

namespace starshipxac.Windows.Dialogs
{
    /// <summary>
    /// タスクダイアログ標準アイコンを定義します。
    /// </summary>
    public enum TaskDialogIcon
    {
        /// <summary>
        /// アイコンなし。
        /// </summary>
        None = 0,

        /// <summary>
        /// 警告アイコン。
        /// </summary>
        Warning = 65535,

        /// <summary>
        /// エラーアイコン。
        /// </summary>
        Error = 65534,

        /// <summary>
        /// 情報アイコン。
        /// </summary>
        Information = 65533,

        /// <summary>
        /// アカウントコントロールアイコン。
        /// </summary>
        Shield = 65532
    }
}