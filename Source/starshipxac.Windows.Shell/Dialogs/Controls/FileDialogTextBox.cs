using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
    public class FileDialogTextBox : FileDialogControl
    {
        private string text;

        public FileDialogTextBox(string name)
            : this(name, String.Empty)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));
        }

        public FileDialogTextBox(string name, string text)
            : base(name)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

            this.text = text;
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
                if (this.Dialog != null)
                {
                    this.Dialog.SetEditBoxText(this, this.text);
                }
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