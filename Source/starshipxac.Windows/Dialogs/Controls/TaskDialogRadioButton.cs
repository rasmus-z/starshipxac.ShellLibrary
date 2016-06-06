using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Windows.Dialogs.Controls
{
    /// <summary>
    ///     タスクダイアログラジオボタンを定義します。
    /// </summary>
    public class TaskDialogRadioButton : TaskDialogButtonBase
    {
        /// <summary>
        ///     <see cref="TaskDialogRadioButton" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="name">コントロール名。</param>
        /// <param name="text">テキスト。</param>
        public TaskDialogRadioButton(string name, string text)
            : base(name, text)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));
        }

        /// <summary>
        ///     ラジオボタンの表示テキストを取得します。
        /// </summary>
        /// <returns>ラジオボタンの表示テキスト。</returns>
        public override string GetButtonText()
        {
            return this.Text;
        }
    }
}