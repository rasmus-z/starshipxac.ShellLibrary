using System;
using WindowsControlsSample.Views;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using starshipxac.MinimalMVVM;

namespace WindowsControlsSample.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            #region RectiveProperty

            this.ShowProgressViewCommand = new AsyncReactiveCommand();
            this.ShowProgressViewCommand
                .Subscribe(async _ => await this.View.ShowProgressViewAsync())
                .AddTo(this.CompositeDisposable);

            #endregion
        }

        public void Loaded(dynamic view)
        {
            this.View = view as IMainView;
        }

        public IMainView View { get; private set; }

        public AsyncReactiveCommand ShowProgressViewCommand { get; }
    }
}