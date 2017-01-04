using System;
using System.Diagnostics;
using System.Reactive.Linq;
using MultiScreenSample.Views;
using Reactive.Bindings;
using starshipxac.MinimalMVVM;
using starshipxac.Windows.Devices;

namespace MultiScreenSample.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            #region ReactiveProperty

            this.CustomWindowState = new ReactiveProperty<CustomWindowStates>(CustomWindowStates.Normal);

            this.CloseCommand = new ReactiveCommand();
            this.CloseCommand.Subscribe(_ => this.View.Close());

            this.FullScreenCommand = this.CustomWindowState
                .Select(x => x != CustomWindowStates.FullScreen)
                .ToReactiveCommand();
            this.FullScreenCommand.Subscribe(_ => this.View.CustomWindowState = CustomWindowStates.FullScreen);

            this.RestoreCommand = this.CustomWindowState
                .Select(x => x != CustomWindowStates.Normal)
                .ToReactiveCommand();
            this.RestoreCommand.Subscribe(_ => this.View.CustomWindowState = CustomWindowStates.Normal);

            this.MaximizeCommand = this.CustomWindowState
                .Select(x => (x != CustomWindowStates.Maximized) && (x != CustomWindowStates.FullScreen))
                .ToReactiveCommand();
            this.MaximizeCommand.Subscribe(_ => this.View.CustomWindowState = CustomWindowStates.Maximized);

            this.MinimizeCommand = this.CustomWindowState
                .Select(x => (x != CustomWindowStates.Minimized) && (x != CustomWindowStates.FullScreen))
                .ToReactiveCommand();
            this.MinimizeCommand.Subscribe(_ => this.View.CustomWindowState = CustomWindowStates.Minimized);

            #endregion
        }

        public dynamic View { get; private set; }

        public ReactiveProperty<CustomWindowStates> CustomWindowState { get; }

        public ReactiveCommand CloseCommand { get; }

        public ReactiveCommand FullScreenCommand { get; }

        public ReactiveCommand RestoreCommand { get; }

        public ReactiveCommand MaximizeCommand { get; }

        public ReactiveCommand MinimizeCommand { get; }

        public void Loaded(dynamic view)
        {
            this.View = view;

            foreach (var monitor in MultiScreen.EnumerateAllMonitors())
            {
                Debug.WriteLine($"{monitor.DeviceName}: Bounds=({monitor.Bounds}), WorkingArea=({monitor.WorkingArea}), DPI={monitor.Dpi}");
            }
        }
    }
}