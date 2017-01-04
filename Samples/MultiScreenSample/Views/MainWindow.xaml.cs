using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using starshipxac.Windows.Devices;

namespace MultiScreenSample.Views
{
    /// <summary>
    ///     MainWindow.xaml の相互作用ロジック
    /// </summary>
    [TemplateVisualState(Name = "Normal", GroupName = "WindowStates")]
    [TemplateVisualState(Name = "Minimized", GroupName = "WindowStates")]
    [TemplateVisualState(Name = "Maximized", GroupName = "WindowStates")]
    [TemplateVisualState(Name = "FullScreen", GroupName = "WindowStates")]
    public partial class MainWindow : Window
    {
        private Rect restoreWindowBounds;
        private WindowState restoreWindowState;
        private readonly ManualResetEventSlim customWindowStateChanging = new ManualResetEventSlim();

        public MainWindow()
        {
            InitializeComponent();

            this.StateChanged += OnStateChanged;
        }

        #region CustomWindowState Property

        public CustomWindowStates CustomWindowState
        {
            get { return (CustomWindowStates)GetValue(CustomWindowStateProperty); }
            set { SetValue(CustomWindowStateProperty, value); }
        }

        public static readonly DependencyProperty CustomWindowStateProperty = DependencyProperty.Register(
            "CustomWindowState", typeof(CustomWindowStates), typeof(MainWindow),
            new PropertyMetadata(CustomWindowStates.Normal, (d, e) =>
            {
                var window = d as MainWindow;
                var newWindowState = (CustomWindowStates)e.NewValue;
                var oldWindowState = (CustomWindowStates)e.OldValue;
                window?.SetCustomWindowState(newWindowState, oldWindowState);
                window?.OnCustomWindowStateChanged(window, new CustomWindowStateEventArgs(newWindowState, oldWindowState));
            }));

        #endregion

        #region CustomWindowState Event

        public event EventHandler<CustomWindowStateEventArgs> CustomWindowStateChanged;

        protected virtual void OnCustomWindowStateChanged(object sender, CustomWindowStateEventArgs args)
        {
            this.CustomWindowStateChanged?.Invoke(sender, args);
        }

        #endregion

        private void OnStateChanged(object sender, EventArgs e)
        {
            Debug.WriteLine($"OnStateChanged: WindowState={this.WindowState}");

            if (!this.customWindowStateChanging.IsSet)
            {
                if (this.WindowState == WindowState.Minimized &&
                    this.CustomWindowState != CustomWindowStates.Minimized)
                {
                    this.CustomWindowState = CustomWindowStates.Minimized;
                }
                else if (this.WindowState == WindowState.Maximized &&
                         this.CustomWindowState != CustomWindowStates.Maximized)
                {
                    this.CustomWindowState = CustomWindowStates.Maximized;
                }
                else
                {
                    if (this.CustomWindowState != CustomWindowStates.FullScreen)
                    {
                        this.CustomWindowState = CustomWindowStates.Normal;
                    }
                }
            }
        }

        private void SetCustomWindowState(CustomWindowStates newWindowState, CustomWindowStates oldWindowState)
        {
            Debug.WriteLine("SetCustomWindowState: begin");

            this.customWindowStateChanging.Set();
            try
            {
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
                        // 通常ウィンドウ
                        this.WindowState = WindowState.Normal;
                        VisualStateManager.GoToElementState(this, "Normal", true);
                    }
                    else if (newWindowState == CustomWindowStates.Minimized)
                    {
                        // 最小化
                        this.WindowState = WindowState.Minimized;
                        VisualStateManager.GoToElementState(this, "Minimized", true);
                    }
                    else if (newWindowState == CustomWindowStates.Maximized)
                    {
                        // 最大化
                        this.WindowState = WindowState.Maximized;
                        VisualStateManager.GoToElementState(this, "Maximized", true);
                    }
                }
            }
            finally
            {
                this.customWindowStateChanging.Reset();
            }

            Debug.WriteLine("SetCustomWindowState: end");
        }

        private void ChangeToFullScreen()
        {
            this.restoreWindowBounds = this.RestoreBounds;
            this.restoreWindowState = this.WindowState;

            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Normal;

            var monitor = MultiScreen.FromWindow(this);
            Debug.WriteLine($"Bounds=({monitor.Bounds}), WorkingArea=({monitor.WorkingArea}), DPI={monitor.Dpi}");

            this.Left = monitor.Bounds.Left;
            this.Top = monitor.Bounds.Top;
            this.Width = monitor.Bounds.Width;
            this.Height = monitor.Bounds.Height;

            VisualStateManager.GoToElementState(this, "FullScreen", true);

            Debug.WriteLine($"({this.Left}, {this.Top}) - ({this.ActualWidth}, {this.ActualHeight})");
        }

        private void RestoreFromFullScreen()
        {
            this.ResizeMode = ResizeMode.CanResize;
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.Left = this.restoreWindowBounds.Left;
            this.Top = this.restoreWindowBounds.Top;
            this.Width = this.restoreWindowBounds.Width;
            this.Height = this.restoreWindowBounds.Height;

            if (this.restoreWindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
            }
            else if (this.restoreWindowState == WindowState.Minimized)
            {
                this.WindowState = WindowState.Minimized;
            }
        }
    }
}