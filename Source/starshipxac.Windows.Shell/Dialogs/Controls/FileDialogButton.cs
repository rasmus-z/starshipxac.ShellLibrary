using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
    /// <summary>
    ///     ファイルダイアログボタンを定義します。
    /// </summary>
    public class FileDialogButton : FileDialogControl
    {
        private string text;

        public FileDialogButton(string name)
            : this(name, String.Empty)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));
        }

        public FileDialogButton(string name, string text)
            : base(name)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

            this.text = text;
        }

        public override string Text
        {
            get { return this.text; }
            set
            {
                if (this.text == value)
                {
                    return;
                }

                this.text = value ?? String.Empty;
                this.Dialog?.SetControlLabel(this, this.text);
            }
        }

        #region Click Event

        public event EventHandler Click;

        protected virtual void OnClick(EventArgs args)
        {
            this.Click?.Invoke(this, args);
        }

        #endregion

        internal void RaiseClickEvent()
        {
            if (this.Enabled)
            {
                OnClick(EventArgs.Empty);
            }
        }

        internal override void Attach(FileDialogBase dialog)
        {
            base.Attach(dialog);

            this.Dialog.AddButton(this);
            this.Dialog.SetControlLabel(this, this.text);
        }
    }
}