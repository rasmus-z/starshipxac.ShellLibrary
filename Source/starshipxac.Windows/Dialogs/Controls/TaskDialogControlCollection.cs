using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;

namespace starshipxac.Windows.Dialogs.Controls
{
    /// <summary>
    ///     タスクダイアログコントロールのコレクションを保持します。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class TaskDialogControlCollection<T> : Collection<T> where T : TaskDialogControl
    {
        internal TaskDialogControlCollection(TaskDialogBase dialog)
        {
            Contract.Requires<ArgumentNullException>(dialog != null);

            this.Dialog = dialog;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.Dialog != null);
        }

        internal TaskDialogBase Dialog { get; set; }

        public TaskDialogControl this[string name]
        {
            get
            {
                Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

                return this.Items.FirstOrDefault(x => x.Name == name);
            }
        }

        protected override void InsertItem(int index, T control)
        {
            base.InsertItem(index, control);
            control.Attach(this.Dialog);
        }

        protected override void RemoveItem(int index)
        {
            var control = this.Items[index];
            control.Detach();
            base.RemoveItem(index);
        }
    }
}