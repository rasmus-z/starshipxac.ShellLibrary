using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Windows.Data;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using starshipxac.Shell;
using starshipxac.Shell.PropertySystem;
using starshipxac.Windows.Shell.Media.Imaging;

namespace ShellExplorerSample.ViewModels.Shell
{
    /// <summary>
    ///     フォルダー情報の<c>ViewModel</c>を定義します。
    /// </summary>
    public class ShellFolderViewModel : ShellObjectViewModel
    {
        /// <summary>
        ///     <see cref="ShellFolder" />および親フォルダーを指定して、
        ///     <see cref="ShellFolderViewModel" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellFolder"></param>
        /// <param name="thumbnailFactory"></param>
        public ShellFolderViewModel(ShellFolder shellFolder, ShellThumbnailFactory thumbnailFactory)
            : base(shellFolder, thumbnailFactory)
        {
            Contract.Requires<ArgumentNullException>(shellFolder != null);
            Contract.Requires<ArgumentNullException>(thumbnailFactory != null);

            #region Reactive Property

            this.DisplayName = new ReactiveProperty<string>(this.ShellFolder.DisplayName);
            this.ItemTypeText = new ReactiveProperty<string>(
                new ShellProperty<string>(this.ShellFolder, "System.ItemTypeText").Value);
            this.DateCreated = new ReactiveProperty<DateTime>(this.ShellFolder.DateCreated);
            this.DateModified = new ReactiveProperty<DateTime>(this.ShellFolder.DateModified);
            this.Thumbnail = new ReactiveProperty<ShellThumbnail>(
                new ShellThumbnail(this.ShellFolder, this.ThumbnailFactory));
            this.IsExpanded = new ReactiveProperty<bool>(false);
            this.IsSelected = new ReactiveProperty<bool>(false);
            this.ShellFolders = new ReactiveCollection<ShellFolderViewModel>();
            this.ShellFolderCollectionView = CollectionViewSource.GetDefaultView(this.ShellFolders);

            this.IsExpanded
                .Subscribe(CreateShellFolders)
                .AddTo(this.CompositeDisposable);

            #endregion
        }

        internal ShellFolderViewModel(ShellThumbnailFactory thumbnailFactory)
            : base(null, thumbnailFactory)
        {
            Contract.Requires<ArgumentNullException>(thumbnailFactory != null);

            #region Reactive Property

            this.DisplayName = new ReactiveProperty<string>(String.Empty);
            this.ItemTypeText = new ReactiveProperty<string>(String.Empty);
            this.DateCreated = new ReactiveProperty<DateTime>(DateTime.MinValue);
            this.DateModified = new ReactiveProperty<DateTime>(DateTime.MinValue);
            this.Thumbnail = new ReactiveProperty<ShellThumbnail>();
            this.IsExpanded = new ReactiveProperty<bool>();
            this.IsSelected = new ReactiveProperty<bool>();
            this.ShellFolders = new ReactiveCollection<ShellFolderViewModel>();
            this.ShellFolderCollectionView = CollectionViewSource.GetDefaultView(this.ShellFolders);

            #endregion
        }

        public ShellFolder ShellFolder => (ShellFolder)this.ShellObject;

        public override ReactiveProperty<string> DisplayName { get; }

        public override ReactiveProperty<string> ItemTypeText { get; }

        public override ReactiveProperty<DateTime> DateCreated { get; }

        public override ReactiveProperty<DateTime> DateModified { get; }

        public override ReactiveProperty<ShellThumbnail> Thumbnail { get; }

        /// <summary>
        ///     フォルダーツリーで、フォルダーが展開されているかどうかを判定する値を取得します。
        /// </summary>
        public ReactiveProperty<bool> IsExpanded { get; }

        /// <summary>
        ///     フォルダーツリー上で、フォルダーが選択されているかどうかを判定する値を取得します。
        /// </summary>
        public ReactiveProperty<bool> IsSelected { get; }

        /// <summary>
        ///     子フォルダーのコレクションを取得します。
        /// </summary>
        public ReactiveCollection<ShellFolderViewModel> ShellFolders { get; }

        public ICollectionView ShellFolderCollectionView { get; }

        /// <summary>
        ///     フォルダー内のファイル/フォルダーを列挙します。
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ShellObjectViewModel> EnumerateItems()
        {
            return this.ShellFolder.EnumerateObjects()
                .Select(ShellViewModelFactory.Create);
        }

        /// <summary>
        ///     フォルダー内のフォルダーを列挙します。
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ShellFolderViewModel> EnumerateFolders()
        {
            return this.ShellFolder.EnumerateFolders()
                .Select(ShellViewModelFactory.CreateFolder);
        }

        private void CreateShellFolders(bool expanded)
        {
            if (expanded)
            {
                try
                {
                    foreach (var folder in EnumerateFolders())
                    {
                        Debug.WriteLine($"  -> {folder.DisplayName.Value}");
                        this.ShellFolders.AddOnScheduler(folder);
                    }
                }
                catch (DirectoryNotFoundException ex)
                {
                    Debug.WriteLine($"{ex.GetType().Name}: {ex.Message}");
                    this.ShellFolders.ClearOnScheduler();
                }
            }
            else
            {
                this.ShellFolders.AddOnScheduler(new PlaceholderViewModel(this.ThumbnailFactory));
            }
        }
    }
}