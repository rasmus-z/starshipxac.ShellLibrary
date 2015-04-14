using System;
using WindowsControlsSample.Manipulations;
using Livet;
using Reactive.Bindings;

namespace WindowsControlsSample.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            #region RectiveProperty

            this.ShowProgressViewCommand = new ReactiveCommand();
            this.ShowProgressViewCommand.Subscribe(_ => this.Manipulator.ShowProgressView());

            #endregion
        }

        public void Loading(IMainWindowManipulator manipurator)
        {
            this.Manipulator = manipurator;
        }

        public void Initialize()
        {
        }

        public IMainWindowManipulator Manipulator { get; private set; }

        public ReactiveCommand ShowProgressViewCommand { get; private set; }
    }
}