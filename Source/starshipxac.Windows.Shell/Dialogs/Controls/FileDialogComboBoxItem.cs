using System;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
    public class FileDialogComboBoxItem
    {
        public FileDialogComboBoxItem(string text)
        {
            this.Text = String.IsNullOrWhiteSpace(text) ? String.Empty : text;
        }

        public string Text { get; set; }
    }
}