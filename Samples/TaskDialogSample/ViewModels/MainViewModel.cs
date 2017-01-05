using System;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using starshipxac.MinimalMVVM;

namespace TaskDialogSample.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            #region ReactiveProperty

            this.SimpleTaskDialogCommand = new AsyncReactiveCommand();
            this.SimpleTaskDialogCommand
                .Subscribe(async _ => await this.View.ShowSimpleTaskDialogAsync())
                .AddTo(this.CompositeDisposable);

            this.AllControlsTaskDialogCommand = new AsyncReactiveCommand();
            this.AllControlsTaskDialogCommand
                .Subscribe(async _ => await this.View.ShowAllControlsTaskDialogAsync())
                .AddTo(this.CompositeDisposable);

            this.CustomButtonTaskDialogCommand = new AsyncReactiveCommand();
            this.CustomButtonTaskDialogCommand
                .Subscribe(async _ => await this.View.ShowCustomButtonTaskDialogAsync())
                .AddTo(this.CompositeDisposable);

            this.CommonLinkTaskDialogCommand = new AsyncReactiveCommand();
            this.CommonLinkTaskDialogCommand
                .Subscribe(async _ => await this.View.ShowCommandLinkTaskDialogAsync())
                .AddTo(this.CompositeDisposable);

            this.RadioButtonTaskDialogCommand = new AsyncReactiveCommand();
            this.RadioButtonTaskDialogCommand
                .Subscribe(async _ => await this.View.ShowRadioButtonTaskDialogAsync())
                .AddTo(this.CompositeDisposable);

            this.MarqueeTaskDialogCommand = new AsyncReactiveCommand();
            this.MarqueeTaskDialogCommand
                .Subscribe(async _ => await this.View.ShowMarqueeTaskDialogAsync())
                .AddTo(this.CompositeDisposable);

            #endregion
        }

        public void Loaded(dynamic view)
        {
            this.View = view;
        }

        public dynamic View { get; private set; }

        public AsyncReactiveCommand SimpleTaskDialogCommand { get; }

        public AsyncReactiveCommand AllControlsTaskDialogCommand { get; }

        public AsyncReactiveCommand CustomButtonTaskDialogCommand { get; }

        public AsyncReactiveCommand CommonLinkTaskDialogCommand { get; }

        public AsyncReactiveCommand RadioButtonTaskDialogCommand { get; }

        public AsyncReactiveCommand MarqueeTaskDialogCommand { get; }
    }
}