using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
    public class FileDialogMenuItem : FileDialogControl
    {
        private string text;

        /// <summary>
        ///     <see cref="FileDialogMenuItem" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="text">メニュー項目のテキスト。</param>
        public FileDialogMenuItem(string text)
            : this(String.Empty, text)
        {
        }

        /// <summary>
        ///     <see cref="FileDialogMenuItem" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="name">コントロール名。</param>
        /// <param name="text">メニュー項目のテキスト。</param>
        public FileDialogMenuItem(string name, string text)
            : base(name)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

            this.text = text ?? String.Empty;
        }

        public FileDialogMenu Menu { get; internal set; }

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

                this.text = value;
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
                this.Click?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}