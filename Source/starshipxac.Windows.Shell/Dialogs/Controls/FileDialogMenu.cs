using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
    public class FileDialogMenu : FileDialogControl
    {
        private string text;
        private readonly Collection<FileDialogMenuItem> items;

        public FileDialogMenu(string text, params FileDialogMenuItem[] menuItems)
            : this(String.Empty, text, menuItems)
        {
        }

        public FileDialogMenu(string name, string text, params FileDialogMenuItem[] menuItems)
            : base(name)
        {
            Contract.Requires<ArgumentNullException>(name != null);

            this.text = text ?? String.Empty;

            this.items = (menuItems == null)
                ? new Collection<FileDialogMenuItem>()
                : new Collection<FileDialogMenuItem>(menuItems);
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

        public IReadOnlyList<FileDialogMenuItem> Items => this.items;

        internal override void Attach(FileDialogBase dialog)
        {
            base.Attach(dialog);

            this.Dialog.AddMenu(this, this.text);

            foreach (var menuItem in this.items)
            {
                menuItem.Menu = this;
                this.Dialog.AddMenuItem(this, menuItem, menuItem.Text);
                menuItem.Attach(dialog);
            }
        }
    }
}