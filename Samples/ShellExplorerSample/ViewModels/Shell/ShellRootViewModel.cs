using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using System.Windows.Data;
using Reactive.Bindings;
using starshipxac.Windows.Shell.Media.Imaging;

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
            this.Thumbnail = new ReactiveProperty<ShellImageSource>();
            this.DetailThumbnail = new ReactiveProperty<ShellImageSource>();
            this.ShellFolders = new ReactiveCollection<ShellFolderViewModel>();
            this.SelectedFolder = new ReactiveProperty<ShellFolderViewModel>();
            this.ShellFolderCollectionView = CollectionViewSource.GetDefaultView(this.ShellFolders);

            #endregion
        }

        public static Task<ShellRootViewModel> CreateAsync()
        {
            var result = new ShellRootViewModel();

            return Task.FromResult(result);
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

        public override ReactiveProperty<ShellImageSource> Thumbnail { get; }

        public override ReactiveProperty<ShellImageSource> DetailThumbnail { get; }

        public ReactiveCollection<ShellFolderViewModel> ShellFolders { get; }

        public ICollectionView ShellFolderCollectionView { get; }

        public ReactiveProperty<ShellFolderViewModel> SelectedFolder { get; }
    }
}