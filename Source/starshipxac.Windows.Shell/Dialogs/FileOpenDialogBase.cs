using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using starshipxac.Shell;
using starshipxac.Shell.Internal;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.Dialogs.Interop;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    /// ファイル選択ダイアログの基底クラスを定義します。
    /// </summary>
    public abstract class FileOpenDialogBase : FileDialogBase
    {
        private bool restoreDirectory = true;
        private bool addToMruList = true;
        private bool setFilter = false;

        /// <summary>
        /// <see cref="FileOpenDialogBase"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        protected FileOpenDialogBase()
        {
            this.FileTypeFilters = new FileTypeFilterCollection();
        }

        /// <summary>
        /// ダイアログのタイトルを指定して、
        /// <see cref="FileOpenDialogBase"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="title"></param>
        protected FileOpenDialogBase(string title)
            : base(title)
        {
            this.FileTypeFilters = new FileTypeFilterCollection();
        }

        [ContractInvariantMethod]
        private void CommonFileDialogBaseInvaliant()
        {
            Contract.Invariant(this.FileTypeFilters != null);
        }

        /// <summary>
        /// 終了後にディレクトリを元の位置に戻すかどうかを示す値を取得または設定します。
        /// </summary>
        /// <exception cref="InvalidOperationException">ダイアログ表示中は変更できません。</exception>
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
        /// 指定したファイルを最近使用したファイル一覧に追加するかどうかを判定する値を取得または設定します。
        /// </summary>
        /// <exception cref="InvalidOperationException">ダイアログ表示中は変更できません。</exception>
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
        /// ファイルダイアログボックスのファイルの種類に表示されるフィルターのコレクションを取得します。
        /// </summary>
        public FileTypeFilterCollection FileTypeFilters { get; private set; }

        /// <summary>
        /// ファイルダイアログボックスで現在選択されているフィルターを取得します。
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
        /// ファイルダイアログボックスで現在選択されているフィルターのインデックスを取得します。
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
        /// ユーザーが選択したファイル情報のコレクションを取得します。
        /// </summary>
        /// <returns>ファイル情報のコレクション。</returns>
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
        /// <see cref="FileOpenDialogNative"/>を作成します。
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
        /// ネイティブダイアログに設定を適用します。
        /// </summary>
        protected override void SetNativeSettings()
        {
            base.SetNativeSettings();

            // フィルター
            if (this.FileTypeFilters.Any() && !this.setFilter)
            {
                this.FileDialogInternal.SetFilters(this.FileTypeFilters);
                this.setFilter = true;
            }
        }
    }
}