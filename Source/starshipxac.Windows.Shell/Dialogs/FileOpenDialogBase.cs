using System;
using System.Collections.Generic;
using System.Linq;
using starshipxac.Shell;
using starshipxac.Shell.Internal;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.Dialogs.Interop;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    ///     Define the base class of the file open dialog.
    /// </summary>
    public abstract class FileOpenDialogBase : FileDialogBase
    {
        private bool restoreDirectory = true;
        private bool addToMruList = true;
        private bool setFilter = false;

        /// <summary>
        ///     Initialize a new instance of the <see cref="FileOpenDialogBase" /> class.
        /// </summary>
        protected FileOpenDialogBase()
        {
            this.FileTypeFilters = new FileTypeFilterCollection();
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="FileOpenDialogBase" /> class
        ///     to the specified dialog title.
        /// </summary>
        /// <param name="title">Dialog title.</param>
        protected FileOpenDialogBase(string title)
            : base(title)
        {
            this.FileTypeFilters = new FileTypeFilterCollection();
        }

        /// <summary>
        ///     Get or set a value indicating whether or not to restore the directory
        ///     to its original position after completion.
        /// </summary>
        /// <exception cref="InvalidOperationException">It can not be changed while dialog is displayed.</exception>
        public bool RestoreDirectory
        {
            get
            {
                return this.restoreDirectory;
            }
            set
            {
                ThrowIfDialogShowingPropertyCannotBeChanged();
                this.restoreDirectory = value;
            }
        }

        /// <summary>
        ///     Get or set a value that determines whether or not to add the specified file to the recently used file list.
        /// </summary>
        /// <exception cref="InvalidOperationException">It can not be changed while dialog is displayed.</exception>
        public bool AddToMostRecentlyUsedList
        {
            get
            {
                return this.addToMruList;
            }
            set
            {
                ThrowIfDialogShowingPropertyCannotBeChanged();
                this.addToMruList = value;
            }
        }

        /// <summary>
        ///     Get the collection of filters displayed in the file type of the file dialog box.
        /// </summary>
        public FileTypeFilterCollection FileTypeFilters { get; }

        /// <summary>
        ///     Get the currently selected filter in the file dialog box.
        /// </summary>
        public FileTypeFilter SelectedFileTypeFilter
        {
            get
            {
                var fileDialogNative = (IFileOpenDialog)this.FileDialogInternal.FileDialogNative;
                if (fileDialogNative == null)
                {
                    return null;
                }

                uint fileType;
                fileDialogNative.GetFileTypeIndex(out fileType);
                return this.FileTypeFilters[(int)fileType];
            }
        }

        /// <summary>
        ///     Get the index of the currently selected filter in the file dialog box.
        /// </summary>
        public int SelectedFileTypeFilterIndex
        {
            get
            {
                var fileDialogNative = (IFileOpenDialog)this.FileDialogInternal.FileDialogNative;
                if (fileDialogNative == null)
                {
                    return -1;
                }

                uint fileType;
                fileDialogNative.GetFileTypeIndex(out fileType);
                return (int)fileType;
            }
        }

        /// <summary>
        ///     Gets a collection of files selected by the user.
        /// </summary>
        /// <returns>A collection of files.</returns>
        public IEnumerable<ShellFile> GetShellFiles()
        {
            var fileDialogNative = (IFileOpenDialog)this.FileDialogInternal.FileDialogNative;

            IShellItemArray shellItemArray;
            fileDialogNative.GetResults(out shellItemArray);

            var count = ShellItemArray.GetShellItemCount(shellItemArray);
            for (var index = 0; index < count; ++index)
            {
                var shellItem = ShellItemArray.GetShellItemAt(shellItemArray, index);
                var shellFile = ShellFactory.FromShellItem(new ShellItem((IShellItem2)shellItem)) as ShellFile;
                if (shellFile != null)
                {
                    yield return shellFile;
                }
            }
        }

        /// <summary>
        ///     Create the <see cref="FileOpenDialogNative" />.
        /// </summary>
        internal override IFileDialog2 CreateNativeFileDialog()
        {
            return new FileOpenDialogNative();
        }

        protected override FileDialogOptions GetDialogOptions()
        {
            var result = base.GetDialogOptions();

            if (this.RestoreDirectory)
            {
                result |= FileDialogOptions.RestoreDirectory;
            }

            result |= FileDialogOptions.PathMustExist;
            result |= FileDialogOptions.FileMustExist;

            return result;
        }

        /// <summary>
        ///     Apply the setting to the native dialog.
        /// </summary>
        protected override void SetNativeSettings()
        {
            base.SetNativeSettings();

            // Filter
            if (this.FileTypeFilters.Any() && !this.setFilter)
            {
                this.FileDialogInternal.SetFilters(this.FileTypeFilters);
                this.setFilter = true;
            }
        }
    }
}