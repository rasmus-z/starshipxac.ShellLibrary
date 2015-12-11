using System;
using System.Diagnostics;
using Livet;
using Reactive.Bindings;
using starshipxac.Windows.Devices;

namespace MultiMonitorSample.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            #region ReactiveProperty

            this.CloseCommand = new ReactiveCommand();
            this.CloseCommand.Subscribe(_ => this.View.Close());

            this.FullScreenCommand = new ReactiveCommand();
            this.FullScreenCommand.Subscribe(_ => this.View.FullScreen());

            this.RestoreCommand = new ReactiveCommand();
            this.RestoreCommand.Subscribe(_ => this.View.Restore());

            this.MaximizeCommand = new ReactiveCommand();
            this.MaximizeCommand.Subscribe(_ => this.View.Maximize());

            this.MinimizeCommand = new ReactiveCommand();
            this.MinimizeCommand.Subscribe(_ => this.View.Minimize());

            #endregion
        }

        public dynamic View { get; private set; }

        public ReactiveCommand CloseCommand { get; }

        public ReactiveCommand FullScreenCommand { get; }

        public ReactiveCommand RestoreCommand { get; }

        public ReactiveCommand MaximizeCommand { get; }

        public ReactiveCommand MinimizeCommand { get; }

        public void Loaded(dynamic view)
        {
            this.View = view;

            foreach (var monitor in MultiMonitor.EnumerateAllMonitors())
            {
                Debug.WriteLine($"{monitor.DeviceName}: Bounds=({monitor.Bounds}), WorkingArea=({monitor.WorkingArea}), DPI={monitor.Dpi}");
            }
        }

        public void Initialize()
        {
        }

    }
}