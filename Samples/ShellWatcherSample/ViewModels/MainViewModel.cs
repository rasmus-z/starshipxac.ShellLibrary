using System;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using starshipxac.MinimalMVVM;
using starshipxac.Shell;
using starshipxac.Shell.Components;

namespace ShellWatcherSample.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private ShellWatcher shellWatcher;

        public MainViewModel()
        {
            var rootFolder = ShellKnownFolders.Pictures;
            this.RootFolder = new ReactiveProperty<ShellFolder>(rootFolder);
        }

        public async void Loaded(dynamic view)
        {
            this.View = view as IMainView;

            this.View.WriteLine(String.Empty);
            this.View.WriteLine($"RootFolder: {this.RootFolder.Value.Path}");

            this.shellWatcher = await ShellWatcher.CreateAsync(this.RootFolder.Value, true);

            this.shellWatcher.CreatedAsObservable()
                .Subscribe(e =>
                {
                    this.View.WriteLine($"Create: {e.ShellObject}");
                })
                .AddTo(this.CompositeDisposable);

            this.shellWatcher.DeletedAsObservable()
                .Subscribe(e =>
                {
                    this.View.WriteLine($"Delete: {e.ShellObject}");
                })
                .AddTo(this.CompositeDisposable);

            this.shellWatcher.RenamtedAsObservable()
                .Subscribe(e =>
                {
                    this.View.WriteLine($"Rename: {e.ShellObject} -> {e.NewShellObject}");
                })
                .AddTo(this.CompositeDisposable);

            this.shellWatcher.Start();
        }

        public void Initialize()
        {
        }

        public IMainView View { get; private set; }

        public ReactiveProperty<ShellFolder> RootFolder { get; }
    }
}