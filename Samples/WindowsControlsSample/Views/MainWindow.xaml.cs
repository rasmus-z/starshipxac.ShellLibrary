using System;
using System.Windows;
using WindowsControlsSample.Manipulations;

namespace WindowsControlsSample.Views
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window, IMainWindowManipulator
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DynamicContext.Loading(this);
        }

        private dynamic DynamicContext
        {
            get
            {
                return this.DataContext;
            }
        }

        public void ShowProgressView()
        {
            var window = new ProgressSampleWindow();
            window.ShowDialog();
        }
    }
}