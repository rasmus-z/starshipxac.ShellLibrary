using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
    public class FileDialogTextBox : FileDialogControl
    {
        private string text;

        /// <summary>
        ///     Initialize a new instance of the <see cref="FileDialogTextBox"/> class
        ///     to the specified text.
        /// </summary>
        /// <param name="text">Text.</param>
        public FileDialogTextBox(string text)
            : this(String.Empty, text)
        {
        }

        public FileDialogTextBox(string name, string text)
            : base(name)
        {
            Contract.Requires<ArgumentNullException>(name != null);

            this.text = text ?? String.Empty;
        }

        public override string Text
        {
            get
            {
                if (this.Dialog != null)
                {
                    this.text = this.Dialog.GetEditBoxText(this);
                }
                return this.text;
            }
            set
            {
                if (this.text == value)
                {
                    return;
                }

                this.text = value;
                this.Dialog?.SetEditBoxText(this, this.text);
            }
        }

        internal override void Attach(FileDialogBase dialog)
        {
            base.Attach(dialog);

            this.Dialog.AddEditBox(this, this.text);
        }

        internal override void Detach()
        {
            this.text = this.Dialog.GetEditBoxText(this);

            base.Detach();
        }
    }
}