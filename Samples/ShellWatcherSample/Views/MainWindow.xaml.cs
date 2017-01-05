using System;
using System.Windows;
using ShellWatcherSample.ViewModels;

namespace ShellWatcherSample.Views
{
    /// <summary>
    ///     MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window, IMainView
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void WriteLine(string message)
        {
            this.RichTextBox.AppendText(message);
            this.RichTextBox.AppendText(Environment.NewLine);
        }
    }
}