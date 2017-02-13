using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
    public class FileDialogMenuItem : FileDialogControl
    {
        private string text;

        /// <summary>
        ///     Initilize a new instance of the <see cref="FileDialogMenuItem"/> class.
        /// </summary>
        /// <param name="text">メニュー項目のテキスト。</param>
        public FileDialogMenuItem(string text)
            : this(String.Empty, text)
        {
        }

        /// <summary>
        ///     Initilize a new instance of the <see cref="FileDialogMenuItem"/> class
        ///     to the specified control name and menu item text.
        /// </summary>
        /// <param name="name">Control name.</param>
        /// <param name="text">Menu item text.</param>
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