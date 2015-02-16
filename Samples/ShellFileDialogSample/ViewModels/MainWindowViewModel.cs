using System;
using System.Diagnostics;
using System.Linq;
using Codeplex.Reactive;
using Livet;
using ShellFileDialogSample.Messaging.Dialogs;
using starshipxac.Shell.PropertySystem;

namespace ShellFileDialogSample.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            #region Reactive Property

            this.Properties = new ReactiveCollection<IShellProperty>();

            #region 開くファイル選択

            this.SelectOpenFileCommand = new ReactiveCommand();
            this.SelectOpenFileCommand.Subscribe(async _ =>
            {
                var message = new SelectOpenFileMessage("SelectOpenFile");
                await this.Messenger.GetResponseAsync(message);
                var selectedFile = message.Response.FirstOrDefault();
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

            #region 保存ファイル選択

            this.SelectSaveFileCommand = new ReactiveCommand();
            this.SelectSaveFileCommand.Subscribe(async _ =>
            {
                var message = new SelectSaveFileMessage("SelectSaveFile");
                await this.Messenger.GetResponseAsync(message);
                var selectedFile = message.Response.FirstOrDefault();
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

            #region フォルダー選択

            this.SelectFolderCommand = new ReactiveCommand();
            this.SelectFolderCommand.Subscribe(async _ =>
            {
                var message = new SelectFolderMessage("SelectFolder");
                await this.Messenger.GetResponseAsync(message);
                var selectedFolder = message.Response.FirstOrDefault();
                if (selectedFolder != null)
                {
                    this.Properties.Clear();
                    foreach (var property in selectedFolder.Properties.DefaultPropertyCollection)
                    {
                        this.Properties.Add(property);
                    }
                }
            });

            #endregion

            #region カスタム FileOpenDialog

            this.ShowCustomOpenFileDialogCommand = new ReactiveCommand();
            this.ShowCustomOpenFileDialogCommand.Subscribe(async _ =>
            {
                var message = new CustomFileOpenDialogMessage("CustomFileOpenDialog");
                await this.Messenger.GetResponseAsync(message);
                var selectedFile = message.Response.SelectedFiles.FirstOrDefault();
                if (selectedFile != null)
                {
                    this.Properties.Clear();
                    foreach (var property in selectedFile.Properties.DefaultPropertyCollection)
                    {
                        this.Properties.Add(property);
                    }

                    Debug.WriteLine("SelectedItemIndex = {0}", message.Response.SelectedItemIndex);
                }
            });

            #endregion

            #endregion
        }

        public void Initialize()
        {
        }

        public ReactiveCollection<IShellProperty> Properties { get; private set; }

        public ReactiveCommand SelectOpenFileCommand { get; private set; }

        public ReactiveCommand SelectSaveFileCommand { get; private set; }

        public ReactiveCommand SelectFolderCommand { get; private set; }

        public ReactiveCommand ShowCustomOpenFileDialogCommand { get; private set; }
    }
}