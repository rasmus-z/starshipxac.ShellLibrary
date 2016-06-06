using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
    public class FileDialogGroupBox : FileDialogControl
    {
        private string text;
        private readonly Collection<FileDialogControl> items;

        public FileDialogGroupBox(params FileDialogControl[] controls)
            : this(String.Empty, null, controls)
        {
        }

        public FileDialogGroupBox(string text, params FileDialogControl[] controls)
            : this(String.Empty, text, controls)
        {
        }

        public FileDialogGroupBox(string name, string text, params FileDialogControl[] controls)
            : base(name)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

            this.text = text ?? String.Empty;

            this.items = controls == null
                ? new Collection<FileDialogControl>()
                : new Collection<FileDialogControl>(this.items);
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

        public IReadOnlyList<FileDialogControl> Items => this.items;

        internal override void Attach(FileDialogBase dialog)
        {
            base.Attach(dialog);

            this.Dialog.StartVisualGroup(this, this.text);

            foreach (var item in this.Items)
            {
                item.Attach(this.Dialog);
            }
            this.Dialog.EndVisualGroup();
        }

        internal override void Detach()
        {
            foreach (var control in this.Items)
            {
                control.Detach();
            }

            base.Detach();
        }
    }
}