using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace starshipxac.Windows.Dialogs.Controls
{
    /// <summary>
    /// タスクダイアログボタンを定義します。
    /// </summary>
    public class TaskDialogButton : TaskDialogButtonBase
    {
        public TaskDialogButton(TaskDialogCommonButtonId id, string name, string text)
            : base((int)id, name, text)
        {
        }

        public TaskDialogButton(int id, string name, string text, bool dialogClosable = false)
            : base(id, name, text, dialogClosable)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id > 0);
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));
        }

        public override string GetButtonText()
        {
            return this.Text;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append("TaskDialogButton[");
            result.AppendFormat("Id={0}", this.Id);
            result.AppendFormat(", Name={0}", this.Name);
            result.AppendFormat(", Text={0}", this.Text);
            result.Append("]");
            return result.ToString();
        }
    }
}