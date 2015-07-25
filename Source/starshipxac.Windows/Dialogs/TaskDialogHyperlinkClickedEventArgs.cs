using System;

namespace starshipxac.Windows.Dialogs
{
    /// <summary>
    /// <see cref="TaskDialog.HyperlinkClicked"/>イベントデータを定義します。
    /// </summary>
    public class TaskDialogHyperlinkClickedEventArgs : EventArgs
    {
        /// <summary>
        /// ハイパーリンクのテキストを指定して、
        /// <see cref="TaskDialogHyperlinkClickedEventArgs"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="linkText"></param>
        public TaskDialogHyperlinkClickedEventArgs(string linkText)
        {
            this.LinkText = linkText;
        }

        /// <summary>
        /// クリックしたハイパーリンクのテキストを取得します。
        /// </summary>
        public string LinkText { get; }
    }
}