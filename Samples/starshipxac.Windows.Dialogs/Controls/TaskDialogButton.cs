using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Windows.Dialogs.Controls
{
    /// <summary>
    ///     タスクダイアログボタンを定義します。
    /// </summary>
    public class TaskDialogButton : TaskDialogButtonBase
    {
        public TaskDialogButton(string name, string text, bool dialogClosable = false)
            : base(name, text, dialogClosable)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));
        }

        public override string GetButtonText()
        {
            return this.Text;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}{{Id={this.Id}, Name={this.Name}, Text={this.Text}}}";
        }
    }
}