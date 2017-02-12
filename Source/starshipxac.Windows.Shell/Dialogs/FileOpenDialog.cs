using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Windows;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    ///     Define file open dialog.
    /// </summary>
    public sealed class FileOpenDialog : FileOpenDialogBase
    {
        private IEnumerable<ShellFile> shellFiles;

        /// <summary>
        ///     Initialize a new instance of the <see cref="FileOpenDialog" /> class.
        /// </summary>
        public FileOpenDialog()
        {
            this.MultiSelect = false;
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="FileOpenDialog" /> class
        ///     to the specified dialog title.
        /// </summary>
        /// <param name="title">Dialog title.</param>
        public FileOpenDialog(string title)
            : base(title)
        {
        }

        /// <summary>
        ///     Get or set a value indicating whether multiple files can be selected.
        /// </summary>
        public bool MultiSelect { get; set; }

        /// <summary>
        ///     Get or set a value indicating whether only read-only files can be selected.
        /// </summary>
        public bool EnsureReadOnly { get; set; }

        /// <summary>
        ///     Get or set a value indicating whether items other than the file system can be selected.
        /// </summary>
        public bool AllowNonFileSystemItem { get; set; }

        /// <summary>
        ///     Enumerate selected files.
        /// </summary>
        public IEnumerable<ShellFile> ShellFiles
        {
            get
            {
                if (this.shellFiles == null)
                {
                    this.shellFiles = GetShellFiles();
                }
                return this.shellFiles;
            }
        }

        /// <summary>
        ///     Show file open dialog.
        /// </summary>
        /// <returns>Dialog result.</returns>
        public FileDialogResult Show()
        {
            return ShowDialog();
        }

        /// <summary>
        ///     Show file open dialog.
        /// </summary>
        /// <param name="parentWindow">Parent window</param>
        /// <returns>Dialog result.</returns>
        public FileDialogResult Show(Window parentWindow)
        {
            Contract.Requires<ArgumentNullException>(parentWindow != null);

            return ShowDialog(parentWindow);
        }

        /// <summary>
        ///     Get the <see cref="FileDialogOptions" />.
        /// </summary>
        /// <returns></returns>
        protected override FileDialogOptions GetDialogOptions()
        {
            var result = base.GetDialogOptions();

            if (this.MultiSelect)
            {
                result |= FileDialogOptions.MultiSelect;
            }
            if (this.EnsureReadOnly)
            {
                result |= FileDialogOptions.EnsureReadOnly;
            }
            if (!this.AllowNonFileSystemItem)
            {
                result |= FileDialogOptions.ForceFileSystem;
            }
            else
            {
                result |= FileDialogOptions.AllNonStotageItems;
            }

            return result;
        }
    }
}