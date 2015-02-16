using System;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    /// ダイアログ表示状態を定義します。
    /// </summary>
    public enum DialogShowStates
    {
        /// <summary>
        /// 表示前。
        /// </summary>
        PreShow,

        /// <summary>
        /// 表示中。
        /// </summary>
        Showing,

        /// <summary>
        /// 終了中。
        /// </summary>
        Closing,

        /// <summary>
        /// 終了。
        /// </summary>
        Closed
    }
}