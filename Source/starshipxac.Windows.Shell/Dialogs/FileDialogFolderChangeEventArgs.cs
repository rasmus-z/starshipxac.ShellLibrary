using System;
using System.ComponentModel;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    ///     Define folder change event arguments class.
    /// </summary>
    public class FileDialogFolderChangeEventArgs : CancelEventArgs
    {
        /// <summary>
        ///     Initialize a new instance of the <see cref="FileDialogFolderChangeEventArgs" /> class
        ///     to the specified folder.
        /// </summary>
        /// <param name="folder"><see cref="ShellFolder" />.</param>
        public FileDialogFolderChangeEventArgs(ShellFolder folder)
        {
            this.ShellFolder = folder;
        }

        /// <summary>
        ///     Get the folder.
        /// </summary>
        public ShellFolder ShellFolder { get; }
    }
}