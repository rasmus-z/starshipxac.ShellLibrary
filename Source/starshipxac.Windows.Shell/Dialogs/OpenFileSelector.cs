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
    ///     Define dialog for selecting a file.
    /// </summary>
    public sealed class OpenFileSelector : FileOpenDialogBase
    {
        private bool multiSelect = false;

        /// <summary>
        ///     Initialize the <see cref="OpenFileSelector" /> class.
        /// </summary>
        static OpenFileSelector()
        {
            EmptyShellFiles = new List<ShellFile>();
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="OpenFileSelector" /> class.
        /// </summary>
        public OpenFileSelector()
        {
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="OpenFileSelector" /> class
        ///     to the specified dialog title.
        /// </summary>
        /// <param name="title">Dialog title.</param>
        public OpenFileSelector(string title)
            : base(title)
        {
        }

        /// <summary>
        ///     <para>
        ///         Get or set a value indicating whether only read-only files can be selected.
        ///     </para>
        ///     <para>
        ///         読み込み専用ファイルのみ選択可能にするかどうかを示す値を取得または設定します。
        ///     </para>
        /// </summary>
        public bool EnsureReadOnly { get; set; }

        /// <summary>
        ///     <para>
        ///         Get or set a value indicating whether items other than the file system can be selected.
        ///     </para>
        ///     <para>
        ///         ファイルシステム以外のアイテムを選択可能にするかどうかを示す値を取得または設定します。
        ///     </para>
        /// </summary>
        public bool AllowNonFileSystemItem { get; set; }

        /// <summary>
        ///     Get the collection of the empty file.
        /// </summary>
        private static IEnumerable<ShellFile> EmptyShellFiles { get; }

        /// <summary>
        ///     Displays a dialog for selecting single file.
        /// </summary>
        /// <returns>The selected file. If the user canceled it will return <c>null</c>.</returns>
        public async Task<ShellFile> SelectSingleFileAsync()
        {
            ShellFile result = null;

            this.multiSelect = false;

            await InvokeAsync(() =>
            {
                var dialogResult = ShowDialog();
                if (dialogResult == FileDialogResult.Ok)
                {
                    result = GetShellFiles().FirstOrDefault();
                }
            });

            return result;
        }

        /// <summary>
        ///     Displays a dialog for selecting multiple file.
        /// </summary>
        /// <returns>A collection of selected files.</returns>
        public async Task<IEnumerable<ShellFile>> SelectMultipleFilesAsync()
        {
            Contract.Ensures(Contract.Result<IEnumerable<ShellFile>>() != null);

            var result = EmptyShellFiles;

            this.multiSelect = true;

            await InvokeAsync(() =>
            {
                var dialogResult = ShowDialog();
                if (dialogResult == FileDialogResult.Ok)
                {
                    result = GetShellFiles();
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

        private static async Task InvokeAsync(Action action)
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