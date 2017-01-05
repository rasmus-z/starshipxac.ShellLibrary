using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Windows.Data;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using starshipxac.MinimalMVVM;
using starshipxac.Shell;
using ShellExplorerSample.ViewModels.Shell;

namespace ShellExplorerSample.ViewModels
{
    /// <summary>
    ///     Shell Explorer Sample View Model.
    /// </summary>
    /// <remarks>
    ///     Gitライブラリフォルダーの<see cref="starshipxac.Shell.ShellFolder.EnumerateObjects" />メソッドで
    ///     <c>FileNotFoundException</c>が発生する場合は、
    ///     プロジェクトのプロパティ -> ビルドの「32ビットの優先」のチェックを外す。
    /// </remarks>
    public class MainViewModel : ViewModel
    {
        /// <summary>
        ///     <see cref="MainViewModel" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MainViewModel()
        {
            ShellViewModelFactory.CreateFactory();

            #region Reactive Property

            this.RootFolder = new ReactiveProperty<ShellRootViewModel>();
            this.ShellItems = new ReactiveCollection<ShellObjectViewModel>();
            this.ShellItemCollectionView = CollectionViewSource.GetDefaultView(this.ShellItems);

            #endregion
        }

        public void Loaded()
        {
            this.RootFolder.Value = ShellViewModelFactory.CreateRoot();

            this.RootFolder.Value.ShellFolders.Add(ShellViewModelFactory.CreateFolder(ShellKnownFolders.OneDrive));
            this.RootFolder.Value.ShellFolders.Add(ShellViewModelFactory.CreateFolder(ShellKnownFolders.HomeGroup));
            this.RootFolder.Value.ShellFolders.Add(ShellViewModelFactory.CreateFolder(ShellKnownFolders.Computer));
            this.RootFolder.Value.ShellFolders.Add(ShellViewModelFactory.CreateFolder(ShellKnownFolders.Libraries));

            this.RootFolder.Value.SelectedFolder
                .Subscribe(CreateShellItems)
                .AddTo(this.CompositeDisposable);
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.RootFolder != null);
            Contract.Invariant(this.ShellItems != null);
            Contract.Invariant(this.ShellItemCollectionView != null);
        }

        public ReactiveProperty<ShellRootViewModel> RootFolder { get; }

        /// <summary>
        ///     選択中のフォルダーに含まれるファイルまたはフォルダーのコレクションを取得します。
        /// </summary>
        public ReactiveCollection<ShellObjectViewModel> ShellItems { get; }

        public ICollectionView ShellItemCollectionView { get; }

        private void CreateShellItems(ShellFolderViewModel folder)
        {
            Debug.WriteLine($"MainViewModel.CreateShellItems({folder})");

            this.ShellItems.Clear();

            if (folder != null)
            {
                foreach (var item in folder.EnumerateItems())
                {
                    this.ShellItems.Add(item);
                }
            }
        }
    }
}