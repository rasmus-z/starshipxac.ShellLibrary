using System;

namespace starshipxac.Windows.Dialogs
{
    public class TaskDialogVerificationChangedEventArgs : EventArgs
    {
        public TaskDialogVerificationChangedEventArgs(bool verificationChecked)
        {
            this.VerificationChecked = verificationChecked;
        }

        public bool VerificationChecked { get; }
    }
}