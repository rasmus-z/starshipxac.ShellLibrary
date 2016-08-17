using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using System.Windows.Data;
using Reactive.Bindings;
using starshipxac.Shell;
using starshipxac.Shell.Media.Imaging;

namespace ShellExplorerSample.ViewModels.Shell
{
    public sealed class ShellRootViewModel : ShellObjectViewModel
    {
        private ShellRootViewModel()
            : base(null)
        {
            #region Reactive Property

            this.DisplayName = new ReactiveProperty<string>(String.Empty);
            this.ItemTypeText = new ReactiveProperty<string>(String.Empty);
            this.DateCreated = new ReactiveProperty<DateTime>(DateTime.MinValue);
            this.DateModified = new ReactiveProperty<DateTime>(DateTime.MinValue);
            this.Thumbnail = new ReactiveProperty<ShellThumbnail>();
            this.ShellFolders = new ReactiveCollection<ShellFolderViewModel>();
            this.SelectedFolder = new ReactiveProperty<ShellFolderViewModel>();
            this.ShellFolderCollectionView = CollectionViewSource.GetDefaultView(this.ShellFolders);

            #endregion
        }

        public static async Task<ShellRootViewModel> CreateAsync()
        {
            var result = new ShellRootViewModel();

            result.ShellFolders.Add(await ShellViewModelFactory.CreateFolderAsync(ShellKnownFolders.OneDrive));
            result.ShellFolders.Add(await ShellViewModelFactory.CreateFolderAsync(ShellKnownFolders.HomeGroup));
            result.ShellFolders.Add(await ShellViewModelFactory.CreateFolderAsync(ShellKnownFolders.Computer));
            result.ShellFolders.Add(await ShellViewModelFactory.CreateFolderAsync(ShellKnownFolders.Libraries));

            return result;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.SelectedFolder != null);
        }

        public override ReactiveProperty<string> DisplayName { get; }

        public override ReactiveProperty<string> ItemTypeText { get; }

        public override ReactiveProperty<DateTime> DateCreated { get; }

        public override ReactiveProperty<DateTime> DateModified { get; }

        public override ReactiveProperty<ShellThumbnail> Thumbnail { get; }

        public ReactiveCollection<ShellFolderViewModel> ShellFolders { get; }

        public ICollectionView ShellFolderCollectionView { get; }

        public ReactiveProperty<ShellFolderViewModel> SelectedFolder { get; }
    }
}