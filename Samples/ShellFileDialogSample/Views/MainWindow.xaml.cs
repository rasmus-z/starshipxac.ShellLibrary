﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using starshipxac.Shell;
using starshipxac.Windows.Shell.Dialogs;
using starshipxac.Windows.Shell.Dialogs.Controls;

namespace ShellFileDialogSample.Views
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (this.DynamicDataContext != null)
            {
                this.DynamicDataContext.Loading(this);
            }
        }

        public dynamic DynamicDataContext => this.DataContext;

        public async Task<ShellFile> ShowSelectOpenFileDialogAsync()
        {
            var selector = new OpenFileSelector();
            return await selector.SelectSingleFileAsync();
        }

        public async Task<ShellFile> ShowSelectSaveFileDialogAsync()
        {
            var selector = new SaveFileSelector();
            return await selector.SelectSaveFileAsync();
        }

        public async Task<ShellFolder> ShowSelectFolderDialogAsync()
        {
            var selector = new FolderSelector();
            return await selector.SelectSingleFolderAsync();
        }

        public ShellFile ShowCustomFileOpenDialog()
        {
            using (var dialog = new FileOpenDialog())
            {
                // Custom Controls
                var button1 = new FileDialogButton("button", "Button1");
                button1.Click += (_, args) => MessageBox.Show("Button1", "Message");
                dialog.Controls.Add(button1);

                var combo1 = new FileDialogComboBox("combo1",
                    new FileDialogComboBoxItem("Item1"),
                    new FileDialogComboBoxItem("Item2"),
                    new FileDialogComboBoxItem("Item3"));
                dialog.Controls.Add(combo1);

                if (dialog.Show() == FileDialogResult.Ok)
                {
                    return dialog.GetShellFiles().FirstOrDefault();
                }
                return null;
            }
        }
    }
}