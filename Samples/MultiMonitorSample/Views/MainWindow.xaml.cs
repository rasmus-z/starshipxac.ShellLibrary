using System;
using System.Diagnostics;
using System.Windows;
using starshipxac.Windows.Devices;

namespace MultiMonitorSample.Views
{
    /// <summary>
    ///     MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private CustomWindowStates customWindowState;
        private Rect restoreWindowBounds;
        private WindowState restoreWindowState;

        public MainWindow()
        {
            InitializeComponent();

            this.ContentRendered += OnContentRendered;

            this.DynamicDataContext.Loaded(this);
        }

        public dynamic DynamicDataContext => this.DataContext;

        public CustomWindowStates CustomWindowState
        {
            get { return this.customWindowState; }
            set { SetCustomWindowState(value); }
        }

        private void OnContentRendered(object sender, EventArgs e)
        {
            this.DynamicDataContext.Initialize();
        }

        public void FullScreen()
        {
            this.CustomWindowState = CustomWindowStates.FullScreen;
        }

        public void Restore()
        {
            this.CustomWindowState = CustomWindowStates.Normal;
        }

        public void Maximize()
        {
            this.CustomWindowState = CustomWindowStates.Maximized;
        }

        public void Minimize()
        {
            this.CustomWindowState = CustomWindowStates.Minimized;
        }

        private void SetCustomWindowState(CustomWindowStates newWindowState)
        {
            var oldWindowState = this.customWindowState;
            this.customWindowState = newWindowState;

            if (newWindowState == CustomWindowStates.FullScreen)
            {
                // フルスクリーン
                ChangeToFullScreen();
            }
            else
            {
                if (oldWindowState == CustomWindowStates.FullScreen)
                {
                    RestoreFromFullScreen();
                }

                if (newWindowState == CustomWindowStates.Normal)
                {
                    this.WindowState = WindowState.Normal;
                }
                else if (newWindowState == CustomWindowStates.Maximized)
                {
                    // 最大化
                    this.WindowState = WindowState.Maximized;
                }
                else if (newWindowState == CustomWindowStates.Minimized)
                {
                    // 最小化
                    this.WindowState = WindowState.Minimized;
                }
            }
        }

        private void ChangeToFullScreen()
        {
            this.restoreWindowBounds = new Rect(this.Left, this.Top, this.Width, this.Height);
            this.restoreWindowState = this.WindowState;

            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Normal;

            var monitor = MultiMonitor.FromWindow(this);
            Debug.WriteLine($"Bounds=({monitor.Bounds}), WorkingArea=({monitor.WorkingArea}), DPI={monitor.Dpi}");

            this.Left = monitor.Bounds.Left;
            this.Top = monitor.Bounds.Top;
            this.Width = monitor.Bounds.Width;
            this.Height = monitor.Bounds.Height;
            Debug.WriteLine($"({this.Left}, {this.Top}) - ({this.ActualWidth}, {this.ActualHeight})");
        }

        private void RestoreFromFullScreen()
        {
            this.ResizeMode = ResizeMode.CanResize;
            this.WindowStyle = WindowStyle.SingleBorderWindow;

            if (this.restoreWindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
            }
            else if (this.restoreWindowState == WindowState.Minimized)
            {
                this.WindowState = WindowState.Minimized;
            }
            else
            {
                this.Left = this.restoreWindowBounds.Left;
                this.Top = this.restoreWindowBounds.Top;
                this.Width = this.restoreWindowBounds.Width;
                this.Height = this.restoreWindowBounds.Height;
            }
        }
    }
}