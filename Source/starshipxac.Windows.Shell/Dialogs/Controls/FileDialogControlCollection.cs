using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using starshipxac.Windows.Shell.Properties;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
    /// <summary>
    /// ファイルダイアログコントロールのコレクションを保持します。
    /// </summary>
    public sealed class FileDialogControlCollection : Collection<FileDialogControl>
    {
        internal FileDialogControlCollection(FileDialogBase dialog)
        {
            Contract.Requires<ArgumentNullException>(dialog != null);

            this.Dialog = dialog;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.Dialog != null);
        }

        internal FileDialogBase Dialog { get; set; }

        public FileDialogControl this[string name]
        {
            get
            {
                Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

                foreach (var control in this.Items)
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
            control.Attach(this.Dialog);
        }

        protected override void RemoveItem(int index)
        {
            throw new NotSupportedException(ErrorMessages.DialogControlCollectionCannotRemoveControls);
        }
    }
}