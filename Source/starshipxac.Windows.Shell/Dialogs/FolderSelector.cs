using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    ///     Define dialog for selecting a folder.
    /// </summary>
    public sealed class FolderSelector : FolderSelectDialogBase
    {
        private bool multiSelect = false;

        /// <summary>
        ///     Initialize the  <see cref="FolderSelector" /> class.
        /// </summary>
        static FolderSelector()
        {
            EmptyShellFolders = new List<ShellFolder>();
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="FolderSelector" /> class.
        /// </summary>
        public FolderSelector()
        {
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="FolderSelector" /> class
        ///     to the specified dialog title.
        /// </summary>
        /// <param name="title">Dialog title.</param>
        public FolderSelector(string title)
            : base(title)
        {
        }

        /// <summary>
        ///     Get the collection of the empty folder.
        /// </summary>
        private static IEnumerable<ShellFolder> EmptyShellFolders { get; }

        /// <summary>
        ///     Displays a dialog for selecting single folder.
        /// </summary>
        /// <returns>The selected folder. If the user canceled it will return <c>null</c>.</returns>
        public async Task<ShellFolder> SelectSingleFolderAsync()
        {
            ShellFolder result = null;

            this.multiSelect = false;

            await InvokeAsync(() =>
            {
                var dialogResult = ShowDialog();
                if (dialogResult == FileDialogResult.Ok)
                {
                    result = GetShellFolders().FirstOrDefault();
                }
            });

            return result;
        }

        /// <summary>
        ///     Displays a dialog for selecting multiple folders.
        /// </summary>
        /// <returns>A collection of selected folders.</returns>
        public async Task<IEnumerable<ShellFolder>> SelectMultipleFoldersAsync()
        {
            Contract.Ensures(Contract.Result<IEnumerable<ShellFolder>>() != null);

            var result = EmptyShellFolders;

            this.multiSelect = true;

            await InvokeAsync(() =>
            {
                var dialogResult = ShowDialog();
                if (dialogResult == FileDialogResult.Ok)
                {
                    result = GetShellFolders();
                }
            });

            return result;
        }

        /// <summary>
        ///     Get the <see cref="FileDialogOptions" />.
        /// </summary>
        /// <returns><see cref="FileDialogOptions" />.</returns>
        protected override FileDialogOptions GetDialogOptions()
        {
            var result = base.GetDialogOptions();

            if (this.multiSelect)
            {
                result |= FileDialogOptions.MultiSelect;
            }

            return result;
        }

        private async Task InvokeAsync(Action action)
        {
            if (Application.Current.Dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                await Application.Current.Dispatcher.InvokeAsync(action);
            }
        }
    }
}