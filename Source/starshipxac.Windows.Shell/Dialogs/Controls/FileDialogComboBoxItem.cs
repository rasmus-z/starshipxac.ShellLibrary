using System;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
    public class FileDialogComboBoxItem
    {
        private string text;

        public FileDialogComboBoxItem(string text)
        {
            this.text = String.IsNullOrWhiteSpace(text) ? String.Empty : text;
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }
    }
}