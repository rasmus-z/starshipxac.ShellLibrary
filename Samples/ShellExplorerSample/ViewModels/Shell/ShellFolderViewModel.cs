using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Data;
using Livet;
using Reactive.Bindings;
using starshipxac.Shell;
using starshipxac.Windows.Shell.Media.Imaging;

namespace ShellExplorerSample.ViewModels.Shell
{
    /// <summary>
    /// フォルダー情報の<c>ViewModel</c>を定義します。
    /// </summary>
    public class ShellFolderViewModel : ShellObjectViewModel
    {
        /// <summary>
        /// <see cref="ShellFolder"/>および親フォルダーを指定して、
        /// <see cref="ShellFolderViewModel"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellFolder"></param>
        /// <param name="parentFolder"></param>
        public ShellFolderViewModel(ShellFolder shellFolder, ShellFolderViewModel parentFolder)
            : base(shellFolder, parentFolder)
        {
            Contract.Requires<ArgumentNullException>(shellFolder != null);
            Contract.Requires<ArgumentNullException>(parentFolder != null);

            this.ShellFolder = shellFolder;
            InitializeReactiveProperties();
        }

        /// <summary>
        /// <see cref="ShellFolder"/>および<see cref="ShellThumbnailFactory"/>を指定して、
        /// <see cref="ShellFolderViewModel"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellFolder"></param>
        /// <param name="thumbnailFactory"></param>
        public ShellFolderViewModel(ShellFolder shellFolder, ShellThumbnailFactory thumbnailFactory)
            : base(shellFolder, thumbnailFactory)
        {
            Contract.Requires<ArgumentNullException>(shellFolder != null);
            Contract.Requires<ArgumentNullException>(thumbnailFactory != null);

            this.ShellFolder = shellFolder;
            InitializeReactiveProperties();
        }

        /// <summary>
        /// 親フォルダーを指定して、
        /// <see cref="ShellFolderViewModel"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="parentFolder"></param>
        protected ShellFolderViewModel(ShellFolderViewModel parentFolder)
            : base(parentFolder)
        {
            Contract.Requires<ArgumentNullException>(parentFolder != null);

            this.ShellFolder = null;
            InitializeReactiveProperties();
        }

        #region ReactiveProperty

        /// <summary>
        /// <c>ReactiveProperty</c>を初期化します。
        /// </summary>
        private void InitializeReactiveProperties()
        {
            this.IsExpanded = new ReactiveProperty<bool>(false);

            this.IsSelected = new ReactiveProperty<bool>(false);

            if (this.ShellFolder == null)
            {
                this.ShellFolders = new ReactiveProperty<ObservableSynchronizedCollection<ShellFolderViewModel>>(
                    new ObservableSynchronizedCollection<ShellFolderViewModel>());
            }
            else
            {
                this.ShellFolders = this.IsExpanded
                    .Select(CreateShellFolders)
                    .ToReactiveProperty();
            }
        }

        #endregion

        public ShellFolder ShellFolder { get; private set; }

        /// <summary>
        /// フォルダーツリーで、フォルダーが展開されているかどうかを判定する値を取得します。
        /// </summary>
        public ReactiveProperty<bool> IsExpanded { get; private set; }

        /// <summary>
        /// フォルダーツリー上で、フォルダーが選択されているかどうかを判定する値を取得します。
        /// </summary>
        public ReactiveProperty<bool> IsSelected { get; private set; }

        /// <summary>
        /// 子フォルダーのコレクションを取得します。
        /// </summary>
        public ReactiveProperty<ObservableSynchronizedCollection<ShellFolderViewModel>> ShellFolders { get; private set; }

        private ICollectionView ShellItemCollectionView { get; set; }

        private ICollectionView ShellFolderCollectionView { get; set; }

        /// <summary>
        /// フォルダー内のファイル/フォルダーを列挙します。
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ShellObjectViewModel> EnumerateItems()
        {
            return this.ShellFolder.EnumerateItems()
                .Select(x => ShellViewModelFactory.Create(x, this));
        }

        /// <summary>
        /// フォルダー内のフォルダーを列挙します。
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ShellFolderViewModel> EnumerateFolders()
        {
            return this.ShellFolder.EnumerateFolders()
                .Select(x => ShellViewModelFactory.CreateFolder(x, this));
        }

        private ObservableSynchronizedCollection<ShellFolderViewModel> CreateShellFolders(bool expanded)
        {
            var result = new ObservableSynchronizedCollection<ShellFolderViewModel>();
            if (expanded)
            {
                try
                {
                    foreach (var folder in EnumerateFolders())
                    {
                        Debug.WriteLine(String.Format("  -> {0}", folder.DisplayName.Value));
                        result.Add(folder);
                    }
                }
                catch (DirectoryNotFoundException ex)
                {
                    Debug.WriteLine(String.Format("{0}: {1}", ex.GetType().Name, ex.Message));
                    result.Clear();
                }
            }
            else
            {
                result.Add(new PlaceholderViewModel(this));
            }

            this.ShellFolderCollectionView = CollectionViewSource.GetDefaultView(result);

            return result;
        }
    }
}