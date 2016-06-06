using System;
using starshipxac.Windows.Dialogs.Controls;

namespace starshipxac.Windows.Dialogs
{
    public class TaskDialogResult
    {
        private TaskDialogResult(TaskDialogButtonBase selectedButton, TaskDialogRadioButton selectedRadioButton,
            bool verificationChecked)
        {
            this.SelectedButton = selectedButton;
            this.SelectedRadioButton = selectedRadioButton;
            this.VerificationChecked = verificationChecked;
        }

        internal static TaskDialogResult Create(TaskDialogButtonBase selectedButton, TaskDialogRadioButton selectedRadioButton,
            bool verificationChecked)
        {
            return new TaskDialogResult(selectedButton, selectedRadioButton, verificationChecked);
        }

        public TaskDialogButtonBase SelectedButton { get; }

        public TaskDialogRadioButton SelectedRadioButton { get; }

        public bool VerificationChecked { get; }
    }
}