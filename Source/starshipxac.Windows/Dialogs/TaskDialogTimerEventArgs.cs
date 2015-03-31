using System;

namespace starshipxac.Windows.Dialogs
{
    /// <summary>
    /// <see cref="TaskDialog.Timer"/>イベントデータを定義します。
    /// </summary>
    public class TaskDialogTimerEventArgs : EventArgs
    {
        /// <summary>
        /// <see cref="TaskDialogTimerEventArgs"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="ticks">コントロールが有効になってからの時間数。</param>
        public TaskDialogTimerEventArgs(int ticks)
        {
            this.Ticks = ticks;
        }

        /// <summary>
        /// 時間数を取得します。
        /// </summary>
        public int Ticks { get; private set; }
    }
}