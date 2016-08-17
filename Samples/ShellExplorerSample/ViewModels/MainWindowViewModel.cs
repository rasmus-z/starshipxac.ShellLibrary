using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using System.Windows.Data;
using Livet;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
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
    public class MainWindowViewModel : ViewModel
    {
        /// <summary>
        ///     <see cref="MainWindowViewModel" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MainWindowViewModel()
        {
            ShellViewModelFactory.CreateFactory();

            #region Reactive Property

            this.RootFolder = new ReactiveProperty<ShellRootViewModel>();
            this.ShellItems = new ReactiveCollection<ShellObjectViewModel>();
            this.ShellItemCollectionView = CollectionViewSource.GetDefaultView(this.ShellItems);

            #endregion
        }

        /// <summary>
        ///     <c>ContentRendered</c>イベントが発生すると呼ばれます。
        /// </summary>
        public void Initialize()
        {
            DispatcherHelper.UIDispatcher.InvokeAsync(async () =>
            {
                this.RootFolder.Value = await ShellRootViewModel.CreateAsync();

                this.RootFolder.Value.ShellFolders.Add(await ShellViewModelFactory.CreateFolderAsync(ShellKnownFolders.OneDrive));
                this.RootFolder.Value.ShellFolders.Add(await ShellViewModelFactory.CreateFolderAsync(ShellKnownFolders.HomeGroup));
                this.RootFolder.Value.ShellFolders.Add(await ShellViewModelFactory.CreateFolderAsync(ShellKnownFolders.Computer));
                this.RootFolder.Value.ShellFolders.Add(await ShellViewModelFactory.CreateFolderAsync(ShellKnownFolders.Libraries));

                this.RootFolder.Value.SelectedFolder
                    .Subscribe(async folder => await CreateShellItemsAsync(folder))
                    .AddTo(this.CompositeDisposable);
            });
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

        private async Task CreateShellItemsAsync(ShellFolderViewModel folder)
        {
            this.ShellItems.Clear();

            if (folder != null)
            {
                foreach (var item in await folder.EnumerateItemsAsync())
                {
                    this.ShellItems.Add(item);
                }
            }
        }
    }
}