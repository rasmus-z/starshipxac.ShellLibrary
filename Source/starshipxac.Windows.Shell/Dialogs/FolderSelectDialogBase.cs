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

        protected FolderSelectDialogBase()
        {
        }

        protected FolderSelectDialogBase(string title)
            : base(title)
        {
        }

        /// <summary>
        ///     ファイルシステム上のフォルダーのみ選択可能にするかどうかを示す値を取得または設定します。
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
        ///     ユーザーが選択したフォルダー情報のコレクションを取得します。
        /// </summary>
        /// <returns>フォルダー情報のコレクション。</returns>
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

        internal override IFileDialog2 CreateNativeFileDialog()
        {
            return new FileOpenDialogNative();
        }

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