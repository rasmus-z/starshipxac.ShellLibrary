using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Windows.Dialogs.Controls
{
    /// <summary>
    /// タスクダイアログコントロール基底クラスを定義します。
    /// </summary>
    public abstract class TaskDialogControl : DialogControl
    {
        /// <summary>
        /// コントロール名を指定して、<see cref="TaskDialogControl"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="name">コントロール名。</param>
        protected TaskDialogControl(string name)
            : base(name)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(name));
        }

        /// <summary>
        /// コントロールIDおよびコントロール名を指定して、<see cref="TaskDialogControl"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="id">コントロールID。</param>
        /// <param name="name">コントロール名。</param>
        protected TaskDialogControl(int id, string name)
            : base(id, name)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id > 0);
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(name));
        }

        public TaskDialogBase Dialog { get; private set; }

        internal virtual void Attach(TaskDialogBase dialog)
        {
            Contract.Requires<ArgumentNullException>(dialog != null);

            this.Dialog = dialog;
        }

        internal virtual void Detach()
        {
            Contract.Requires<InvalidOperationException>(this.Dialog != null);

            this.Dialog = null;
        }

        protected void ThrowIfNotInitialized()
        {
            if (this.Dialog == null)
            {
                throw new InvalidOperationException();
            }
        }
    }
}