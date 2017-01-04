using System;
using System.ComponentModel;
using System.Windows.Data;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using starshipxac.MinimalMVVM;
using starshipxac.Shell;

namespace ShellKnownFoldersSample.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            #region Reactive Property

            this.KnownFolders = new ReactiveCollection<ShellKnownFolderViewModel>()
                .AddTo(this.CompositeDisposable);
            this.KnownFoldersSource = CollectionViewSource.GetDefaultView(this.KnownFolders);

            #endregion
        }

        public async void Loaded()
        {
            foreach (var knownFolder in ShellKnownFolders.EnumerateKnownFolders())
            {
                this.KnownFolders.Add(await ShellKnownFolderViewModel.CreateAsync(knownFolder));
            }
        }

        public ICollectionView KnownFoldersSource { get; }
        public ReactiveCollection<ShellKnownFolderViewModel> KnownFolders { get; }
    }
}