using System;
using System.Collections.Generic;
using starshipxac.Shell;
using starshipxac.Shell.Internal;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.Dialogs.Interop;

namespace starshipxac.Windows.Shell.Dialogs
{
    public abstract class FolderSelectDialogBase : FileDialogBase
    {
        private bool forceFileSystem = false;

        /// <summary>
        ///     Initialize a new instance of the <see cref="FolderSelectDialogBase" /> class.
        /// </summary>
        protected FolderSelectDialogBase()
        {
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="FolderSelectDialogBase" /> class
        ///     to the specified dialog title.
        /// </summary>
        /// <param name="title">Dialog title.</param>
        protected FolderSelectDialogBase(string title)
            : base(title)
        {
        }

        /// <summary>
        ///     <para>
        ///         Get or set a value indicating whether only the folder on the file system can be selected.
        ///     </para>
        ///     <para>
        ///         ファイルシステム上のフォルダーのみ選択可能にするかどうかを示す値を取得または設定します。
        ///     </para>
        /// </summary>
        public bool ForceFileSystem
        {
            get
            {
                return this.forceFileSystem;
            }
            set
            {
                ThrowIfDialogShowingPropertyCannotBeChanged();
                this.forceFileSystem = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Get a collection of folders selected by the user.
        ///     </para>
        ///     <para>
        ///         ユーザーが選択したフォルダー情報のコレクションを取得します。
        ///     </para>
        /// </summary>
        /// <returns>Collection of folder.</returns>
        public IEnumerable<ShellFolder> GetShellFolders()
        {
            var fileDialogNative = (IFileOpenDialog)this.FileDialogInternal.FileDialogNative;

            IShellItemArray shellItemArray;
            fileDialogNative.GetResults(out shellItemArray);

            var count = ShellItemArray.GetShellItemCount(shellItemArray);
            for (var index = 0; index < count; ++index)
            {
                var shellItem = ShellItemArray.GetShellItemAt(shellItemArray, index);
                var shellFolder = ShellFactory.FromShellItem(new ShellItem((IShellItem2)shellItem)) as ShellFolder;
                if (shellFolder != null)
                {
                    yield return shellFolder;
                }
            }
        }

        /// <summary>
        ///     Create the <see cref="IFileDialog2" />.
        /// </summary>
        /// <returns></returns>
        internal override IFileDialog2 CreateNativeFileDialog()
        {
            return new FileOpenDialogNative();
        }

        /// <summary>
        ///     Get the <see cref="FileDialogOptions" />.
        /// </summary>
        /// <returns></returns>
        protected override FileDialogOptions GetDialogOptions()
        {
            var result = base.GetDialogOptions();

            result |= FileDialogOptions.SelectFolers;

            if (this.ForceFileSystem)
            {
                result |= FileDialogOptions.ForceFileSystem;
            }

            return result;
        }
    }
}