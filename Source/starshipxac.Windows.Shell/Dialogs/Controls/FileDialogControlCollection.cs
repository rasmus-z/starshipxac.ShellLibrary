using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using starshipxac.Windows.Shell.Properties;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
    public sealed class FileDialogControlCollection : Collection<FileDialogControl>
    {
        internal FileDialogControlCollection(FileDialogBase fileDialog)
        {
            Contract.Requires<ArgumentNullException>(fileDialog != null);

            this.FileDialog = fileDialog;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.FileDialog != null);
        }

        internal FileDialogBase FileDialog { get; set; }

        public FileDialogControl this[string name]
        {
            get
            {
                Contract.Requires<ArgumentException>(String.IsNullOrWhiteSpace(name));

                foreach (var control in Items)
                {
                    FileDialogGroupBox groupBox;
                    if (control.Name == name)
                    {
                        return control;
                    }
                    if ((groupBox = control as FileDialogGroupBox) != null)
                    {
                        foreach (var dialogControl in groupBox.Items)
                        {
                            if (dialogControl.Name == name)
                            {
                                return dialogControl;
                            }
                        }
                    }
                }
                return null;
            }
        }

        protected override void InsertItem(int index, FileDialogControl control)
        {
            base.InsertItem(index, control);
            control.Attach(this.FileDialog);
        }

        protected override void RemoveItem(int index)
        {
            throw new NotSupportedException(ErrorMessages.DialogControlCollectionCannotRemoveControls);
        }

        internal FileDialogControl GetControlbyId(int id)
        {
            return GetSubControlbyId(Items, id);
        }

        internal FileDialogControl GetSubControlbyId(IEnumerable<FileDialogControl> controlCollection, int id)
        {
            if (controlCollection == null)
            {
                return null;
            }

            foreach (var control in controlCollection)
            {
                if (control.Id == id)
                {
                    return control;
                }

                var groupBox = control as FileDialogGroupBox;
                if (groupBox != null)
                {
                    var temp = GetSubControlbyId(groupBox.Items, id);
                    if (temp != null)
                    {
                        return temp;
                    }
                }
            }

            return null;
        }
    }
}