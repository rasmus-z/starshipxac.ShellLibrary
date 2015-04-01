using System;

namespace starshipxac.Windows.Dialogs
{
    public class TaskDialogExpandChangedEventArgs : EventArgs
    {
        public TaskDialogExpandChangedEventArgs(bool expanded)
        {
            this.Expanded = expanded;
        }

        public bool Expanded { get; private set; }
    }
}