using System;
using System.Collections.Generic;
using starshipxac.Shell;

namespace ShellFileDialogSample.Messaging.Dialogs
{
    public class CustomFileOpenDialogResponse
    {
        public CustomFileOpenDialogResponse(IEnumerable<ShellFile> selectedFiles)
        {
            this.SelectedFiles = new List<ShellFile>(selectedFiles);
        }

        public IReadOnlyList<ShellFile> SelectedFiles { get; private set; }

        public int SelectedItemIndex { get; internal set; }
    }
}