using System;

namespace starshipxac.Windows.Dialogs
{
    public class TaskDialogResult
    {
        private TaskDialogResult(int selectedButtonId, int selectedRadioButtonId, bool verificationChecked)
        {
            this.SelectedButtonId = selectedButtonId;
            this.SelectedRadioButtonId = selectedRadioButtonId;
            this.VerificationChecked = verificationChecked;
        }

        internal static TaskDialogResult Create(int selectedButtonId, int selectedRadioButtonId, bool verificationChecked)
        {
            return new TaskDialogResult(selectedButtonId, selectedRadioButtonId, verificationChecked);
        }

        public int SelectedButtonId { get; private set; }

        public int SelectedRadioButtonId { get; private set; }

        public bool VerificationChecked { get; private set; }
    }
}