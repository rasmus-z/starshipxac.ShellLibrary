using System;
using Livet;
using Reactive.Bindings;

namespace TaskDialogSample.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            #region ReactiveProperty

            this.SimpleTaskDialogCommand = new ReactiveCommand();
            this.SimpleTaskDialogCommand.Subscribe(async _ =>
            {
                await DispatcherHelper.UIDispatcher.InvokeAsync(() =>
                {
                    this.View.ShowSimpleTaskDialog();
                });
            });

            this.AllControlsTaskDialogCommand = new ReactiveCommand();
            this.AllControlsTaskDialogCommand.Subscribe(async _ =>
            {
                await DispatcherHelper.UIDispatcher.InvokeAsync(() =>
                {
                    this.View.ShowAllControlsTaskDialog();
                });
            });

            this.CustomButtonTaskDialogCommand = new ReactiveCommand();
            this.CustomButtonTaskDialogCommand.Subscribe(async _ =>
            {
                await DispatcherHelper.UIDispatcher.InvokeAsync(() =>
                {
                    this.View.ShowCustomButtonTaskDialog();
                });
            });

            this.CommonLinkTaskDialogCommand = new ReactiveCommand();
            this.CommonLinkTaskDialogCommand.Subscribe(async _ =>
            {
                await DispatcherHelper.UIDispatcher.InvokeAsync(() =>
                {
                    this.View.ShowCommandLinkTaskDialog();
                });
            });

            this.RadioButtonTaskDialogCommand = new ReactiveCommand();
            this.RadioButtonTaskDialogCommand.Subscribe(async _ =>
            {
                await DispatcherHelper.UIDispatcher.InvokeAsync(() =>
                {
                    this.View.ShowRadioButtonTaskDialog();
                });
            });

            this.MarqueeTaskDialogCommand = new ReactiveCommand();
            this.MarqueeTaskDialogCommand.Subscribe(async _ =>
            {
                await DispatcherHelper.UIDispatcher.InvokeAsync(() =>
                {
                    this.View.ShowMarqueeTaskDialog();
                });
            });

            #endregion
        }

        public void Loading(dynamic view)
        {
            this.View = view;
        }

        public dynamic View { get; private set; }

        public ReactiveCommand SimpleTaskDialogCommand { get; }

        public ReactiveCommand AllControlsTaskDialogCommand { get; }

        public ReactiveCommand CustomButtonTaskDialogCommand { get; }

        public ReactiveCommand CommonLinkTaskDialogCommand { get; }

        public ReactiveCommand RadioButtonTaskDialogCommand { get; }

        public ReactiveCommand MarqueeTaskDialogCommand { get; }
    }
}