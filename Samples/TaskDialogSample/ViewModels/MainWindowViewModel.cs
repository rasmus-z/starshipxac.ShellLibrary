using System;
using Livet;
using Reactive.Bindings;
using TaskDialogSample.Manipulations;

namespace TaskDialogSample.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            #region ReactiveProperty

            this.SimpleTaskDialogCommand = new ReactiveCommand();
            this.SimpleTaskDialogCommand.Subscribe(_ => this.Manipulator.ShowSimpleTaskDialog());

            this.AllControlsTaskDialogCommand = new ReactiveCommand();
            this.AllControlsTaskDialogCommand.Subscribe(_ => this.Manipulator.ShowAllControlsTaskDialog());

            this.CustomButtonTaskDialogCommand = new ReactiveCommand();
            this.CustomButtonTaskDialogCommand.Subscribe(_ => this.Manipulator.ShowCustomButtonTaskDialog());

            this.CommonLinkTaskDialogCommand = new ReactiveCommand();
            this.CommonLinkTaskDialogCommand.Subscribe(_ => this.Manipulator.ShowCommandLinkTaskDialog());

            this.RadioButtonTaskDialogCommand = new ReactiveCommand();
            this.RadioButtonTaskDialogCommand.Subscribe(_ => this.Manipulator.ShowRadioButtonTaskDialog());

            this.MarqueeTaskDialogCommand = new ReactiveCommand();
            this.MarqueeTaskDialogCommand.Subscribe(_ => this.Manipulator.ShowMarqueeTaskDialog());

            #endregion
        }

        public void Loading(IMainWindowManipulator manipulator)
        {
            this.Manipulator = manipulator;
        }

        public void Initialize()
        {
        }

        public IMainWindowManipulator Manipulator { get; private set; }

        public ReactiveCommand SimpleTaskDialogCommand { get; private set; }

        public ReactiveCommand AllControlsTaskDialogCommand { get; private set; }

        public ReactiveCommand CustomButtonTaskDialogCommand { get; private set; }

        public ReactiveCommand CommonLinkTaskDialogCommand { get; private set; }

        public ReactiveCommand RadioButtonTaskDialogCommand { get; private set; }

        public ReactiveCommand MarqueeTaskDialogCommand { get; private set; }
    }
}