using System;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
    public class FileDialogRadioButtonListItem
    {
        public FileDialogRadioButtonListItem()
            : this(string.Empty)
        {
        }

        public FileDialogRadioButtonListItem(string text)
        {
            this.Text = text;
        }

        public string Text { get; set; }
    }
}