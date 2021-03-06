﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Threading;
using starshipxac.Windows.Shell.Properties;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
    public class FileDialogRadioButtonList : FileDialogControl, IFileDialogIndexedControls
    {
        private readonly Collection<FileDialogRadioButtonListItem> items;
        private int selectedIndex = -1;

        public FileDialogRadioButtonList(params FileDialogRadioButtonListItem[] items)
            : this(String.Empty, items)
        {
        }

        public FileDialogRadioButtonList(string name, params FileDialogRadioButtonListItem[] items)
            : base(name)
        {
            Contract.Requires<ArgumentNullException>(name != null);

            this.items = (items == null)
                ? new Collection<FileDialogRadioButtonListItem>()
                : new Collection<FileDialogRadioButtonListItem>(items);
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

        public IReadOnlyList<FileDialogRadioButtonListItem> Items => this.items;

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
                    throw new ArgumentOutOfRangeException(ErrorMessages.RadioButtonListIndexOutOfBounds);
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

        public virtual void OnSelectedIndexChanged(EventArgs args)
        {
            var handler = Interlocked.CompareExchange(ref this.SelectedIndexChanged, null, null);
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion

        public void RaiseSelectedIndexChangedEvent()
        {
            OnSelectedIndexChanged(EventArgs.Empty);
        }

        internal override void Attach(FileDialogBase dialog)
        {
            base.Attach(dialog);

            this.Dialog.AddRadioButtonList(this);

            for (var index = 0; index < this.items.Count; ++index)
            {
                var item = this.items[index];
                this.Dialog.AddControlItem(this, index, item.Text);
            }

            if (0 <= this.selectedIndex && this.selectedIndex < this.items.Count)
            {
                this.Dialog.SetControlSelectedIndex(this, this.SelectedIndex);
            }
        }

        internal override void Detach()
        {
            this.selectedIndex = this.Dialog.GetControlSelectedIndex(this);

            base.Detach();
        }
    }
}