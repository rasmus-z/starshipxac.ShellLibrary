using System;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
    public class FileDialogComboBoxItem
    {
        private string text = string.Empty;

        public FileDialogComboBoxItem(string text)
        {
            if (String.IsNullOrWhiteSpace(text))
            {
                this.text = String.Empty;
            }
            else
            {
                this.text = text;
            }
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