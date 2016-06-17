using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Livet;
using Reactive.Bindings;
using starshipxac.Shell.PropertySystem;
using ShellFileDialogSample.Controllers;

namespace ShellFileDialogSample.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            #region Reactive Property

            this.Properties = new ReactiveCollection<IShellProperty>();
            this.PropertiesView = CollectionViewSource.GetDefaultView(this.Properties);

            // 開くファイル選択
            this.SelectOpenFileCommand = new ReactiveCommand();
            this.SelectOpenFileCommand.Subscribe(async _ =>
            {
                var selectedFile = await this.View.ShowSelectOpenFileDialogAsync();
                if (selectedFile != null)
                {
                    this.Properties.Clear();
                    foreach (var property in selectedFile.Properties.DefaultPropertyCollection)
                    {
                        this.Properties.Add(property);
                    }
                }
            });

            // 保存ファイル選択
            this.SelectSaveFileCommand = new ReactiveCommand();
            this.SelectSaveFileCommand.Subscribe(async _ =>
            {
                var selectedFile = await this.View.ShowSelectSaveFileDialogAsync();
                if (selectedFile != null)
                {
                    this.Properties.Clear();
                    foreach (var property in selectedFile.Properties.DefaultPropertyCollection)
                    {
                        this.Properties.Add(property);
                    }
                }
            });

            // フォルダー選択
            this.SelectFolderCommand = new ReactiveCommand();
            this.SelectFolderCommand.Subscribe(async _ =>
            {
                var selectedFolder = await this.View.ShowSelectFolderDialogAsync();
                if (selectedFolder != null)
                {
                    this.Properties.Clear();
                    foreach (var property in selectedFolder.Properties.DefaultPropertyCollection)
                    {
                        this.Properties.Add(property);
                    }
                }
            });

            // カスタム FileOpenDialog
            this.ShowCustomOpenFileDialogCommand = new ReactiveCommand();
            this.ShowCustomOpenFileDialogCommand.Subscribe(_ =>
            {
                var selectedFile = this.View.ShowCustomFileOpenDialog();
                if (selectedFile != null)
                {
                    this.Properties.Clear();
                    foreach (var property in selectedFile.Properties.DefaultPropertyCollection)
                    {
                        this.Properties.Add(property);
                    }
                }
            });

            #endregion
        }

        public void Loading(IMainView view)
        {
            this.View = view;
        }

        public IMainView View { get; private set; }

        public ReactiveCollection<IShellProperty> Properties { get; }

        public ICollectionView PropertiesView { get; }

        public ReactiveCommand SelectOpenFileCommand { get; }

        public ReactiveCommand SelectSaveFileCommand { get; }

        public ReactiveCommand SelectFolderCommand { get; }

        public ReactiveCommand ShowCustomOpenFileDialogCommand { get; }
    }
}