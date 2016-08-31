using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Windows.Data;
using Reactive.Bindings;

namespace ShellExplorerSample.ViewModels.Shell
{
    public sealed class ShellRootViewModel : ShellObjectViewModel
    {
        public ShellRootViewModel()
            : base(null)
        {
            #region Reactive Property

            this.DisplayName = new ReactiveProperty<string>(String.Empty);
            this.ItemTypeText = new ReactiveProperty<string>(String.Empty);
            this.DateCreated = new ReactiveProperty<DateTime>(DateTime.MinValue);
            this.DateModified = new ReactiveProperty<DateTime>(DateTime.MinValue);
            this.ShellFolders = new ReactiveCollection<ShellFolderViewModel>();
            this.SelectedFolder = new ReactiveProperty<ShellFolderViewModel>();
            this.ShellFolderCollectionView = CollectionViewSource.GetDefaultView(this.ShellFolders);

            #endregion
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

        public ReactiveCollection<ShellFolderViewModel> ShellFolders { get; }

        public ICollectionView ShellFolderCollectionView { get; }

        public ReactiveProperty<ShellFolderViewModel> SelectedFolder { get; }
    }
}