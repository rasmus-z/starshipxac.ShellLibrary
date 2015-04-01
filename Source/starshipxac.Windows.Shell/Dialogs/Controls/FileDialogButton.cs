using System;
using System.Threading;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
    /// <summary>
    /// ファイルダイアログボタンを定義します。
    /// </summary>
    public class FileDialogButton : FileDialogControl
    {
        private string text;

        public FileDialogButton(string name)
            : this(name, String.Empty)
        {
        }

        public FileDialogButton(string name, string text)
            : base(name)
        {
            this.text = text;
        }

        public override string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                if (this.text == value)
                {
                    return;
                }

                this.text = value ?? String.Empty;
                if (this.Dialog != null)
                {
                    this.Dialog.SetControlLabel(this, this.text);
                }
            }
        }

        #region Click Event

        public event EventHandler Click;

        protected virtual void OnClick(EventArgs args)
        {
            var handler = Interlocked.CompareExchange(ref this.Click, null, null);
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion

        internal void RaiseClickEvent()
        {
            if (this.Enabled)
            {
                OnClick(EventArgs.Empty);
            }
        }

        override internal void Attach(FileDialogBase dialog)
        {
            base.Attach(dialog);

            this.Dialog.AddButton(this);
            this.Dialog.SetControlLabel(this, this.text);
        }
    }
}