using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Windows.Dialogs.Controls
{
    /// <summary>
    /// タスクダイアログラジオボタンを定義します。
    /// </summary>
    public class TaskDialogRadioButton : TaskDialogButtonBase
    {
        /// <summary>
        /// <see cref="TaskDialogRadioButton"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="id">コントロールID。</param>
        /// <param name="name">コントロール名。</param>
        /// <param name="text">テキスト。</param>
        public TaskDialogRadioButton(int id, string name, string text)
            : base(id, name, text)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id > 0);
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));
        }

        /// <summary>
        /// ラジオボタンの表示テキストを取得します。
        /// </summary>
        /// <returns>ラジオボタンの表示テキスト。</returns>
        public override string GetButtonText()
        {
            return this.Text;
        }
    }
}