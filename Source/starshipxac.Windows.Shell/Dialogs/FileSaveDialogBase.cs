using System;
using System.Diagnostics.Contracts;
using System.Linq;
using starshipxac.Shell;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.Dialogs.Interop;

namespace starshipxac.Windows.Shell.Dialogs
{
    public abstract class FileSaveDialogBase : FileDialogBase
    {
        private bool validateNames = true;
        private bool appendExtension = true;
        private bool restoreDirectory = true;
        private bool addToMruList = true;
        private bool setFilter = false;
        private string defaultFileExtension;

        protected FileSaveDialogBase()
        {
            this.FileTypeFilters = new FileTypeFilterCollection();
        }

        protected FileSaveDialogBase(string title)
            : base(title)
        {
            this.FileTypeFilters = new FileTypeFilterCollection();
        }

        [ContractInvariantMethod]
        private void ObjectInvarinat()
        {
            Contract.Invariant(this.FileTypeFilters != null);
        }

        /// <summary>
        ///     ファイル名を検証するかどうかを示す値を取得または設定します。
        /// </summary>
        /// <exception cref="InvalidOperationException">ダイアログ表示中は変更できません。</exception>
        public bool ValidateNames
        {
            get
            {
                return this.validateNames;
            }
            set
            {
                ThrowIfDialogShowingPropertyCannotBeChanged();
                this.validateNames = value;
            }
        }

        /// <summary>
        ///     ユーザーが拡張子を指定しない場合、ファイル名に自動的に拡張子を付加するかどうかを示す値を取得または設定します。
        /// </summary>
        /// <exception cref="InvalidOperationException">ダイアログ表示中は変更できません。</exception>
        public bool AppendExtension
        {
            get
            {
                return this.appendExtension;
            }
            set
            {
                ThrowIfDialogShowingPropertyCannotBeChanged();
                this.appendExtension = value;
            }
        }

        /// <summary>
        ///     終了後にディレクトリを元の位置に戻すかどうかを示す値を取得または設定します。
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
        ///     指定したファイルを最近使用したファイル一覧に追加するかどうかを判定する値を取得または設定します。
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
        ///     プロパティを編集できるかどうかを判定する値を取得または設定します。
        /// </summary>
        public bool AllowPropertyEditing { get; set; }

        /// <summary>
        ///     規定のファイル名を取得または設定します。
        /// </summary>
        public string DefaultFileName { get; set; }

        /// <summary>
        ///     ファイル名に追加する拡張子を取得または設定します。
        ///     <c>null</c>または空文字列の場合は、ファイル名に拡張子を追加しません。
        /// </summary>
        public string DefaultFileExtension
        {
            get
            {
                return this.defaultFileExtension;
            }
            set
            {
                this.defaultFileExtension = value;
                this.AppendExtension = !String.IsNullOrWhiteSpace(this.defaultFileExtension);
            }
        }

        /// <summary>
        ///     ファイルダイアログボックスのファイルの種類に表示されるフィルターのコレクションを取得します。
        /// </summary>
        public FileTypeFilterCollection FileTypeFilters { get; }

        /// <summary>
        ///     ファイルダイアログボックスで現在選択されているフィルターを取得します。
        /// </summary>
        public FileTypeFilter SelectedFileTypeFilter
        {
            get
            {
                var nativeDialog = (IFileSaveDialog)this.FileDialogInternal.FileDialogNative;
                if (nativeDialog == null)
                {
                    return null;
                }

                uint fileType;
                nativeDialog.GetFileTypeIndex(out fileType);
                return this.FileTypeFilters[(int)fileType];
            }
        }

        /// <summary>
        ///     ファイルダイアログボックスで現在選択されているフィルターのインデックスを取得します。
        /// </summary>
        public int SelectedFileTypeFilterIndex
        {
            get
            {
                var nativeDialog = (IFileSaveDialog)this.FileDialogInternal.FileDialogNative;
                if (nativeDialog == null)
                {
                    return -1;
                }

                uint fileType;
                nativeDialog.GetFileTypeIndex(out fileType);
                return (int)fileType;
            }
        }

        /// <summary>
        ///     ユーザーが指定したファイル情報を取得します。
        /// </summary>
        /// <returns></returns>
        public ShellFile GetShellFile()
        {
            var fileDialogNative = (IFileSaveDialog)this.FileDialogInternal.FileDialogNative;
            IShellItem shellItem;
            fileDialogNative.GetResult(out shellItem);
            if (shellItem == null)
            {
                return null;
            }
            return ShellFactory.FromShellItem(new ShellItem((IShellItem2)shellItem)) as ShellFile;
        }

        /// <summary>
        ///     上書き保存ダイアログの初期ファイル情報を設定します。
        /// </summary>
        /// <param name="shellFile">初期ファイル情報。</param>
        public void SetSaveAsFile(ShellFile shellFile)
        {
            Contract.Requires<ArgumentNullException>(shellFile != null);

            var fileDialogNative = (IFileSaveDialog)this.FileDialogInternal.FileDialogNative;
            fileDialogNative.SetSaveAsItem(shellFile.ShellItem.ShellItemInterface);
        }

        #region FileDialogBase Members

        internal override IFileDialog2 CreateNativeFileDialog()
        {
            return new FileSaveDialogNative();
        }

        protected override FileDialogOptions GetDialogOptions()
        {
            var result = base.GetDialogOptions();

            if (this.appendExtension)
            {
                result |= FileDialogOptions.AppendDefaultExtension;
            }
            if (this.restoreDirectory)
            {
                result |= FileDialogOptions.RestoreDirectory;
            }

            return result;
        }

        /// <summary>
        ///     ネイティブダイアログに設定を適用します。
        /// </summary>
        protected override void SetNativeSettings()
        {
            base.SetNativeSettings();

            // フィルター
            if (this.FileTypeFilters.Any() && !this.setFilter)
            {
                SetFilter();
            }

            // デフォルトファイル名
            if (!String.IsNullOrWhiteSpace(this.DefaultFileName))
            {
                this.FileDialogInternal.SetDefaultFileName(this.DefaultFileName);
            }

            // デフォルト拡張子
            if (!String.IsNullOrEmpty(this.DefaultFileExtension))
            {
                this.FileDialogInternal.SetDefaultExtension(this.DefaultFileExtension);
            }
        }

        /// <summary>
        ///     ファイルダイアログにフィルターを設定します。
        /// </summary>
        private void SetFilter()
        {
            this.FileDialogInternal.SetFilters(this.FileTypeFilters);
            this.setFilter = true;

            if (!String.IsNullOrWhiteSpace(this.DefaultFileExtension))
            {
                for (var index = 0; index < this.FileTypeFilters.Count; ++index)
                {
                    var filter = this.FileTypeFilters[index];
                    if (filter.Extensions.Contains(this.DefaultFileExtension))
                    {
                        this.FileDialogInternal.SetFilterIndex(index + 1);
                        break;
                    }
                }
            }
        }

        #endregion
    }
}