using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Windows.Data;
using Livet;
using Reactive.Bindings;
using ShellExplorerSample.ViewModels.Shell;
using starshipxac.Shell;

namespace ShellExplorerSample.ViewModels
{
    /// <summary>
    /// Shell Explorer Sample View Model.
    /// </summary>
    /// <remarks>
    /// Gitライブラリフォルダーの<see cref="ShellLibrary.EnumerateItems"/>メソッドで <c>FileNotFoundException</c>が発生する場合は、
    /// プロジェクトのプロパティ -> ビルドの「32ビットの優先」のチェックを外す。
    /// </remarks>
    public class MainWindowViewModel : ViewModel
    {
        /// <summary>
        /// <see cref="MainWindowViewModel"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MainWindowViewModel()
        {
            #region ReactiveProperty

            this.RootFolders = new ReactiveProperty<ObservableSynchronizedCollection<ShellFolderViewModel>>();

            this.SelectedFolder = new ReactiveProperty<ShellFolderViewModel>();

            this.ShellItems = this.SelectedFolder
                .Select(CreateShellItems)
                .ToReactiveProperty();

            #endregion
        }

        /// <summary>
        /// <c>ContentRendered</c>イベントが発生すると呼ばれます。
        /// </summary>
        public void Initialize()
        {
            ShellViewModelFactory.Initialize();

            this.RootFolders.Value = new ObservableSynchronizedCollection<ShellFolderViewModel>()
            {
                ShellViewModelFactory.CreateRoot(ShellKnownFolders.OneDrive),
                ShellViewModelFactory.CreateRoot(ShellKnownFolders.HomeGroup),
                ShellViewModelFactory.CreateRoot(ShellKnownFolders.Computer),
                ShellViewModelFactory.CreateRoot(ShellKnownFolders.Libraries),
            };
            this.RootFolderCollectionView = CollectionViewSource.GetDefaultView(this.RootFolders.Value);
        }

        /// <summary>
        /// ルートフォルダーのコレクションを取得します。
        /// </summary>
        public ReactiveProperty<ObservableSynchronizedCollection<ShellFolderViewModel>> RootFolders { get; private set; }

        /// <summary>
        /// 選択中のフォルダーを取得します。
        /// </summary>
        public ReactiveProperty<ShellFolderViewModel> SelectedFolder { get; private set; }

        /// <summary>
        /// 選択中のフォルダーに含まれるファイルまたはフォルダーのコレクションを取得します。
        /// </summary>
        public ReactiveProperty<ObservableSynchronizedCollection<ShellObjectViewModel>> ShellItems { get; private set; }

        private ICollectionView RootFolderCollectionView { get; set; }

        private ICollectionView ShellItemCollectionView { get; set; }

        private ObservableSynchronizedCollection<ShellObjectViewModel> CreateShellItems(ShellFolderViewModel folder)
        {
            var result = new ObservableSynchronizedCollection<ShellObjectViewModel>();
            if (folder != null)
            {
                foreach (var item in folder.EnumerateItems())
                {
                    result.Add(item);
                }
            }

            this.ShellItemCollectionView = CollectionViewSource.GetDefaultView(result);

            return result;
        }
    }
}