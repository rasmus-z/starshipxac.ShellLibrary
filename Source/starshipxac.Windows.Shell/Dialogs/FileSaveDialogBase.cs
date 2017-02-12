using System;
using System.Diagnostics.Contracts;
using System.Linq;
using starshipxac.Shell;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.Dialogs.Interop;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    ///     Define the base class of the file save dialog.
    /// </summary>
    public abstract class FileSaveDialogBase : FileDialogBase
    {
        private bool validateNames = true;
        private bool appendExtension = true;
        private bool restoreDirectory = true;
        private bool addToMruList = true;
        private bool setFilter = false;
        private string defaultFileExtension;

        /// <summary>
        ///     Initialize a new instance of the <see cref="FileSaveDialogBase" /> class.
        /// </summary>
        protected FileSaveDialogBase()
        {
            this.FileTypeFilters = new FileTypeFilterCollection();
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="FileSaveDialogBase" /> class
        ///     to the specified dialog title.
        /// </summary>
        /// <param name="title"></param>
        protected FileSaveDialogBase(string title)
            : base(title)
        {
            this.FileTypeFilters = new FileTypeFilterCollection();
        }

        /// <summary>
        ///     <para>
        ///         Get or set a value indicating whether to verify the file name.
        ///     </para>
        ///     <para>
        ///         ファイル名を検証するかどうかを示す値を取得または設定します。
        ///     </para>
        /// </summary>
        /// <exception cref="InvalidOperationException">It can not be changed while dialog is displayed.</exception>
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
        ///     <para>
        ///         Gets or sets a value indicating whether or not to append extension automatically to the file name
        ///         if the user does not specify an extension.
        ///     </para>
        ///     <para>
        ///         ユーザーが拡張子を指定しない場合、ファイル名に自動的に拡張子を付加するかどうかを示す値を取得または設定します。
        ///     </para>
        /// </summary>
        /// <exception cref="InvalidOperationException">It can not be changed while dialog is displayed.</exception>
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
        ///     <para>
        ///         Get or set a value indicating whether or not to restore the directory
        ///         to its original position after completion.
        ///     </para>
        ///     <para>
        ///         終了後にディレクトリを元の位置に戻すかどうかを示す値を取得または設定します。
        ///     </para>
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
        ///     <para>
        ///         Gets or sets a value that determines whether or not to add the specified file
        ///         to the recently used file list.
        ///     </para>
        ///     <para>
        ///         指定したファイルを最近使用したファイル一覧に追加するかどうかを判定する値を取得または設定します。
        ///     </para>
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
        ///     <para>
        ///         Gets the collection of filters displayed in the file type of the file dialog box.
        ///     </para>
        ///     <para>
        ///         ファイルダイアログボックスのファイルの種類に表示されるフィルターのコレクションを取得します。
        ///     </para>
        /// </summary>
        public FileTypeFilterCollection FileTypeFilters { get; }

        /// <summary>
        ///     <para>
        ///         Get the currently selected filter in the file dialog box.
        ///     </para>
        ///     <para>
        ///         ファイルダイアログボックスで現在選択されているフィルターを取得します。
        ///     </para>
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
        ///     <para>
        ///         Get the index of the currently selected filter in the file dialog box.
        ///     </para>
        ///     <para>
        ///         ファイルダイアログボックスで現在選択されているフィルターのインデックスを取得します。
        ///     </para>
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
        ///     Get the file selected by user.
        /// </summary>
        /// <returns><see cref="ShellFile" />.</returns>
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
        ///     <para>
        ///         Sets the initial file information of the overwrite save dialog.
        ///     </para>
        ///     <para>
        ///         上書き保存ダイアログの初期ファイル情報を設定します。
        ///     </para>
        /// </summary>
        /// <param name="shellFile">Initial file.</param>
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
        ///     Apply the setting to the native dialog.
        /// </summary>
        protected override void SetNativeSettings()
        {
            base.SetNativeSettings();

            // Filter
            if (this.FileTypeFilters.Any() && !this.setFilter)
            {
                SetFilter();
            }

            // Default file name.
            if (!String.IsNullOrWhiteSpace(this.DefaultFileName))
            {
                this.FileDialogInternal.SetDefaultFileName(this.DefaultFileName);
            }

            // Default file extension.
            if (!String.IsNullOrEmpty(this.DefaultFileExtension))
            {
                this.FileDialogInternal.SetDefaultExtension(this.DefaultFileExtension);
            }
        }

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