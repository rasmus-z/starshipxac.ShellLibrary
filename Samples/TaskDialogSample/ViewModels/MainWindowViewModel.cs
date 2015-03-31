using System;
using Codeplex.Reactive;
using Livet;
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

            this.AllPartsTaskDialogCommand = new ReactiveCommand();
            this.AllPartsTaskDialogCommand.Subscribe(_ => this.Manipulator.ShowAllPartsTaskDialog());

            this.CustomButtonTaskDialogCommand = new ReactiveCommand();
            this.CustomButtonTaskDialogCommand.Subscribe(_ => this.Manipulator.ShowCustomButtonTaskDialog());

            this.CommonLinkTaskDialogCommand = new ReactiveCommand();
            this.CommonLinkTaskDialogCommand.Subscribe(_ => this.Manipulator.ShowCommandLinkTaskDialog());

            this.RadioButtonTaskDialogCommand = new ReactiveCommand();
            this.RadioButtonTaskDialogCommand.Subscribe(_ => this.Manipulator.ShowRadioButtonTaskDialog());

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

        public ReactiveCommand AllPartsTaskDialogCommand { get; private set; }

        public ReactiveCommand CustomButtonTaskDialogCommand { get; private set; }

        public ReactiveCommand CommonLinkTaskDialogCommand { get; private set; }

        public ReactiveCommand RadioButtonTaskDialogCommand { get; private set; }
    }
}