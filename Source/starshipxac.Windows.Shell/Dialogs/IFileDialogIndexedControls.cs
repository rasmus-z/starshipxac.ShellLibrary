using System;

namespace starshipxac.Windows.Shell.Dialogs
{
    interface IFileDialogIndexedControls
    {
        int SelectedIndex { get; set; }

        event EventHandler SelectedIndexChanged;

        void RaiseSelectedIndexChangedEvent();
    }
}