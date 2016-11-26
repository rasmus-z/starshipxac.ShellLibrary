using System;
using System.ComponentModel;
using System.Windows.Data;
using Livet;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using starshipxac.Shell;

namespace ShellKnownFoldersSample.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            #region Reactive Property

            this.KnownFolders = new ReactiveCollection<ShellKnownFolderViewModel>()
                .AddTo(this.CompositeDisposable);
            this.KnownFoldersSource = CollectionViewSource.GetDefaultView(this.KnownFolders);

            #endregion
        }

        public void Initialize()
        {
            DispatcherHelper.UIDispatcher.InvokeAsync(async () =>
            {
                foreach (var knownFolder in ShellKnownFolders.EnumerateKnownFolders())
                {
                    this.KnownFolders.AddOnScheduler(await ShellKnownFolderViewModel.CreateAsync(knownFolder));
                }
            });
        }

        public ICollectionView KnownFoldersSource { get; }
        public ReactiveCollection<ShellKnownFolderViewModel> KnownFolders { get; }
    }
}