using System;
using System.Windows.Data;
using Reactive.Bindings;
using starshipxac.MinimalMVVM;
using starshipxac.Shell.PropertySystem;
using ShellFileDialogSample.Controllers;

namespace ShellFileDialogSample.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            #region Reactive Property

            this.Properties = new ReactiveCollection<IShellProperty>();
            this.PropertiesSource = CollectionViewSource.GetDefaultView(this.Properties) as ListCollectionView;
            if (this.PropertiesSource != null)
            {
                this.PropertiesSource.CustomSort = new ShellPropertyComparer();
            }

            // 開くファイル選択
            this.SelectOpenFileCommand = new ReactiveCommand();
            this.SelectOpenFileCommand.Subscribe(async _ =>
            {
                var selectedFile = await this.View.ShowSelectOpenFileDialogAsync();
                if (selectedFile != null)
                {
                    this.Properties.Clear();
                    foreach (var property in selectedFile.Properties)
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
                    foreach (var property in selectedFile.Properties)
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
                    foreach (var property in selectedFolder.Properties)
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
                    foreach (var property in selectedFile.Properties)
                    {
                        this.Properties.Add(property);
                    }
                }
            });

            #endregion
        }

        public void Loaded(dynamic view)
        {
            this.View = view as IMainView;
        }

        public IMainView View { get; private set; }

        public ReactiveCollection<IShellProperty> Properties { get; }

        public ListCollectionView PropertiesSource { get; }

        public ReactiveCommand SelectOpenFileCommand { get; }

        public ReactiveCommand SelectSaveFileCommand { get; }

        public ReactiveCommand SelectFolderCommand { get; }

        public ReactiveCommand ShowCustomOpenFileDialogCommand { get; }
    }
}