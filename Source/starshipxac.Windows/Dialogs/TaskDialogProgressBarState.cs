using System;
using starshipxac.Windows.Dialogs.Interop;

namespace starshipxac.Windows.Dialogs
{
    /// <summary>
    /// プログレスバーの状態を定義します。
    /// </summary>        
    public enum TaskDialogProgressBarState : uint
    {
        /// <summary>
        /// 通常の状態。
        /// </summary>
        Normal = ProgressBarStates.PBST_NORMAL,

        /// <summary>
        /// エラー状態。
        /// </summary>
        Error = ProgressBarStates.PBST_ERROR,

        /// <summary>
        /// 一時停止状態。
        /// </summary>
        Paused = ProgressBarStates.PBST_PAUSED,
    }
}