using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using starshipxac.Windows.Shell.Properties;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
    public class FileDialogComboBox : FileDialogControl, IFileDialogIndexedControls
    {
        private readonly Collection<FileDialogComboBoxItem> items;
        private int selectedIndex = -1;

        public FileDialogComboBox(params FileDialogComboBoxItem[] items)
            : this(String.Empty, items)
        {
        }

        public FileDialogComboBox(string name, params FileDialogComboBoxItem[] items)
            : base(name)
        {
            Contract.Requires<ArgumentNullException>(name != null);

            this.items = items == null
                ? new Collection<FileDialogComboBoxItem>()
                : new Collection<FileDialogComboBoxItem>(items);
        }

        public override string Text
        {
            get
            {
                return String.Empty;
            }
            set
            {
                throw new InvalidOperationException();
            }
        }

        public IReadOnlyList<FileDialogComboBoxItem> Items => this.items;

        public int SelectedIndex
        {
            get
            {
                if (this.Dialog != null)
                {
                    this.selectedIndex = this.Dialog.GetControlSelectedIndex(this);
                }
                return this.selectedIndex;
            }
            set
            {
                if (value < 0 || this.items.Count <= value)
                {
                    throw new ArgumentOutOfRangeException(ErrorMessages.ComboBoxIndexOutsideBounds);
                }
                if (this.selectedIndex == value)
                {
                    return;
                }

                this.selectedIndex = value;
                this.Dialog?.SetControlSelectedIndex(this, this.selectedIndex);
            }
        }

        #region SelectedIndexChanged Event

        public event EventHandler SelectedIndexChanged;

        void IFileDialogIndexedControls.RaiseSelectedIndexChangedEvent()
        {
            if (this.Enabled)
            {
                this.SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion

        internal override void Attach(FileDialogBase dialog)
        {
            base.Attach(dialog);

            this.Dialog.AddComboBox(this);

            for (var index = 0; index < this.items.Count; ++index)
            {
                var item = this.items[index];
                this.Dialog.AddControlItem(this, index, item.Text);
            }

            this.selectedIndex = 0;
            if (0 <= this.selectedIndex && this.selectedIndex < this.items.Count)
            {
                this.Dialog.SetControlSelectedIndex(this, this.selectedIndex);
            }
        }

        internal override void Detach()
        {
            this.selectedIndex = this.Dialog.GetControlSelectedIndex(this);

            base.Detach();
        }
    }
}