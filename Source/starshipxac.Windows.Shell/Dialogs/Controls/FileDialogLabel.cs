using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
    public class FileDialogLabel : FileDialogControl
    {
        private string text;

        public FileDialogLabel(string text)
            : this(String.Empty, text)
        {
        }

        public FileDialogLabel(string name, string text)
            : base(name)
        {
            Contract.Requires<ArgumentNullException>(name != null);

            this.text = text ?? String.Empty;
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

                this.text = value;
                this.Dialog?.SetControlLabel(this, this.text);
            }
        }

        internal override void Attach(FileDialogBase dialog)
        {
            base.Attach(dialog);

            this.Dialog.AddLabel(this, this.text);
        }
    }
}