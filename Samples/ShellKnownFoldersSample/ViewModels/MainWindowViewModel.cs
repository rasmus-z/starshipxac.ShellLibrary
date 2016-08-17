using System;
using System.Threading.Tasks;
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

            #endregion
        }

        public void Initialize()
        {
            Task.Run(async () =>
            {
                foreach (var knownFolder in ShellKnownFolders.EnumerateKnownFolders())
                {
                    this.KnownFolders.AddOnScheduler(await ShellKnownFolderViewModel.CreateAsync(knownFolder));
                }
            })
            .Wait();
        }

        public ReactiveCollection<ShellKnownFolderViewModel> KnownFolders { get; }
    }
}