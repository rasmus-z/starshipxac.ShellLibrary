using System;
using System.Windows;
using System.Windows.Threading;

namespace WindowsControlsSample.Views
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window, IMainView
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public DispatcherOperation ShowProgressViewAsync()
        {
            return this.Dispatcher.InvokeAsync(() =>
            {
                var window = new ProgressSampleWindow();
                window.ShowDialog();
            });
        }
    }
}