using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
    public class FileDialogMenu : FileDialogControl
    {
        private string text;
        private readonly Collection<FileDialogMenuItem> items = new Collection<FileDialogMenuItem>();

        public FileDialogMenu(params FileDialogMenuItem[] menuItems)
            : this(String.Empty, menuItems)
        {
        }

        public FileDialogMenu(string text, params FileDialogMenuItem[] menuItems)
            : base(String.Empty)
        {
        }

        public FileDialogMenu(string name, string text, params FileDialogMenuItem[] menuItems)
            : base(name)
        {
            this.text = text;

            if (menuItems == null)
            {
                this.items = new Collection<FileDialogMenuItem>();
            }
            else
            {
                this.items = new Collection<FileDialogMenuItem>(menuItems);
            }
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
                if (this.Dialog != null)
                {
                    this.Dialog.SetControlLabel(this, this.text);
                }
            }
        }

        public IReadOnlyList<FileDialogMenuItem> Items
        {
            get
            {
                return this.items;
            }
        }

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